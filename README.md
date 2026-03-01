#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""
UK Domain Catcher v2.1
v1.9 → v2.0 changes:
  1. Pipeline burst: send N creates, then read N responses (5-10x faster)
  2. Spin lock: busy-wait in last 200ms for precise timing
  3. Fast string parsing: no XML parsing during attack
  4. Tight attack window: 0.5s before drop instead of 3s
v2.0 → v2.1 fixes:
  5. Fixed greeting spam in debug log (check 500 chars, not 200)
  6. Fixed budget race condition (atomic burst reservation)
  7. PIPELINE_BURST default 15
v2.1 → v2.2 fixes:
  8. Fixed SEGFAULT: check_worker had no dedicated EPP connection
     Two threads doing SSL on same socket = crash
     Now: 5 workers for pipeline, 1 reserved for check
v2.2 → v2.3 fixes:
  9. Two-phase paced attack: creates spread AROUND drop time
     Before: all budget burned at T-0.5s, nothing left at drop
     Now: staggered workers (30ms apart) + 40ms pause between bursts
     Creates land from T-0.1s to T+2s instead of all at T-0.5s
 10. Catch statistics: tracks CAUGHT/TAKEN/TIMEOUT with timing
     Records catch_delay_sec = registration_time - drop_time
     Shows percentages, competitor rankings, delay distribution
v2.3 → v2.4:
 11. Dynamic burst pacing (replaced by v2.7 continuous pipeline)
v2.4 → v2.7:
 12. CONTINUOUS PIPELINE: zero-gap attack
     Before: send 15, read 15, pause = huge gaps where no creates flow
     Now: keep 8 creates in-flight per connection, non-blocking read
     Every response triggers immediate new send = creates flow non-stop
     5 workers × 8 in-flight = 40 creates always in the air
v2.7 → v2.8:
 13. Fixed check_worker never running: budget burned in <2s,
     stop_flag set immediately after workers exit, check_worker
     still sleeping → never detected TAKEN
     Now: wait for MAX_DURATION after workers finish
     First check at 1s instead of CHECK_INTERVAL
v2.8 → v2.9:
 14. TCP_NODELAY on EPP socket - disable Nagle's algorithm
     Without: kernel waits up to 40ms to batch small packets
     With: each create XML sent IMMEDIATELY = ~5-15ms faster per send
 15. Reduced time.time() calls in hot loop (every 100 iterations)
v2.9 → v3.0:
 16. select() timeout 0.0 instead of 0.001 (1ms sleep per check killed speed)
 17. Pre-baked frames: all XML encoded + header packed BEFORE attack
     During attack: sock.sendall(prebaked_bytes) = zero allocation
 18. Removed 0.5ms yield sleep - true spin during attack
 19. Budget reserve default 0% (was 10%)
v3.0 → v3.1:
 20. gc.disable() during attack - prevents random GC freezes
 21. perf_counter() for spin lock - monotonic, NTP-immune, nanosecond res
 22. CPU affinity: pin to cores 0,1 - prevents OS context switches
 23. ATTACK_BEFORE default 0.007s (RTT-based)
v3.2 → v3.3:
 24. Fixed SSL+select() bug: select() says ready but SSL record incomplete
     → recv() blocks for 2s timeout. Workers sent 8 each and froze.
     Now: pipeline_recv_nb() with 1ms timeout, no has_data_ready().
 25. Per-worker debug logging: sends, reads, timing per worker
 26. ATTACK_BEFORE configurable for earlier start (wastes some budget but primes pipeline)
v3.3 → v3.4:
 27. FIRE-ALL architecture: send ALL creates first, read responses after.
     Before: send 8, wait for response, send 1 more = 45 creates in 1s
     Now: blast ALL frames in one tight loop = ALL creates in <10ms
     No MAX_IN_FLIGHT limit. TCP buffer handles flow control.
 28. Separate send phase (pure fire) and recv phase (check for success)
v3.4 → v3.5:
 29. Fast parallel DAC checker - detects domain taken every 50ms during attack.
     Stops fire-all workers immediately when taken detected = saves budget.
     Uses dedicated DAC socket, no lock contention.

שימוש:
  python3 uk_catcher_v3.4.py
