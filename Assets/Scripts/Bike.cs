using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
using UnityEngine.SocialPlatforms;

#if UNITY_IPHONE
using UnityEngine.iOS;
#endif

public partial class Bike : MonoBehaviour
{

    private GameObject maincamera, maincamera2, maincamera3, maincamera5, roads, menu, levels, rcolliders, bbike, bbike2, bbike3, circle, circle2, audiotog, musictog, oaudio, omusic, q, q2, q3, q4, quadropdown, logo, setting, settings, shopboard, play, shop, levreturn,
    marker, play2, dclock, background, scoreboard, sbitems, pointerSeconds, times, n0, n1, n2, n3, la, lb, lc, wheel2, gas, stop, towtruck, Terrain, Terrain2, tmirror, tmirror2, bikeback, tire, tire2, tire3, tire4, incar,
    fade, gosign, gosign2, gscore, goreason, gosettings, goreturn, gotext2, gosettings2, goreturn2, goreplay2, scorepanel, scoretext, scoretext2, starr, starr2, starr3, leveltext, level1, level2, level3, level4, level5, level6, level7, level8,
    level9, level10, level11, level12, level13, level14, level15, level16, level17, level18, level19, level20, LTerrain1, LTerrain2, LTerrain3, LTerrain4, LTerrain5, LTerrain6, LTerrain7, LTerrain8, LTerrain9, LTerrain10, LTerrain11, LTerrain12,
    LTerrain13, LTerrain14, LTerrain15, LTerrain16, LTerrain17, LTerrain18, LTerrain19, LTerrain20, nobreak, minspeeds, minspeedtext, slower, slowertext, owind, towtruck3, towtruck4, FTerrain, tire6, tire7, tire8, tire9, mine, mine2, bar,
    secscore, engine, creturn, wheel4, wheel5, tred, tyellow, tgreen, tred2, tyellow2, tgreen2, startcamera, tpause, paused, dwheel, dwheel2, dwheel3, dwheel4, artext, artext2, TDL, TDL2, TDL3, TDL4, zometfinal, loading, loading2,
    levelsv, openingvid, beggining, pstar, pstar2, pstar3, pstart, pstart2, pstart3, upsideo, speedo, speedoarrow, minedetect, mines, lv1sign, lv1sign2, lv1sign3, sign, sign3, minenow, devtext, nextlevels, prevlevels,
    freecoins, videocoins, treasure, fct2, popupcoin, popupni, pmessagec, freecoinsp, coinc, coinc2, coinc3, coinc4, coinc5, coinc6, coinc7, coinc8, cointext, keycam, minecam, extratimepic, Purchase, purchasem, PurchaseF, magicwranchgo, epback, mineback,
    shopinfo, sititle, simessage, youhaveedit, sinum, shockwhite, slowmotiono, slowback, buymenu, buytitle, buyprice, buyamount, slownum, canvas2, opencamera, texttest, supdated, credits, connectmsg;

    private GameObject[] allzomet;
    private Renderer[] allzometr;

    private int wi, he, start, lasto = 3, bridge, lastdir, tlevels = 29, place, zcount, passed, up, down, right, left, set, res, quality, alevels, lastr, arc, arc2, inwheel, isgo, instop, fading, rightpanel, nostop, minspeed, maxspeed, exspeed, exspeednow, mson, notice, wind, winddir, windnum, mode, wlight, scene, levid, active, lowfps, first, load, startingvid, mineon, upside, offsett, engineon, mplayed, started, wheelupside, tr, tl, gm,
    fcs, hour, minute, second, day, month, year, cotimer, yescoins, fcs2, hour2, minute2, second2, day2, month2, year2, cotimer2, yescoins2, atshop, antimine, keypass, extracount, slowmotion, antiminecount, keypasscount, timeused, slowused, ismine, amused, slow, icused, camloading, testing, settesting, psigned, signin, firsttime, testing2, gplay;

    private float lastx, lasty, lastw, lasth, music, setz, msecs, wheelspeed = 0.5f, tspeed = 0.08f, mirw, mirw2, bbw, miry, miry2, bby, windcount, mtx = 0, mty = 92, ft6, ft7, ft8, ft9, ftx, ftx2, ftx3, ftx4, ftz, ftz2, ftz3, ftz4, mspeed = 20, score2, lcount, levtimer, pause, fpscounter, starpos, enginevol, enginevol2, finishplaying, starsoundp, starsoundp2, starsoundp3, spinf, finished, dev, devcount, fsc, fsc2, fcc;
    public int level, gameover;
    public float speed = 1, bspeed = 1, count = 1, dcount, seconds, gospeed = 0, audio = 0;
    private Vector3 offset, startpos, startang, bbikestart;
    private string[] allscores, allstars, starst, ctime, ctime2, allstats;
    private int[] ar, adir, played;
    private bool isSaving;
    private Sprite tx0, tx1, tx2, tf0, tf1, tf2, tf0b, tf1b, tf2b, ot0, ot2, to0, to1, zto1, zto2, border, borders, borders2, cb1, cb1l, cb1r, cb2, cb2l, cb2r, sand;
    private RawImage pattern;
    private Texture locked, unlocked, star, star2, speedo2, minspeed40, minspeed50, minspeed60, minspeed80, minspeed90, minspeed100, minspeed120, redspeed10, redspeed30, redspeed40;
    private float alpha = 1.0f, alpha2 = 1.0f, alpha3 = 1.0f, alpha4 = 0f, xyzsize = 0, alphaf = 0f, speedrot = 14.3f, blpha, blpha2, ccf, ccf2, ccf3, ccf4, starttimer, starttimer2;
    private string stars;
    public float clockSpeed = 1.0f;
    private List<GameObject> alist;
    private Vector2 offsetMax;
    private int buyid;
    private float tused, sused, slowcount;
    private AudioSource backengine, gasengine, decengine, crash, finishsound, starsound, starsound2, starsound3, mainmusic, click;

}