"""

import os
import gc
import sys
import time
import json
import struct
import socket
import threading
import tempfile
from datetime import datetime, timedelta, timezone

import paramiko
import requests
import logging

logging.getLogger("paramiko").setLevel(logging.CRITICAL)

from NominetEPP import NominetEPP, NominetDAC
from NominetDropList import get_nominet_droplist
from logger import log, debug, close_log


# ╔══════════════════════════════════════════════════════════════╗
# ║                    CONFIGURATION                             ║
# ╚══════════════════════════════════════════════════════════════╝

EPP_HOST = os.getenv("EPP_HOST", "epp.nominet.org.uk")
EPP_PORT = int(os.getenv("EPP_PORT", "700"))
EPP_USER = os.getenv("EPP_USER", "CRUCIO")
EPP_PASS = os.getenv("EPP_PASS", "V1z2n9v3V6z2n9v3")
NOMINET_TAG = os.getenv("NOMINET_TAG", "CRUCIO")
REGISTRANT_ID = os.getenv("REGISTRANT_ID", "20873794")

EPP_CONNECTIONS = int(os.getenv("EPP_CONNECTIONS", "6"))

# ---- FTP (SFTP) ----
FTP_HOST = os.getenv("FTP_HOST", "85.215.194.145")
FTP_USER = os.getenv("FTP_USER", "root")
FTP_PASS = os.getenv("FTP_PASS", "hpGEl8lG")
FTP_PORT = int(os.getenv("FTP_PORT", "22"))
REMOTE_PATH = os.getenv("REMOTE_PATH", "/root/domainsuk.txt")

# ---- אסטרטגיית תפיסה v2.0 ----
ATTACK_BEFORE_SEC = float(os.getenv("ATTACK_BEFORE", "0.007"))   # v3.0: RTT-based (7ms)
CHECK_INTERVAL_SEC = int(os.getenv("CHECK_INTERVAL", "2"))
MAX_DURATION_SEC = int(os.getenv("MAX_DURATION", "30"))
MAX_IN_FLIGHT = int(os.getenv("MAX_IN_FLIGHT", "8"))            # v2.7: creates in-flight per connection
STAGGER_MS = int(os.getenv("STAGGER_MS", "5"))                  # v2.7: ms between worker starts

# Budget
MIN_CREATES_PER_DOMAIN = int(os.getenv("MIN_CREATES_DOMAIN", "30"))
BUDGET_RESERVE_PCT = float(os.getenv("BUDGET_RESERVE_PCT", "0.0"))

# ---- מגבלות ----
MAX_REQUESTS_DAY = int(os.getenv("MAX_REQUESTS_DAY", "1000"))

# ---- תזמונים ----
FTP_REFRESH_SEC = int(os.getenv("FTP_REFRESH_SEC", "3600"))
HEARTBEAT_SEC = int(os.getenv("HEARTBEAT_SEC", "120"))
DROPLIST_REFRESH_HOURS = int(os.getenv("DROPLIST_REFRESH_H", "6"))

PRE_WARM_SEC = int(os.getenv("PRE_WARM_SEC", "15"))

CAUGHT_FILE = os.getenv("CAUGHT_FILE", "caught_uk.txt")
STATS_FILE = os.getenv("STATS_FILE", "catch_stats.json")


# ╔══════════════════════════════════════════════════════════════╗
# ║                  CATCH STATISTICS v2.3                       ║
# ╚══════════════════════════════════════════════════════════════╝

class CatchStats:
    """
    v2.3: מעקב סטטיסטיקות תפיסה - CAUGHT / TAKEN / TIMEOUT
    שומר לקובץ JSON, מחשב אחוזים ומגמות
    """
    def __init__(self, filepath=STATS_FILE):
        self.filepath = filepath
        self.records = []
        self.lock = threading.Lock()
        self._load()

    def _load(self):
        try:
            with open(self.filepath, "r") as f:
                self.records = json.load(f)
            log(f"[STATS] Loaded {len(self.records)} records")
        except FileNotFoundError:
            log(f"[STATS] No stats file - starting fresh")
        except Exception as e:
            log(f"[STATS] Load error: {e}")

    def _save(self):
        try:
            with open(self.filepath, "w") as f:
                json.dump(self.records, f, indent=2, default=str)
        except Exception as e:
            debug(f"[STATS] Save error: {e}")

    def add(self, record: dict):
        """הוסף רשומה חדשה"""
        with self.lock:
            self.records.append(record)
            self._save()

    def get_summary(self) -> str:
        """סיכום סטטיסטי עם דיוק ומידע שימושי"""
        with self.lock:
            recs = list(self.records)

        if not recs:
            return "  📈 No statistics yet"

        total = len(recs)
        caught = [r for r in recs if r.get("result") == "CAUGHT"]
        taken = [r for r in recs if r.get("result") == "TAKEN"]
        timeout = [r for r in recs if r.get("result") == "TIMEOUT"]

        msg = f"  📈 Total: {total} | ✅ CAUGHT: {len(caught)} ({100*len(caught)//total}%)"
        msg += f" | ❌ TAKEN: {len(taken)} ({100*len(taken)//total}%)"
        msg += f" | ⏰ TIMEOUT: {len(timeout)} ({100*len(timeout)//total}%)\n"

        # Competitor catch delays (how fast do winners register after drop?)
        comp_delays = []
        for r in taken:
            d = r.get("catch_delay_sec")
            if d is not None:
                comp_delays.append(d)

        # Our catch delays (when we caught)
        our_delays = []
        for r in caught:
            d = r.get("catch_delay_sec")
            if d is not None:
                our_delays.append(d)

        all_delays = comp_delays + our_delays
        if all_delays:
            all_delays.sort()
            n = len(all_delays)
            under_05 = sum(1 for d in all_delays if d < 0.5)
            under_1 = sum(1 for d in all_delays if d < 1.0)
            under_2 = sum(1 for d in all_delays if d < 2.0)
            under_3 = sum(1 for d in all_delays if d < 3.0)
            under_5 = sum(1 for d in all_delays if d < 5.0)
            avg = sum(all_delays) / n
            fastest = min(all_delays)
            slowest = max(all_delays)
            msg += f"  ⏱️ Catch delays ({n}): avg={avg:.2f}s | fastest={fastest:.2f}s | slowest={slowest:.2f}s\n"
            msg += f"     <0.5s: {100*under_05//n}% | <1s: {100*under_1//n}% | <2s: {100*under_2//n}% | <3s: {100*under_3//n}% | <5s: {100*under_5//n}%\n"

        # Last 5 results (recent history)
        recent = recs[-5:]
        if recent:
            msg += f"  📋 Recent:\n"
            for r in recent:
                result = r.get("result", "?")
                domain = r.get("domain", "?")
                icon = "✅" if result == "CAUGHT" else "❌" if result == "TAKEN" else "⏰"
                delay = r.get("catch_delay_sec")
                delay_str = f"{delay:.2f}s" if delay is not None else "?"
                tag = r.get("registrar_tag", "")
                creates = r.get("creates", 0)
                msg += f"     {icon} {domain} → {result} | delay: {delay_str} | {creates} creates"
                if result == "TAKEN" and tag:
                    msg += f" | by: {tag}"
                msg += "\n"

        # Top competitors
        competitors = {}
        for r in taken:
            tag = r.get("registrar_tag", "unknown")
            competitors[tag] = competitors.get(tag, 0) + 1

        if competitors:
            top = sorted(competitors.items(), key=lambda x: -x[1])[:5]
            comp_str = " | ".join(f"{tag}: {cnt}" for tag, cnt in top)
            msg += f"  🏁 Competitors: {comp_str}"

        return msg


# ╔══════════════════════════════════════════════════════════════╗
# ║                  REQUEST BUDGET TRACKER                      ║
# ╚══════════════════════════════════════════════════════════════╝

BUDGET_FILE = os.getenv("BUDGET_FILE", "epp_budget.json")

class RequestBudget:
    """מעקב בקשות EPP - rolling 24 hours"""

    def __init__(self, max_day=MAX_REQUESTS_DAY):
        self.max_day = max_day
        self.timestamps = []
        self.lock = threading.Lock()
        self._load()

    def _prune(self):
        cutoff = time.time() - 86400
        self.timestamps = [t for t in self.timestamps if t > cutoff]

    def _load(self):
        try:
            with open(BUDGET_FILE, "r") as f:
                data = json.load(f)
                self.timestamps = data.get("timestamps", [])
                self._prune()
                used = len(self.timestamps)
                remaining = max(0, self.max_day - used)
                if used > 0:
                    oldest = datetime.fromtimestamp(self.timestamps[0], tz=timezone.utc)
                    frees_at = oldest + timedelta(hours=24)
                    log(f"[BUDGET] Loaded: {used} used in last 24h ({remaining} remaining)")
                    log(f"[BUDGET] First slot frees at {frees_at.strftime('%H:%M:%S')} UTC")
                else:
                    log(f"[BUDGET] Clean slate - {self.max_day} available")
        except FileNotFoundError:
            log(f"[BUDGET] No budget file - starting fresh ({self.max_day} available)")
        except Exception as e:
            log(f"[BUDGET] Load error: {e}")

    def _save(self):
        try:
            with open(BUDGET_FILE, "w") as f:
                json.dump({"timestamps": self.timestamps}, f)
        except Exception:
            pass

    def can_request(self):
        with self.lock:
            self._prune()
            return len(self.timestamps) < self.max_day

    def use(self, n=1):
        with self.lock:
            self._prune()
            now = time.time()
            for _ in range(n):
                self.timestamps.append(now)
            if len(self.timestamps) % 10 == 0:
                self._save()
            return len(self.timestamps)

    def remaining(self):
        with self.lock:
            self._prune()
            return max(0, self.max_day - len(self.timestamps))

    def used(self):
        with self.lock:
            self._prune()
            return len(self.timestamps)

    def next_free_slot(self):
        with self.lock:
            self._prune()
            if len(self.timestamps) < self.max_day:
                return 0
            if not self.timestamps:
                return 0
            oldest = min(self.timestamps)
            frees_at = oldest + 86400
            return max(0, frees_at - time.time())


# ╔══════════════════════════════════════════════════════════════╗
# ║                      SFTP CLIENT                             ║
# ╚══════════════════════════════════════════════════════════════╝

class LiveSFTP:
    def __init__(self, host, user, pw, port, remote_path):
        self.host = host
        self.user = user
        self.pw = pw
        self.port = port
        self.remote_path = remote_path
        self.transport = None
        self.sftp = None

    def _connect(self):
        try:
            self._close()
            sock = socket.create_connection((self.host, self.port), timeout=10)
            self.transport = paramiko.Transport(sock)
            self.transport.set_keepalive(30)
            self.transport.connect(username=self.user, password=self.pw)
            self.sftp = paramiko.SFTPClient.from_transport(self.transport)
        except Exception as e:
            log(f"[SFTP] Connect failed: {type(e).__name__}")
            self._close()

    def _close(self):
        try:
            if self.sftp: self.sftp.close()
        except: pass
        try:
            if self.transport: self.transport.close()
        except: pass
        self.transport = None
        self.sftp = None

    def _ensure(self):
        if self.sftp is None:
            self._connect()

    def download(self, local_path):
        self._ensure()
        if not self.sftp:
            return False
        try:
            with self.sftp.open(self.remote_path, "rb") as rf, open(local_path, "wb") as lf:
                while True:
                    data = rf.read(32768)
                    if not data: break
                    lf.write(data)
            return True
        except Exception:
            self._close()
            self._connect()
            if not self.sftp: return False
            try:
                with self.sftp.open(self.remote_path, "rb") as rf, open(local_path, "wb") as lf:
                    while True:
                        data = rf.read(32768)
                        if not data: break
                        lf.write(data)
                return True
            except:
                return False

    def upload(self, local_path):
        self._ensure()
        if not self.sftp: return False
        try:
            with open(local_path, "rb") as lf, self.sftp.open(self.remote_path, "wb") as rf:
                while True:
                    data = lf.read(32768)
                    if not data: break
                    rf.write(data)
            return True
        except:
            return False

    def close(self):
        self._close()


# ╔══════════════════════════════════════════════════════════════╗
# ║                     UK CATCHER v2.0                          ║
# ╚══════════════════════════════════════════════════════════════╝

class UKCatcher:
    def __init__(self):
        self.lock = threading.RLock()
        self.domains_uk = []
        self.nominet = get_nominet_droplist()
        self.budget = RequestBudget(MAX_REQUESTS_DAY)
        self.caught_domains = set()
        self.attempted_domains = set()
        self.attacking = set()
        self.dry_run = False

        self.epp_pool = []
        self.epp_index = 0
        self.epp_lock = threading.Lock()

        # v2.0: Pre-built XML templates per domain (bytes-ready)
        self._xml_cache = {}
        # v2.3: Statistics tracker
        self.catch_stats = CatchStats()
        # v3.2: Persistent DAC connection
        self.dac = NominetDAC()

    def _calc_budget_for_domain(self, domain):
        """חישוב budget דינמי לכל דומיין"""
        budget_remaining = self.budget.remaining()
        if budget_remaining <= 0:
            return 0

        now = datetime.now(timezone.utc)
        with self.lock:
            domains = list(self.domains_uk)

        upcoming_count = 0
        for d in domains:
            secs = self.nominet.get_seconds_until_drop(d)
            if secs is not None and -60 <= secs <= 86400:
                upcoming_count += 1

        upcoming_count = max(1, upcoming_count)
        usable = int(budget_remaining * (1.0 - BUDGET_RESERVE_PCT))
        usable = max(usable, MIN_CREATES_PER_DOMAIN)
        per_domain = usable // upcoming_count
        per_domain = max(MIN_CREATES_PER_DOMAIN, per_domain)
        per_domain = min(per_domain, budget_remaining)

        log(f"[BUDGET] {domain}: {per_domain} creates (budget={budget_remaining}, upcoming={upcoming_count})")
        return per_domain

    # ---- EPP POOL ----

    def init_epp(self):
        if not EPP_USER or not EPP_PASS:
            log("[EPP] ⚠ אין פרטי EPP - DRY-RUN mode")
            self.dry_run = True
            return False

        for i in range(EPP_CONNECTIONS):
            epp = NominetEPP(
                host=EPP_HOST, port=EPP_PORT,
                username=EPP_USER, password=EPP_PASS,
                tag=NOMINET_TAG or EPP_USER,
                registrant_id=REGISTRANT_ID
            )
            if epp.connect() and epp.login():
                self.epp_pool.append(epp)
                log(f"[EPP] ✓ Connection {i+1}/{EPP_CONNECTIONS} ready")
            else:
                log(f"[EPP] ✗ Connection {i+1}/{EPP_CONNECTIONS} FAILED")

        if self.epp_pool:
            log(f"[EPP] Pool: {len(self.epp_pool)} connections")
            threading.Thread(target=self._keepalive_loop, daemon=True).start()
            return True
        else:
            log("[EPP] ⚠ No connections - DRY-RUN mode")
            self.dry_run = True
            return False

    def _keepalive_loop(self):
        while True:
            time.sleep(240)
            for epp in self.epp_pool:
                try:
                    if not epp.hello():
                        epp.ensure_connected()
                except:
                    try:
                        epp.ensure_connected()
                    except:
                        pass

    def get_epp(self, index=None):
        if not self.epp_pool:
            return None
        if index is not None and index < len(self.epp_pool):
            epp = self.epp_pool[index]
        else:
            with self.epp_lock:
                epp = self.epp_pool[self.epp_index % len(self.epp_pool)]
                self.epp_index += 1

        if not epp.connected or not epp.logged_in:
            epp.ensure_connected()
        return epp if epp.connected and epp.logged_in else None

    def _pre_warm_connections(self):
        alive = 0
        for i, epp in enumerate(self.epp_pool):
            try:
                if epp.hello():
                    alive += 1
                else:
                    if epp.ensure_connected():
                        alive += 1
            except:
                try:
                    if epp.ensure_connected():
                        alive += 1
                except:
                    pass
        return alive

    def _build_create_xml(self, domain):
        """v2.0: Pre-built XML template with {trid} placeholder"""
        if domain in self._xml_cache:
            return self._xml_cache[domain]

        NS_EPP = "urn:ietf:params:xml:ns:epp-1.0"
        NS_DOMAIN = "urn:ietf:params:xml:ns:domain-1.0"

        registrant_block = ""
        if REGISTRANT_ID:
            registrant_block = f"<domain:registrant>{REGISTRANT_ID}</domain:registrant>"

        xml = f"""<?xml version="1.0" encoding="UTF-8" standalone="no"?>
<epp xmlns="{NS_EPP}">
  <command>
    <create>
      <domain:create xmlns:domain="{NS_DOMAIN}">
        <domain:name>{domain}</domain:name>
        <domain:period unit="y">2</domain:period>
        {registrant_block}
        <domain:authInfo><domain:pw/></domain:authInfo>
      </domain:create>
    </create>
    <clTRID>{{trid}}</clTRID>
  </command>
</epp>"""

        self._xml_cache[domain] = xml
        return xml

    # ---- FTP ----

    def load_domains_from_ftp(self):
        log("[FTP] Loading UK domains...")
        tmp = os.path.join(tempfile.gettempdir(), f"uk_dom_{int(time.time())}.txt")
        client = LiveSFTP(FTP_HOST, FTP_USER, FTP_PASS, FTP_PORT, REMOTE_PATH)
        ok = client.download(tmp)
        if not ok:
            client.close()
            log("[FTP] ✗ Download failed")
            return []
        client.close()

        try:
            with open(tmp, "r", encoding="utf-8", errors="ignore") as f:
                text = f.read()
        except Exception as e:
            log(f"[FTP] Read error: {e}")
            return []

        domains = []
        for line in text.splitlines():
            line = line.strip().lower()
            if not line or line.startswith("--") or line.startswith("#"):
                continue
            if "." in line and line.endswith(".uk"):
                domains.append(line)

        log(f"[FTP] Found {len(domains)} UK domains")
        for d in domains[:15]:
            print(f"  → {d}")
        if len(domains) > 15:
            print(f"  ... +{len(domains)-15} more")

        with self.lock:
            old_set = set(self.domains_uk)
            skip = self.caught_domains | self.attempted_domains
            self.domains_uk = sorted(set(domains) - skip)
            new_count = len(set(self.domains_uk) - old_set)
            if new_count:
                log(f"[FTP] {new_count} new UK domains added")

        return domains

    # ---- ATTACK v2.0: Continuous ----

    def attack_domain(self, domain, drop_time):
        d = domain.lower()

        with self.lock:
            if d in self.attacking or d in self.caught_domains:
                return
            self.attacking.add(d)

        try:
            self._run_attack(d, drop_time)
        finally:
            with self.lock:
                self.attacking.discard(d)

    def _run_attack(self, domain, drop_time):
        now = datetime.now(timezone.utc)
        secs_until = (drop_time - now).total_seconds()

        # v2.0: Pre-build XML template
        xml_template = self._build_create_xml(domain)

        # Pre-warm connections before waiting
        if secs_until > PRE_WARM_SEC:
            wait = secs_until - PRE_WARM_SEC
            log(f"[ATTACK] ⏳ {domain} - waiting {wait:.1f}s (pre-warm in {PRE_WARM_SEC}s)")
            time.sleep(wait)

        # Pre-warm all EPP connections
        alive = self._pre_warm_connections()
        log(f"[ATTACK] 🔌 Pre-warm: {alive}/{len(self.epp_pool)} connections alive")

        # v2.0: Sleep until 0.5s before drop (regular sleep)
        now = datetime.now(timezone.utc)
        secs_until = (drop_time - now).total_seconds()
        if secs_until > 0.5:
            time.sleep(secs_until - 0.5)

        # v3.0: SPIN LOCK with perf_counter (monotonic, high-res, NTP-immune)
        target_ts = drop_time.timestamp() - ATTACK_BEFORE_SEC
        now_ts = time.time()
        # Regular sleep until 200ms before target
        remaining = target_ts - now_ts
        if remaining > 0.2:
            time.sleep(remaining - 0.2)
        # v3.1: Disable GC during attack to prevent random freezes
        gc.disable()
        # Busy wait with perf_counter for final stretch
        wait_left = target_ts - time.time()
        spin_start = time.perf_counter()
        while (time.perf_counter() - spin_start) < wait_left:
            pass

        # ---- START ATTACK ----
        budget_now = self.budget.remaining()
        max_creates = self._calc_budget_for_domain(domain)
        num_attack_workers = max(1, len(self.epp_pool) - 1) if not self.dry_run else 1

        log(f"[ATTACK] 🚀 {domain} - FIRE ALL! (budget: {budget_now}, allocated: {max_creates}, "
            f"workers: {num_attack_workers})")

        # v3.0: Pre-bake ALL frames as bytes - zero allocation during attack
        # Each worker gets its own list of ready-to-send frame bytes
        prebaked_frames = {}
        frames_per_worker = (max_creates // num_attack_workers) + 1
        for w in range(num_attack_workers):
            frames = []
            for i in range(frames_per_worker):
                trid = f"c{w}-{i:06d}"
                xml = xml_template.format(trid=trid)
                raw = xml.encode("utf-8")
                frame = struct.pack("!I", len(raw) + 4) + raw
                frames.append(frame)
            prebaked_frames[w] = frames
        debug(f"[ATTACK] Pre-baked {num_attack_workers * frames_per_worker} frames")

        caught = threading.Event()
        taken_by_other = threading.Event()
        stop_flag = threading.Event()
        stats = {"creates": 0, "errors": 0, "responses": 0}
        stats_lock = threading.Lock()
        attack_start = time.time()

        def fire_all_worker(epp_idx, start_delay=0):
            """
            v3.4: FIRE-ALL worker - blast all creates, then read responses.
            Phase 1: Send ALL prebaked frames as fast as possible (no reading!)
            Phase 2: Read responses to detect success
            """
            if start_delay > 0:
                time.sleep(start_delay)

            epp = self.get_epp(epp_idx)
            if not epp:
                log(f"[WORKER-{epp_idx}] No EPP connection!")
                return

            epp.set_attack_timeout(5.0)
            my_frames = prebaked_frames.get(epp_idx, [])
            w_start = time.perf_counter()

            # ══════════════ PHASE 1: FIRE ALL ══════════════
            # Pure send loop - no reading, no checking, just blast
            w_sends = 0
            for frame in my_frames:
                if caught.is_set() or taken_by_other.is_set() or stop_flag.is_set():
                    break

                # Budget check
                can_send = False
                with stats_lock:
                    remaining = max_creates - stats["creates"]
                    if remaining > 0:
                        stats["creates"] += 1
                        can_send = True
                if not can_send:
                    break

                if not self.budget.can_request():
                    log(f"[ATTACK] ⚠ {domain} - BUDGET EXHAUSTED")
                    stop_flag.set()
                    break

                try:
                    epp.send_prebaked(frame)
                    self.budget.use(1)
                    w_sends += 1
                except Exception as e:
                    debug(f"[WORKER-{epp_idx}] Send failed: {e}")
                    with stats_lock:
                        stats["creates"] -= 1
                    break

            send_done = time.perf_counter()
            send_ms = (send_done - w_start) * 1000
            log(f"[WORKER-{epp_idx}] 🔫 FIRED {w_sends} creates in {send_ms:.1f}ms "
                f"({w_sends/max(0.001,send_ms/1000):.0f}/s)")

            # ══════════════ PHASE 2: READ RESPONSES ══════════════
            # Now read responses - mainly to detect if WE caught it
            w_reads = 0
            for _ in range(w_sends):
                if caught.is_set() or stop_flag.is_set():
                    break
                try:
                    success, reason = epp.pipeline_recv()

                    if success is None and reason in ("no_response", "no_data"):
                        break

                    w_reads += 1
                    with stats_lock:
                        stats["responses"] += 1

                    if success:
                        caught.set()
                        log(f"[ATTACK] 🎉🎉🎉 {domain} REGISTERED! (response #{stats['responses']})")
                        return

                    if reason == "rate_limit":
                        log(f"[WORKER-{epp_idx}] Rate limited!")
                        break

                    if reason == "syntax_error":
                        log(f"[ATTACK] ⛔ {domain} - EPP SYNTAX ERROR!")
                        stop_flag.set()
                        return
                except:
                    break

            w_elapsed = time.perf_counter() - w_start
            log(f"[WORKER-{epp_idx}] sends={w_sends} reads={w_reads} "
                f"send_phase={send_ms:.1f}ms total={w_elapsed:.3f}s "
                f"rate={w_sends/max(0.001,send_ms/1000):.0f}/s")

        def check_worker(dedicated_epp_idx):
            """בדיקת זמינות - uses dedicated EPP connection to avoid segfault"""
            time.sleep(1)  # v2.8: first check after 1s, then every CHECK_INTERVAL_SEC
            while not stop_flag.is_set():
                elapsed = time.time() - attack_start
                if elapsed > MAX_DURATION_SEC:
                    stop_flag.set()
                    return
                if caught.is_set():
                    stop_flag.set()
                    return

                status = None

                # EPP check - use dedicated connection only
                try:
                    check_epp = self.get_epp(dedicated_epp_idx)
                    if check_epp:
                        avail = check_epp.check_domain(domain)
                        if avail is True:
                            status = "available"
                            log(f"[CHECK-EPP] {domain} AVAILABLE!")
                        elif avail is False:
                            status = "taken"
                except:
                    pass

                # DAC fallback
                if status is None:
                    dac_status = self.dac.check(domain)
                    if dac_status:
                        status = dac_status

                # RDAP fallback
                if status is None:
                    try:
                        resp = requests.get(
                            f"https://rdap.nominet.uk/uk/domain/{domain}",
                            timeout=5,
                            headers={"Accept": "application/rdap+json", "User-Agent": "UKCatcher/3.5"}
                        )
                        if resp.status_code == 404:
                            status = "available"
                        elif resp.ok:
                            data = resp.json()
                            st = " ".join(str(s).lower() for s in data.get("status", []))
                            if "add period" in st or "addperiod" in st:
                                status = "taken"
                            elif "clienttransferprohibited" in st:
                                status = "taken"
                    except:
                        pass

                with stats_lock:
                    c = stats["creates"]
                log(f"[CHECK] {domain} | {elapsed:.0f}s | status={status} | creates={c}")

                if status == "taken":
                    if not caught.is_set():
                        taken_by_other.set()
                    stop_flag.set()
                    return

                time.sleep(CHECK_INTERVAL_SEC)

        def fast_dac_checker():
            """
            v3.5: Fast parallel DAC checker - detects taken in ~50ms intervals.
            Phase 1: Wait for domain to become Available (= dropped)
            Phase 2: Monitor for RF (= someone caught it) → stop workers
            """
            try:
                dac_sock = socket.create_connection(
                    ('dac.nominet.org.uk', 2043), timeout=2
                )
                dac_sock.setsockopt(socket.IPPROTO_TCP, socket.TCP_NODELAY, 1)
                dac_sock.settimeout(0.1)
            except Exception as e:
                log(f"[FAST-DAC] Connect failed: {e}")
                return

            checks = 0
            saw_available = False

            while not stop_flag.is_set() and not caught.is_set() and not taken_by_other.is_set():
                try:
                    dac_sock.sendall(f"{domain}\r\n".encode())
                    data = b""
                    try:
                        while b"\n" not in data:
                            chunk = dac_sock.recv(1024)
                            if not chunk:
                                break
                            data += chunk
                    except socket.timeout:
                        pass

                    checks += 1
                    if data:
                        text = data.decode("utf-8", errors="ignore").strip()
                        parts = text.split(",")
                        if len(parts) >= 2:
                            status = parts[-1].strip().upper()

                            if status == "A":
                                if not saw_available:
                                    elapsed_ms = (time.time() - attack_start) * 1000
                                    log(f"[FAST-DAC] 🟢 {domain} AVAILABLE at {elapsed_ms:.0f}ms (check #{checks})")
                                    saw_available = True

                            elif status == "RF" and saw_available:
                                # Was available, now taken = someone caught it
                                elapsed_ms = (time.time() - attack_start) * 1000
                                with stats_lock:
                                    c = stats["creates"]
                                log(f"[FAST-DAC] ⚡ {domain} TAKEN at {elapsed_ms:.0f}ms "
                                    f"(checked {checks}x, {c} creates sent)")
                                if not caught.is_set():
                                    taken_by_other.set()
                                stop_flag.set()
                                break

                except Exception as e:
                    debug(f"[FAST-DAC] Error: {e}")
                    break

                time.sleep(0.05)  # 50ms between checks

            try:
                dac_sock.close()
            except:
                pass

        def status_worker():
            while not stop_flag.is_set() and not caught.is_set() and not taken_by_other.is_set():
                elapsed = time.time() - attack_start
                with stats_lock:
                    c = stats["creates"]
                    r = stats["responses"]
                rate = c / max(1, elapsed)
                log(f"[ATTACK] {domain} | {elapsed:.0f}s | {c}/{max_creates} creates | {r} responses ({rate:.1f}/s)")
                time.sleep(15)

        # ---- Launch workers ----
        threads = []
        # v2.2: Reserve LAST connection for check_worker, use rest for pipeline
        check_epp_idx = len(self.epp_pool) - 1 if len(self.epp_pool) > 1 else 0

        # v3.4: No stagger - fire all workers simultaneously
        for i in range(num_attack_workers):
            t = threading.Thread(target=fire_all_worker, args=(i, 0), daemon=True)
            threads.append(t)
            t.start()

        t_check = threading.Thread(target=check_worker, args=(check_epp_idx,), daemon=True)
        t_check.start()

        # v3.5: Fast DAC checker - parallel detection
        t_fast_dac = threading.Thread(target=fast_dac_checker, daemon=True)
        t_fast_dac.start()

        t_status = threading.Thread(target=status_worker, daemon=True)
        t_status.start()

        for t in threads:
            t.join(timeout=MAX_DURATION_SEC + 30)

        # v2.8: Workers done (budget spent) but keep check_worker alive
        # to detect if domain was taken or is available
        if not caught.is_set() and not taken_by_other.is_set():
            remaining_wait = MAX_DURATION_SEC - (time.time() - attack_start)
            if remaining_wait > 0:
                debug(f"[ATTACK] {domain} - budget spent, waiting {remaining_wait:.0f}s for check_worker...")
                wait_end = time.time() + remaining_wait
                while time.time() < wait_end:
                    if caught.is_set() or taken_by_other.is_set() or stop_flag.is_set():
                        break
                    time.sleep(0.5)

        stop_flag.set()

        # v2.0: Restore normal socket timeout after attack
        for epp in self.epp_pool:
            try:
                epp.set_normal_timeout(30.0)
            except:
                pass

        # v3.1: Re-enable GC
        gc.enable()

        # ---- Results ----
        elapsed = time.time() - attack_start
        rate = stats["creates"] / max(1, elapsed)

        if caught.is_set():
            log(f"[ATTACK] 🏆 {domain} CAUGHT! {stats['creates']} creates in {elapsed:.1f}s ({rate:.1f}/s)")
            with self.lock:
                self.caught_domains.add(domain)
                if domain in self.domains_uk:
                    self.domains_uk.remove(domain)
            self._log_caught(domain, stats["creates"], elapsed)
            self._remove_from_ftp(domain)
            # v2.3: Record stats
            self.catch_stats.add({
                "domain": domain,
                "result": "CAUGHT",
                "drop_time": drop_time.isoformat(),
                "registered_at": datetime.now(timezone.utc).isoformat(),
                "catch_delay_sec": round(elapsed, 2),
                "creates": stats["creates"],
                "registrar_tag": NOMINET_TAG or EPP_USER,
                "date": datetime.now(timezone.utc).strftime("%Y-%m-%d %H:%M:%S")
            })
        elif taken_by_other.is_set():
            log(f"[ATTACK] ❌ {domain} - taken by competitor ({stats['creates']} creates in {elapsed:.1f}s, {rate:.1f}/s)")
            threading.Thread(target=self._check_who_caught, args=(domain, drop_time, stats["creates"], elapsed), daemon=True).start()
            with self.lock:
                self.attempted_domains.add(domain)
                if domain in self.domains_uk:
                    self.domains_uk.remove(domain)
            self._remove_from_ftp(domain)
        else:
            log(f"[ATTACK] ❌ {domain} - timed out ({stats['creates']} creates, {rate:.1f}/s)")
            with self.lock:
                self.attempted_domains.add(domain)
            # v2.3: Record timeout
            self.catch_stats.add({
                "domain": domain,
                "result": "TIMEOUT",
                "drop_time": drop_time.isoformat(),
                "creates": stats["creates"],
                "date": datetime.now(timezone.utc).strftime("%Y-%m-%d %H:%M:%S")
            })

        self.nominet.mark_attempted(domain)
        self.budget._save()
        self._xml_cache.pop(domain, None)

    def _check_who_caught(self, domain, drop_time=None, our_creates=0, our_elapsed=0):
        """בדוק דרך RDAP מי רשם את הדומיין + שמור סטטיסטיקה"""
        time.sleep(3)
        try:
            for attempt in range(3):
                resp = requests.get(
                    f"https://rdap.nominet.uk/uk/domain/{domain}",
                    timeout=10,
                    headers={"Accept": "application/rdap+json", "User-Agent": "UKCatcher/3.5"}
                )
                if resp.status_code == 404:
                    if attempt < 2:
                        time.sleep(5)
                        continue
                    log(f"[WHO-CAUGHT] {domain} - still not in RDAP")
                    # Record without RDAP data
                    self.catch_stats.add({
                        "domain": domain,
                        "result": "TAKEN",
                        "drop_time": drop_time.isoformat() if drop_time else None,
                        "creates": our_creates,
                        "our_elapsed": round(our_elapsed, 2),
                        "registrar_tag": "unknown",
                        "date": datetime.now(timezone.utc).strftime("%Y-%m-%d %H:%M:%S")
                    })
                    return

                if not resp.ok:
                    return

                data = resp.json()

                registrar_tag = None
                registrar_name = None
                for entity in data.get("entities", []):
                    roles = entity.get("roles", [])
                    if "registrar" in roles:
                        handle = entity.get("handle", "")
                        if handle:
                            registrar_tag = handle
                        vcard = entity.get("vcardArray", [])
                        if len(vcard) > 1:
                            for field in vcard[1]:
                                if field[0] == "fn":
                                    registrar_name = field[3]

                reg_date_str = None
                reg_date = None
                for ev in data.get("events", []):
                    if ev.get("eventAction") == "registration":
                        reg_date_str = ev.get("eventDate", "")

                # v2.3: Calculate catch delay = registration_time - drop_time
                catch_delay = None
                if reg_date_str and drop_time:
                    try:
                        reg_dt = datetime.fromisoformat(reg_date_str.replace('Z', '+00:00'))
                        catch_delay = (reg_dt - drop_time).total_seconds()
                    except:
                        pass

                statuses = data.get("status", [])

                if registrar_tag:
                    msg = f"[WHO-CAUGHT] 🔍 {domain} → TAG: {registrar_tag}"
                    if registrar_name:
                        msg += f" ({registrar_name})"
                    if reg_date_str:
                        msg += f" | registered: {reg_date_str}"
                    if catch_delay is not None:
                        msg += f" | delay: {catch_delay:.2f}s after drop"
                    log(msg)
                else:
                    log(f"[WHO-CAUGHT] {domain} - no registrar info found (status: {statuses})")

                # v2.3: Record statistics
                self.catch_stats.add({
                    "domain": domain,
                    "result": "TAKEN",
                    "drop_time": drop_time.isoformat() if drop_time else None,
                    "registered_at": reg_date_str,
                    "catch_delay_sec": round(catch_delay, 2) if catch_delay is not None else None,
                    "creates": our_creates,
                    "our_elapsed": round(our_elapsed, 2),
                    "registrar_tag": registrar_tag or "unknown",
                    "registrar_name": registrar_name,
                    "date": datetime.now(timezone.utc).strftime("%Y-%m-%d %H:%M:%S")
                })
                return

        except Exception as e:
            debug(f"[WHO-CAUGHT] {domain} error: {e}")

    def _log_caught(self, domain, attempts, elapsed):
        now = datetime.now(timezone.utc)
        tag = NOMINET_TAG or EPP_USER or "?"
        line = f"{domain} | {now.strftime('%d/%m/%Y %H:%M:%S')} UTC | TAG:{tag} | {attempts} attempts | {elapsed:.1f}s\n"
        try:
            with open(CAUGHT_FILE, "a", encoding="utf-8") as f:
                f.write(line)
        except Exception as e:
            log(f"[CAUGHT] Write error: {e}")

    def _remove_from_ftp(self, domain):
        d = domain.lower()
        try:
            tmp = os.path.join(tempfile.gettempdir(), f"ftp_rm_{int(time.time())}.txt")
            client = LiveSFTP(FTP_HOST, FTP_USER, FTP_PASS, FTP_PORT, REMOTE_PATH)
            if not client.download(tmp):
                client.close()
                return

            with open(tmp, "r", encoding="utf-8", errors="ignore") as f:
                lines = f.readlines()

            new_lines = [l for l in lines if l.strip().lower() != d]

            if len(new_lines) < len(lines):
                with open(tmp, "w", encoding="utf-8") as f:
                    f.writelines(new_lines)
                if client.upload(tmp):
                    log(f"[FTP] 🗑️ Removed {d}")

            client.close()
        except Exception as e:
            debug(f"[FTP-REMOVE] {d}: {e}")

    # ---- LOOPS ----

    def monitor_loop(self):
        log("[MONITOR] Started")
        while True:
            try:
                with self.lock:
                    domains = list(self.domains_uk)

                for domain in domains:
                    with self.lock:
                        if domain in self.caught_domains or domain in self.attempted_domains:
                            continue
                        if domain in self.attacking:
                            continue

                    secs = self.nominet.get_seconds_until_drop(domain)
                    if secs is None:
                        continue

                    if secs < -300:
                        continue

                    if secs <= PRE_WARM_SEC + 30:
                        drop_time = self.nominet.get_drop_time(domain)
                        if drop_time:
                            log(f"[MONITOR] 🎯 {domain} dropping in {secs:.1f}s")
                            t = threading.Thread(
                                target=self.attack_domain,
                                args=(domain, drop_time),
                                daemon=True
                            )
                            t.start()

            except Exception as e:
                log(f"[MONITOR] Error: {e}")

            time.sleep(1)

    def heartbeat_loop(self):
        while True:
            try:
                now = datetime.now(timezone.utc)

                with self.lock:
                    total = len(self.domains_uk)
                    caught = len(self.caught_domains)
                    attacking = len(self.attacking)
                    uk_list = list(self.domains_uk)

                budget = self.budget.remaining()
                budget_used = self.budget.used()
                next_free = self.budget.next_free_slot()
                epp_active = sum(1 for e in self.epp_pool if e.connected and e.logged_in)

                msg = f"\n{'=' * 55}\n"
                msg += f"  [{now.strftime('%H:%M:%S')} UTC] UK CATCHER v3.5 (Fire-All+FastDAC)\n"
                msg += f"  📋 UK: {total} | 🎯 Attacking: {attacking} | ✅ Caught: {caught}\n"
                budget_line = f"  💰 Budget: {budget}/{MAX_REQUESTS_DAY} (rolling 24h, {budget_used} used)"
                if budget == 0 and next_free > 0:
                    hours = int(next_free // 3600)
                    mins = int((next_free % 3600) // 60)
                    budget_line += f" | ⏳ next slot in {hours}h{mins}m"
                msg += budget_line + f" | 🔌 EPP: {epp_active}/{EPP_CONNECTIONS}\n"
                msg += f"  ⚡ Fire-All pipeline | Attack: T-{ATTACK_BEFORE_SEC}s\n"

                upcoming_24h = 0
                for d in uk_list:
                    secs = self.nominet.get_seconds_until_drop(d)
                    if secs is not None and -60 <= secs <= 86400:
                        upcoming_24h += 1
                if upcoming_24h > 0:
                    usable = int(budget * (1.0 - BUDGET_RESERVE_PCT))
                    per_dom = max(MIN_CREATES_PER_DOMAIN, usable // upcoming_24h)
                    msg += f"  📊 24h: {upcoming_24h} domains → ~{per_dom} creates/domain\n"
                else:
                    msg += f"  📊 24h: no drops scheduled\n"

                upcoming = []
                for d in uk_list:
                    secs = self.nominet.get_seconds_until_drop(d)
                    if secs is not None and secs > -60:
                        upcoming.append((d, secs))

                if upcoming:
                    upcoming.sort(key=lambda x: x[1])
                    msg += f"\n  🇬🇧 Upcoming:\n"
                    for d, s in upcoming[:10]:
                        if s > 3600:
                            msg += f"    {d}: {int(s//3600)}h {int((s%3600)//60)}m\n"
                        elif s > 0:
                            msg += f"    {d}: {int(s//60)}m {int(s%60)}s\n"
                        else:
                            msg += f"    {d}: 🔴 NOW\n"

                msg += f"{'=' * 55}"
                
                # v2.3: Show catch statistics
                stats_summary = self.catch_stats.get_summary()
                if stats_summary:
                    msg += f"\n{stats_summary}\n{'=' * 55}"
                
                print(msg)

            except Exception as e:
                log(f"[HEARTBEAT] Error: {e}")

            time.sleep(HEARTBEAT_SEC)

    def refresh_loop(self):
        while True:
            try:
                self.load_domains_from_ftp()
                self._cleanup_ftp()

                with self.lock:
                    domains = list(self.domains_uk)

                missing = [d for d in domains if not self.nominet.is_drop_time_known(d)]
                if missing:
                    log(f"[REFRESH] {len(missing)} missing drop times → RDAP...")
                    for d in missing[:10]:
                        self.nominet.fetch_single_domain_rdap(d)
                        time.sleep(2)

            except Exception as e:
                log(f"[REFRESH] Error: {e}")

            time.sleep(FTP_REFRESH_SEC)

    def _cleanup_ftp(self):
        with self.lock:
            to_remove = set(self.caught_domains) | set(self.attempted_domains)

        if not to_remove:
            return

        try:
            tmp = os.path.join(tempfile.gettempdir(), f"ftp_clean_{int(time.time())}.txt")
            client = LiveSFTP(FTP_HOST, FTP_USER, FTP_PASS, FTP_PORT, REMOTE_PATH)
            if not client.download(tmp):
                client.close()
                return

            with open(tmp, "r", encoding="utf-8", errors="ignore") as f:
                lines = f.readlines()

            original = len(lines)
            new_lines = [l for l in lines if l.strip().lower() not in to_remove]
            removed = original - len(new_lines)

            if removed > 0:
                with open(tmp, "w", encoding="utf-8") as f:
                    f.writelines(new_lines)
                if client.upload(tmp):
                    log(f"[CLEANUP] 🗑️ Removed {removed} domains from FTP")

            client.close()
        except Exception as e:
            log(f"[CLEANUP] Error: {e}")

    # ---- STARTUP ----

    def start(self):
        utc = datetime.now(timezone.utc)
        log(f"\n{'=' * 60}")
        log(f"  UK Domain Catcher v2.2 (Continuous)")
        log(f"  {utc.strftime('%Y-%m-%d %H:%M:%S')} UTC")
        log(f"{'=' * 60}")
        log(f"  EPP:      {EPP_HOST}:{EPP_PORT}")
        log(f"  TAG:      {NOMINET_TAG or EPP_USER or '⚠ NOT SET'}")
        log(f"  User:     {EPP_USER or '⚠ NOT SET'}")
        log(f"  Contact:  {REGISTRANT_ID or '⚠ NOT SET'}")
        log(f"  Budget:   {MAX_REQUESTS_DAY}/day (rolling 24h, {BUDGET_RESERVE_PCT*100:.0f}% reserve)")
        log(f"  Strategy: {EPP_CONNECTIONS} conns, fire-all pipeline")
        log(f"  Attack:   T-{ATTACK_BEFORE_SEC}s, stagger={STAGGER_MS}ms")
        log(f"  Min/dom:  {MIN_CREATES_PER_DOMAIN} | Pre-warm: {PRE_WARM_SEC}s")

        # v3.1: Pin process to first 2 CPU cores to avoid context switches
        try:
            os.sched_setaffinity(0, {0, 1})
            log(f"  CPU:      pinned to cores 0,1")
        except:
            log(f"  CPU:      affinity not available")

        log(f"{'=' * 60}\n")

        self.init_epp()

        log("[STARTUP] Loading Nominet drop list...")
        self.nominet.fetch_drop_list()
        self.nominet.schedule_refresh(interval_hours=DROPLIST_REFRESH_HOURS)

        self.load_domains_from_ftp()

        with self.lock:
            missing = [d for d in self.domains_uk if not self.nominet.is_drop_time_known(d)]
        if missing:
            log(f"[STARTUP] {len(missing)} missing drop times")
            threading.Thread(target=self._rdap_batch, args=(missing,), daemon=True).start()

        threading.Thread(target=self.monitor_loop, daemon=True).start()
        threading.Thread(target=self.heartbeat_loop, daemon=True).start()
        threading.Thread(target=self.refresh_loop, daemon=True).start()

        log("[STARTUP] ✅ Running!\n")

        try:
            while True:
                time.sleep(3600)
        except KeyboardInterrupt:
            log("\n🛑 Shutting down...")
            for epp in self.epp_pool:
                try:
                    epp.logout()
                except:
                    pass
            close_log()

    def _rdap_batch(self, domains):
        for d in domains:
            if not self.nominet.is_drop_time_known(d):
                self.nominet.fetch_single_domain_rdap(d)
            time.sleep(2)


def main():
    catcher = UKCatcher()
    catcher.start()


if __name__ == "__main__":
    main()