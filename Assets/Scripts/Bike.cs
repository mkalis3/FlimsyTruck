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

public class Bike : MonoBehaviour {


    GameObject maincamera, maincamera2, maincamera3, maincamera5, roads, menu, levels, rcolliders, bbike, bbike2, bbike3, circle, circle2, audiotog, musictog, oaudio, omusic, q, q2, q3, q4, quadropdown, logo, setting, settings, shopboard, play, shop, levreturn,
    marker, play2, dclock, background, scoreboard, sbitems, pointerSeconds, times, n0, n1, n2, n3, la, lb, lc, wheel2, gas, stop, towtruck, Terrain, Terrain2, tmirror, tmirror2, bikeback, tire, tire2, tire3, tire4, incar,
    fade, gosign, gosign2, gscore, goreason, gosettings, goreturn, gotext2, gosettings2, goreturn2, goreplay2, scorepanel, scoretext, scoretext2, starr, starr2, starr3, leveltext, level1, level2, level3, level4, level5, level6, level7, level8,
    level9, level10, level11, level12, level13, level14, level15, level16, level17, level18, level19, level20, LTerrain1, LTerrain2, LTerrain3, LTerrain4, LTerrain5, LTerrain6, LTerrain7, LTerrain8, LTerrain9, LTerrain10, LTerrain11, LTerrain12,
    LTerrain13, LTerrain14, LTerrain15, LTerrain16, LTerrain17, LTerrain18, LTerrain19, LTerrain20, nobreak, minspeeds, minspeedtext, slower, slowertext, owind, towtruck3, towtruck4, FTerrain, tire6, tire7, tire8, tire9, mine, mine2, bar,
    secscore, engine, creturn, wheel4, wheel5, tred, tyellow, tgreen, tred2, tyellow2, tgreen2, startcamera, tpause, paused, dwheel, dwheel2, dwheel3, dwheel4, artext, artext2, TDL, TDL2, TDL3, TDL4, zometfinal, loading, loading2,
    levelsv, openingvid, beggining, pstar, pstar2, pstar3, pstart, pstart2, pstart3, upsideo, speedo, speedoarrow, minedetect, mines, lv1sign, lv1sign2, lv1sign3, sign, sign3, minenow, devtext, nextlevels, prevlevels,
    freecoins, videocoins, treasure, fct2, popupcoin, popupni, pmessagec, freecoinsp, coinc, coinc2, coinc3, coinc4, coinc5, coinc6, coinc7, coinc8, cointext, keycam, minecam, extratimepic, Purchase, purchasem, PurchaseF, magicwranchgo, epback, mineback,
    shopinfo, sititle, simessage, youhaveedit, sinum, shockwhite, slowmotiono, slowback, buymenu, buytitle, buyprice, buyamount, slownum, canvas2, opencamera, texttest, supdated, credits, connectmsg;

    GameObject[] allzomet;
    Renderer[] allzometr;

    int wi, he, start, lasto = 3, bridge, lastdir, tlevels = 29, place, zcount, passed, up, down, right, left, set, res, quality, alevels, lastr, arc, arc2, inwheel, isgo, instop, fading, rightpanel, nostop, minspeed, maxspeed, exspeed, exspeednow, mson, notice, wind, winddir, windnum, mode, wlight, scene, levid, active, lowfps, first, load, startingvid, mineon, upside, offsett, engineon, mplayed, started, wheelupside, tr, tl, gm,
    fcs, hour, minute, second, day, month, year, cotimer, yescoins, fcs2, hour2, minute2, second2, day2, month2, year2, cotimer2, yescoins2, atshop, antimine, keypass, extracount, slowmotion, antiminecount, keypasscount, timeused, slowused, ismine, amused, slow, icused, camloading, testing, settesting, psigned, signin, firsttime, testing2, gplay;

    float lastx, lasty, lastw, lasth, music, setz, msecs, wheelspeed = 0.5f, tspeed = 0.08f, mirw, mirw2, bbw, miry, miry2, bby, windcount, mtx = 0, mty = 92, ft6, ft7, ft8, ft9, ftx, ftx2, ftx3, ftx4, ftz, ftz2, ftz3, ftz4, mspeed = 20, score2, lcount, levtimer, pause, fpscounter, starpos, enginevol, enginevol2, finishplaying, starsoundp, starsoundp2, starsoundp3, spinf, finished, dev, devcount, fsc, fsc2, fcc;
    public int level, gameover;
    public float speed = 1, bspeed = 1, count = 1, dcount, seconds, gospeed = 0, audio = 0;
    private Vector3 offset, startpos, startang, bbikestart;
    string[] allscores, allstars, starst, ctime, ctime2, allstats;
    int[] ar, adir, played;
    bool isSaving;
    Sprite tx0, tx1, tx2, tf0, tf1, tf2, tf0b, tf1b, tf2b, ot0, ot2, to0, to1, zto1, zto2, border, borders, borders2, cb1, cb1l, cb1r, cb2, cb2l, cb2r, sand;
    RawImage pattern;
    Texture locked, unlocked, star, star2, speedo2, minspeed40, minspeed50, minspeed60, minspeed80, minspeed90, minspeed100, minspeed120, redspeed10, redspeed30, redspeed40;
    float alpha = 1.0f, alpha2 = 1.0f, alpha3 = 1.0f, alpha4 = 0f, xyzsize = 0, alphaf = 0f, speedrot = 14.3f, blpha, blpha2, ccf, ccf2, ccf3, ccf4, starttimer, starttimer2;
    string stars;
    public float clockSpeed = 1.0f;
    List<GameObject> alist;
    Vector2 offsetMax;
    int buyid;
    float tused, sused, slowcount;
    AudioSource backengine, gasengine, decengine, crash, finishsound, starsound, starsound2, starsound3, mainmusic, click;

    // Use this for initialization
    void Start() {
        dev = 0;

        canvas2 = GameObject.Find("Canvas");
        hour = PlayerPrefs.GetInt("hour");
        minute = PlayerPrefs.GetInt("minute");
        second = PlayerPrefs.GetInt("second");
        day = PlayerPrefs.GetInt("day");
        month = PlayerPrefs.GetInt("month");
        year = PlayerPrefs.GetInt("year");
        hour2 = PlayerPrefs.GetInt("hour2");
        minute2 = PlayerPrefs.GetInt("minute2");
        second2 = PlayerPrefs.GetInt("second2");
        day2 = PlayerPrefs.GetInt("day2");
        month2 = PlayerPrefs.GetInt("month2");
        year2 = PlayerPrefs.GetInt("year2");

        artext2 = GameObject.Find("artext2");

        wi = Screen.width;
        he = Screen.height;
        maincamera = GameObject.Find("Main Camera");
        maincamera2 = GameObject.Find("Main Camera2");
        maincamera3 = GameObject.Find("Main Camera3");
        maincamera5 = GameObject.Find("Main Camera5");
        startcamera = GameObject.Find("Start Camera");
        //bike = GameObject.Find ("bike");
        roads = GameObject.Find("roads");
        menu = GameObject.Find("menu");
        levels = GameObject.Find("levels");
        rcolliders = GameObject.Find("rcolliders");
        bbike = GameObject.Find("bbike");
        bbike2 = GameObject.Find("bbike2");
        bbike3 = GameObject.Find("bbike3");
        circle = GameObject.Find("circle");
        circle2 = GameObject.Find("circle2");
        audiotog = GameObject.Find("audiotog");
        musictog = GameObject.Find("musictog");
        oaudio = GameObject.Find("audio");
        omusic = GameObject.Find("music");
        q = GameObject.Find("q");
        q2 = GameObject.Find("q2");
        q3 = GameObject.Find("q3");
        q4 = GameObject.Find("q4");
        quadropdown = GameObject.Find("quadropdown");
        logo = GameObject.Find("logo");
        setting = GameObject.Find("setting");
        settings = GameObject.Find("settings");
        shopboard = GameObject.Find("shopboard");
        play = GameObject.Find("play");
        shop = GameObject.Find("shop");
        levreturn = GameObject.Find("levreturn");
        marker = GameObject.Find("marker");
        play2 = GameObject.Find("play2");
        dclock = GameObject.Find("dclock");
        background = GameObject.Find("background");
        scoreboard = GameObject.Find("scoreboard");
        sbitems = GameObject.Find("sbitems");
        pointerSeconds = GameObject.Find("rotation_axis_pointer_seconds");
        times = GameObject.Find("times");
        n0 = GameObject.Find("n0");
        n1 = GameObject.Find("n1");
        n2 = GameObject.Find("n2");
        n3 = GameObject.Find("n3");
        wheel2 = GameObject.Find("3dwheel2");
        wheel4 = GameObject.Find("3dwheel4");
        wheel5 = GameObject.Find("2dwheel3");
        gas = GameObject.Find("gas");
        stop = GameObject.Find("stop");
        towtruck = GameObject.Find("towtruck");
        Terrain = GameObject.Find("Terrain");
        Terrain2 = GameObject.Find("Terrain2s");
        tmirror = GameObject.Find("tmirror");
        tmirror2 = GameObject.Find("tmirror2");
        bikeback = GameObject.Find("bikeback");
        tire = GameObject.Find("tire");
        tire2 = GameObject.Find("tire2");
        tire3 = GameObject.Find("tire3");
        tire4 = GameObject.Find("tire4");
        incar = GameObject.Find("incar");
        fade = GameObject.Find("fade");
        gosign = GameObject.Find("gosign");
        gosign2 = GameObject.Find("gosign2");
        gscore = GameObject.Find("gscore");
        goreason = GameObject.Find("goreason");
        gosettings = GameObject.Find("gosettings");
        goreturn = GameObject.Find("goreturn");
        gotext2 = GameObject.Find("gotext2");
        gosettings2 = GameObject.Find("gosettings2");
        goreturn2 = GameObject.Find("goreturn2");
        goreplay2 = GameObject.Find("goreplay");
        scorepanel = GameObject.Find("scorepanel");
        scoretext = GameObject.Find("scoretext");
        scoretext2 = GameObject.Find("scoretext2");
        starr = GameObject.Find("starr");
        starr2 = GameObject.Find("starr2");
        starr3 = GameObject.Find("starr3");
        leveltext = GameObject.Find("leveltext");
        level1 = GameObject.Find("level1final");
        level2 = GameObject.Find("level2");
        level3 = GameObject.Find("level3");
        level4 = GameObject.Find("level4");
        level5 = GameObject.Find("level5");
        level6 = GameObject.Find("level6");
        level7 = GameObject.Find("level7");
        level8 = GameObject.Find("level8");
        level9 = GameObject.Find("level9");
        level10 = GameObject.Find("level10");
        level11 = GameObject.Find("level11");
        level12 = GameObject.Find("level12");
        level13 = GameObject.Find("level13");
        level14 = GameObject.Find("level14");
        level15 = GameObject.Find("level15");
        level16 = GameObject.Find("level16");
        level17 = GameObject.Find("level17");
        level18 = GameObject.Find("level18");
        level19 = GameObject.Find("level19");
        level20 = GameObject.Find("level20");
        //LTerrain1 = GameObject.Find("Terrain1");
        LTerrain2 = GameObject.Find("Terrain2");
        LTerrain3 = GameObject.Find("Terrain3");
        LTerrain4 = GameObject.Find("Terrain4");
        LTerrain5 = GameObject.Find("Terrain5");
        LTerrain6 = GameObject.Find("Terrain6");
        LTerrain7 = GameObject.Find("Terrain7");
        LTerrain8 = GameObject.Find("Terrain8");
        LTerrain9 = GameObject.Find("Terrain9");
        LTerrain10 = GameObject.Find("Terrain10");
        LTerrain11 = GameObject.Find("Terrain11");
        LTerrain12 = GameObject.Find("Terrain12");
        LTerrain13 = GameObject.Find("Terrain13");
        LTerrain14 = GameObject.Find("Terrain14");
        LTerrain15 = GameObject.Find("Terrain15");
        LTerrain16 = GameObject.Find("Terrain16");
        LTerrain17 = GameObject.Find("Terrain17");
        LTerrain18 = GameObject.Find("Terrain18");
        LTerrain19 = GameObject.Find("Terrain19");
        LTerrain20 = GameObject.Find("Terrain20");
        nobreak = GameObject.Find("nobreak");
        minspeeds = GameObject.Find("minspeed");
        minspeedtext = GameObject.Find("minspeedtext");
        slower = GameObject.Find("slower");
        slowertext = GameObject.Find("slowertext");
        owind = GameObject.Find("wind");
        towtruck3 = GameObject.Find("towtruck3");
        towtruck4 = GameObject.Find("towtruck4");
        FTerrain = GameObject.Find("FTerrain");
        tire6 = GameObject.Find("tire6");
        tire7 = GameObject.Find("tire7");
        tire8 = GameObject.Find("tire8");
        tire9 = GameObject.Find("tire9");
        mine = GameObject.Find("mine");
        mine2 = GameObject.Find("mine2");
        bar = GameObject.Find("bar");
        secscore = GameObject.Find("secondscore");
        engine = GameObject.Find("engine");
        creturn = GameObject.Find("creturn");
        tpause = GameObject.Find("tpause");
        paused = GameObject.Find("paused");
        dwheel = GameObject.Find("2dwheel");
        dwheel2 = GameObject.Find("2dwheel2");
        dwheel3 = GameObject.Find("2dwheel3");
        dwheel4 = GameObject.Find("2dwheel4");
        artext = GameObject.Find("artext");
        TDL = GameObject.Find("TDL");
        TDL2 = GameObject.Find("TDL2");
        TDL3 = GameObject.Find("TDL3");
        TDL4 = GameObject.Find("TDL4");
        loading = GameObject.Find("loading");
        levelsv = GameObject.Find("levelsv");
        openingvid = GameObject.Find("openingvid");
        beggining = GameObject.Find("beggining");
        pstar = GameObject.Find("pstar");
        pstar2 = GameObject.Find("pstar2");
        pstar3 = GameObject.Find("pstar3");
        pstart = GameObject.Find("pstart");
        pstart2 = GameObject.Find("pstart2");
        pstart3 = GameObject.Find("pstart3");
        upsideo = GameObject.Find("upsideo");
        speedo = GameObject.Find("speedo");
        speedoarrow = GameObject.Find("speedoarrow");
        mines = GameObject.Find("mines");
        lv1sign = GameObject.Find("lv1sign");
        lv1sign2 = GameObject.Find("lv1sign2");
        lv1sign3 = GameObject.Find("lv1sign3");
        devtext = GameObject.Find("devtext");
        nextlevels = GameObject.Find("nextlevels");
        prevlevels = GameObject.Find("prevlevels");
        freecoins = GameObject.Find("freecoins");
        videocoins = GameObject.Find("videocoins");
        treasure = GameObject.Find("treasure");
        fct2 = GameObject.Find("fct2");
        popupcoin = GameObject.Find("popupcoins");
        popupni = GameObject.Find("popupni");
        pmessagec = GameObject.Find("pmessagec");
        freecoinsp = GameObject.Find("freecoinsp");
        coinc = GameObject.Find("coinc");
        coinc2 = GameObject.Find("coinc2");
        coinc3 = GameObject.Find("coinc3");
        coinc4 = GameObject.Find("coinc4");
        coinc5 = GameObject.Find("coinc5");
        coinc6 = GameObject.Find("coinc6");
        coinc7 = GameObject.Find("coinc7");
        coinc8 = GameObject.Find("coinc8");
        cointext = GameObject.Find("cointext");
        keycam = GameObject.Find("keycam");
        minecam = GameObject.Find("minecam");
        extratimepic = GameObject.Find("extratimepic");
        Purchase = GameObject.Find("Purchase");
        purchasem = GameObject.Find("purchasem");
        PurchaseF = GameObject.Find("PurchaseF");
        magicwranchgo = GameObject.Find("magicwranchgo");
        epback = GameObject.Find("epback");
        mineback = GameObject.Find("mineback");
        shopinfo = GameObject.Find("shopinfo");
        sititle = GameObject.Find("sititle");
        simessage = GameObject.Find("simessage");
        youhaveedit = GameObject.Find("youhaveedit");
        sinum = GameObject.Find("sinum");
        shockwhite = GameObject.Find("shockwhite");
        slowmotiono = GameObject.Find("slowmotion");
        slowback = GameObject.Find("slowback");
        buymenu = GameObject.Find("buymenu");
        buytitle = GameObject.Find("buytitle");
        buyprice = GameObject.Find("buyprice");
        buyamount = GameObject.Find("buyamount");
        slownum = GameObject.Find("slownum");
        opencamera = GameObject.Find("OpenCamera");
        texttest = GameObject.Find("texttest");
        supdated = GameObject.Find("supdated");
        credits = GameObject.Find("credits");
        connectmsg = GameObject.Find("connectmsg");

        first = PlayerPrefs.GetInt("first");
        if (first == 0)
        {
            PlayerPrefs.SetInt("first", 1);
            PlayerPrefs.SetFloat("music", 1);
            PlayerPrefs.SetFloat("audio", 1);
            PlayerPrefs.SetInt("coins", 50);
            testing = 1;
            firsttime = 1;
        }

        audio = PlayerPrefs.GetFloat("audio");
        gplay = PlayerPrefs.GetInt("gplay");

        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            if (audio == 1)
            {
                opencamera.GetComponent<UnityEngine.Video.VideoPlayer>().SetDirectAudioVolume(0, 1);
            }

        }
        if (offsett == 0)
        {
            startpos = startcamera.transform.localPosition;
            startang = startcamera.transform.localEulerAngles;
            offsetMax = new Vector2(scorepanel.GetComponent<RectTransform>().offsetMax.x, scorepanel.GetComponent<RectTransform>().offsetMax.y);
            offsett = 1;
        }

        if (dev == 1)
        {
            artext.transform.localScale = new Vector3(1, 1, 1);
        }
        //FixCamera169(startcamera.GetComponent<Camera>());

        //CombineMeshes(GameObject.Find("chrome_blinker"));

        if (SceneManager.GetActiveScene().name.Equals("third"))
        {
            /*TDL.GetComponent<Light>().enabled = true;
            TDL2.GetComponent<Light>().enabled = true;
            TDL3.GetComponent<Light>().enabled = true;
            TDL4.GetComponent<Light>().enabled = true;*/
            TDL2.GetComponent<Light>().enabled = true;
            TDL3.GetComponent<Light>().enabled = true;
            tred = GameObject.Find("tred");
            tyellow = GameObject.Find("tgreen");
            tgreen = GameObject.Find("secondscore");
            tred2 = GameObject.Find("tred2");
            tyellow2 = GameObject.Find("tyellow2");
            tgreen2 = GameObject.Find("tgreen2");


            allzomet = GameObject.FindGameObjectsWithTag("zomet");
            allzometr = new Renderer[allzomet.Length];


            for (int i = 0; i < allzometr.Length; i++)
            {
                allzometr[i] = allzomet[i].GetComponent<Renderer>();
            }

        }
        //bike.AddComponent<Detect> ();

        locked = Resources.Load("locked", typeof(Texture2D)) as Texture;
        unlocked = Resources.Load("unlocked", typeof(Texture2D)) as Texture;
        star = Resources.Load("star", typeof(Texture2D)) as Texture;
        star2 = Resources.Load("star2", typeof(Texture2D)) as Texture;
        speedo2 = Resources.Load("speedo2", typeof(Texture2D)) as Texture;
        minspeed40 = Resources.Load("speed40", typeof(Texture2D)) as Texture;
        minspeed50 = Resources.Load("speed50", typeof(Texture2D)) as Texture;
        minspeed60 = Resources.Load("speed60", typeof(Texture2D)) as Texture;
        minspeed80 = Resources.Load("speed80", typeof(Texture2D)) as Texture;
        minspeed90 = Resources.Load("speed90", typeof(Texture2D)) as Texture;
        minspeed100 = Resources.Load("speed100", typeof(Texture2D)) as Texture;
        minspeed120 = Resources.Load("speed120", typeof(Texture2D)) as Texture;
        redspeed10 = Resources.Load("redspeed10", typeof(Texture2D)) as Texture;
        redspeed30 = Resources.Load("redspeed30", typeof(Texture2D)) as Texture;
        redspeed40 = Resources.Load("redspeed40", typeof(Texture2D)) as Texture;

        /*Reset
        PlayerPrefs.SetInt("passed",0);
        PlayerPrefs.SetString("scores","");
        PlayerPrefs.SetString("stars","");*/

        passed = PlayerPrefs.GetInt("passed");
        string scores = PlayerPrefs.GetString("scores");
        if (scores == "") {
            scores = "0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0";
            allscores = scores.Split(":"[0]);
            PlayerPrefs.SetString("scores", scores);
        }
        allscores = scores.Split(":"[0]);
        stars = PlayerPrefs.GetString("stars");
        if (stars == "") {
            stars = "0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0";
            allstars = stars.Split(":"[0]);
            PlayerPrefs.SetString("stars", stars);
        }
        allstars = stars.Split(":"[0]);
        starst = new string[3];
        ar = new int[50];
        adir = new int[50];
        played = new int[100];
        msecs = 0.0f;
        seconds = 0;
        alist = new List<GameObject>();

        starttimer = 10;
        starttimer2 = 3;

        quality = PlayerPrefs.GetInt("quality");
        res = PlayerPrefs.GetInt("res");

        if (testing == 0)
        {
            if (quality == 0)
            {
                QualitySettings.SetQualityLevel(0);
                quadropdown.GetComponent<Dropdown>().value = 0;
                Screen.SetResolution(640, 360, true);
            }
            else if (quality == 1)
            {
                QualitySettings.SetQualityLevel(1);
                quadropdown.GetComponent<Dropdown>().value = 1;
                Screen.SetResolution(800, 450, true);
            }
            else if (quality == 2)
            {
                QualitySettings.SetQualityLevel(2);
                quadropdown.GetComponent<Dropdown>().value = 2;
                Screen.SetResolution(960, 540, true);
            }
            else if (quality == 3)
            {
                QualitySettings.SetQualityLevel(3);
                quadropdown.GetComponent<Dropdown>().value = 3;
                Screen.SetResolution(1280, 720, true);
            }
            settesting = 0;
        }

        /*if (res == 0)
        {
            res = 2;
            Screen.SetResolution(1280, 720, true);
            PlayerPrefs.SetInt("res", res);
            resdropdown.GetComponent<Dropdown>().value = 1;
        }
        else if (res == 1)
        {
            Screen.SetResolution(640, 360, true);
            resdropdown.GetComponent<Dropdown>().value = 0;
        }
        else if (res == 2)
        {
            Screen.SetResolution(1280, 720, true);
            resdropdown.GetComponent<Dropdown>().value = 1;
        }
        else if (res == 3)
        {
            Screen.SetResolution(1920, 1080, true);
            resdropdown.GetComponent<Dropdown>().value = 2;
        }*/
        //Screen.SetResolution((int)Screen.width, (int)Screen.height, true);

        wlight = 1;



        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;

        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            if (startingvid == 0)
            {
                audio = PlayerPrefs.GetFloat("audio");
                if (audio == 0)
                {
                    opencamera.GetComponent<UnityEngine.Video.VideoPlayer>().SetDirectAudioMute(0, true);
                }
                opencamera.GetComponent<UnityEngine.Video.VideoPlayer>().Play();
                opencamera.GetComponent<UnityEngine.Video.VideoPlayer>().loopPointReached += EndReached;
                startingvid = 1;
            }
        }
        else
        {
            StartGame();
        }


        bbikepos();
        bbike.transform.localPosition = bbikestart;


    }

    void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        vp.enabled = false;
        StartGame();
    }

    void StartGame()
    {
        if (started == 0)
        {
#if UNITY_ANDROID
            if (firsttime == 1)
            {
                ConnectPopupShow();
            }
            else
            {
                if(gplay == 1)
                {
                    ConnectPlay();
                }
            }
#endif


            if (testing == 1)
            {
                testing = 0;
                if (settesting == 0)
                {
                    settesting = 1;
                    if (Application.platform == RuntimePlatform.Android)
                    {
                        if (quality == 0)
                        {
                            QualitySettings.SetQualityLevel(0);
                            quadropdown.GetComponent<Dropdown>().value = 0;
                            Screen.SetResolution(640, 360, true);
                        }
                        else if (quality == 1)
                        {
                            QualitySettings.SetQualityLevel(1);
                            quadropdown.GetComponent<Dropdown>().value = 1;
                            Screen.SetResolution(800, 450, true);
                        }
                        else if (quality == 2)
                        {
                            QualitySettings.SetQualityLevel(2);
                            quadropdown.GetComponent<Dropdown>().value = 2;
                            Screen.SetResolution(960, 540, true);
                        }
                        else if (quality == 3)
                        {
                            QualitySettings.SetQualityLevel(3);
                            quadropdown.GetComponent<Dropdown>().value = 3;
                            Screen.SetResolution(1280, 720, true);
                        }
                    }
                    else if (Application.platform == RuntimePlatform.IPhonePlayer)
                    {
#if UNITY_IPHONE
                        if (Device.generation == DeviceGeneration.iPhone4S || Device.generation == DeviceGeneration.iPhone5C || Device.generation == DeviceGeneration.iPad1Gen || Device.generation == DeviceGeneration.iPad2Gen || Device.generation == DeviceGeneration.iPadMini1Gen)
                        {
                            QualitySettings.SetQualityLevel(1);
                            quadropdown.GetComponent<Dropdown>().value = 1;
                            Screen.SetResolution(800, 450, true);
                            quality = 1;
                        }
                        else if (Device.generation == DeviceGeneration.iPhone5 || Device.generation == DeviceGeneration.iPhone5S || Device.generation == DeviceGeneration.iPhone6)
                        {
                            QualitySettings.SetQualityLevel(2);
                            quadropdown.GetComponent<Dropdown>().value = 2;
                            Screen.SetResolution(960, 540, true);
                            quality = 2;
                        }
                        else
                        {
                            QualitySettings.SetQualityLevel(3);
                            quadropdown.GetComponent<Dropdown>().value = 3;
                            Screen.SetResolution(1280, 720, true);
                            quality = 3;
                        }
                        PlayerPrefs.SetInt("quality", quality);
#endif
                    }
                }
            }

            startcamera.GetComponent<Camera>().enabled = true;
            opencamera.GetComponent<Camera>().enabled = false;
            camloading = 1;
            Aspect();

            beggining.transform.localScale = new Vector3(0, 0, 0);
            menu.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);

            if (mplayed == 0)
            {
                mainmusic = GameObject.Find("mainmusic").GetComponent<AudioSource>();
                click = GameObject.Find("click").GetComponent<AudioSource>();
                music = PlayerPrefs.GetFloat("music");
                audio = PlayerPrefs.GetFloat("audio");
                if (music == 1)
                {
                    mainmusic.volume = 0.12f;
                    mainmusic.Play();
                    Texture musicon = Resources.Load("musicon", typeof(Texture2D)) as Texture;
                    RawImage img = (RawImage)omusic.GetComponent<RawImage>();
                    img.texture = musicon;
                    musicon = Resources.Load("toggleon", typeof(Texture2D)) as Texture;
                    img = (RawImage)musictog.GetComponent<RawImage>();
                    img.texture = musicon;
                    mplayed = 1;
                }
                else
                {
                    Texture musicoff = Resources.Load("musicoff", typeof(Texture2D)) as Texture;
                    RawImage img = (RawImage)omusic.GetComponent<RawImage>();
                    img.texture = musicoff;
                    musicoff = Resources.Load("toggleoff", typeof(Texture2D)) as Texture;
                    img = (RawImage)musictog.GetComponent<RawImage>();
                    img.texture = musicoff;
                }

                if (audio == 1)
                {
                    Texture audioon = Resources.Load("audioon", typeof(Texture2D)) as Texture;
                    RawImage img = (RawImage)oaudio.GetComponent<RawImage>();
                    img.texture = audioon;
                    audioon = Resources.Load("toggleon", typeof(Texture2D)) as Texture;
                    img = (RawImage)audiotog.GetComponent<RawImage>();
                    img.texture = audioon;
                }
                else
                {
                    Texture audioff = Resources.Load("audiooff", typeof(Texture2D)) as Texture;
                    RawImage img = (RawImage)oaudio.GetComponent<RawImage>();
                    img.texture = audioff;
                    audioff = Resources.Load("toggleoff", typeof(Texture2D)) as Texture;
                    img = (RawImage)audiotog.GetComponent<RawImage>();
                    img.texture = audioff;
                }
            }
            ccf = 62.86f;
            ccf2 = 84.6f;
            ccf3 = 73.1f;
            ccf4 = 97.7f;
            StartCoroutine("getTime");
            StartCoroutine("getTime2");
            started = 1;
        }
    }

    void FixCamera169(Camera camera)
    {
        float targetaspect = 16.0f / 9.0f;
        float windowaspect = (float)Screen.width / (float)Screen.height;
        float scaleheight = windowaspect / targetaspect;

        if (scaleheight < 1.0f)
        {
            Rect rect = camera.rect;

            rect.width = 1.0f;
            rect.height = scaleheight;
            rect.x = 0;
            rect.y = (1.0f - scaleheight) / 2.0f;

            camera.rect = rect;
        }
        else // add pillarbox
        {
            float scalewidth = 1.0f / scaleheight;

            Rect rect = camera.rect;

            rect.width = scalewidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scalewidth) / 2.0f;
            rect.y = 0;

            camera.rect = rect;
        }
    }

    bool isUp()
    {
        int cy = (int)(circle2.transform.localPosition.y);
        if (cy > 3) {
            return true;
        }
        return false;
    }


    bool isDown()
    {
        int cy = (int)(circle2.transform.localPosition.y);
        if (cy < -3) {
            return true;
        }
        return false;
    }

    bool isRight()
    {
        int cx = (int)(circle2.transform.localPosition.x);
        if (cx > 20) {
            return true;
        }
        return false;
    }

    bool isLeft()
    {
        int cx = (int)(circle2.transform.localPosition.x);
        if (cx < -20) {
            return true;
        }
        return false;
    }

    /* public void ChangeRes()
     {
         int rvalue = resdropdown.GetComponent<Dropdown>().value;
         //print("res " + rvalue);

         if (rvalue == 0) {
             Screen.SetResolution(640, 360, true);
             res = 1;
         }
         else if (rvalue == 1) {
             Screen.SetResolution(1280, 720, true);
             res = 2;
         }
         else if (rvalue == 2) {
             Screen.SetResolution(1920, 1080, true);
             res = 3;
         }
         PlayerPrefs.SetInt("res", res);
     }*/

    public void ChangeQuality()
    {
        int rvalue = quadropdown.GetComponent<Dropdown>().value;

        if (rvalue == 0)
        {
            QualitySettings.SetQualityLevel(0);
            quadropdown.GetComponent<Dropdown>().value = 0;
            Screen.SetResolution(640, 360, true);
            quality = 0;
        }
        else if (rvalue == 1)
        {
            QualitySettings.SetQualityLevel(1);
            quadropdown.GetComponent<Dropdown>().value = 1;
            Screen.SetResolution(800, 450, true);
            quality = 1;
        }
        else if (rvalue == 2)
        {
            QualitySettings.SetQualityLevel(2);
            quadropdown.GetComponent<Dropdown>().value = 2;
            Screen.SetResolution(960, 540, true);
            quality = 2;
        }
        else if (rvalue == 3)
        {
            QualitySettings.SetQualityLevel(3);
            quadropdown.GetComponent<Dropdown>().value = 3;
            Screen.SetResolution(1280, 720, true);
            quality = 3;
        }

        PlayerPrefs.SetInt("quality", quality);
    }

    public void ChangeQuality2(int id)
    {
        //print("quality2 " + id);

        if (id != quality) {
            if (quality == 0) {
                //q.GetComponent<RawImage> ().texture = sg;
            }
            else if (quality == 1) {
                //q2.GetComponent<RawImage> ().texture = sg;
            }
            else if (quality == 2) {
                //q3.GetComponent<RawImage> ().texture = sg;
            }
            else if (quality == 3) {
                //q4.GetComponent<RawImage> ().texture = sg;
            }
            if (id == 0) {
                //	q.GetComponent<RawImage> ().texture = sp;
                QualitySettings.SetQualityLevel(0);
            }
            else if (id == 1) {
                //q2.GetComponent<RawImage> ().texture = sp;
                QualitySettings.SetQualityLevel(2);
            }
            else if (id == 2) {
                //q3.GetComponent<RawImage> ().texture = sp;
                QualitySettings.SetQualityLevel(3);
            }
            else if (id == 3) {
                //q.GetComponent<RawImage> ().texture = sp;
                QualitySettings.SetQualityLevel(5);
            }
            quality = id;
            PlayerPrefs.SetInt("quality", quality);
        }
    }


    public void Audio()
    {
        if (set == 1)
        {
            if (audio == 0) {
                audio = 1;
                PlayerPrefs.SetFloat("audio", audio);
                /*eg = engine.GetComponent<AudioSource> ();
				eg.Pause ();

				bo = boom.GetComponent<AudioSource> ();
				bo.Pause ();

				di = ding.GetComponent<AudioSource> ();
				di.Pause ();

				fa = fall.GetComponent<AudioSource> ();
				fa.Pause ();
                */
                Texture audioon = Resources.Load("audioon", typeof(Texture2D)) as Texture;
                RawImage img = (RawImage)oaudio.GetComponent<RawImage>();
                img.texture = audioon;
                audioon = Resources.Load("toggleon", typeof(Texture2D)) as Texture;
                img = (RawImage)audiotog.GetComponent<RawImage>();
                img.texture = audioon;
            } else {
                audio = 0;
                PlayerPrefs.SetFloat("audio", audio);
                /*eg = engine.GetComponent<AudioSource> ();
				eg.volume = volume;
				if (aplayed == 0) {
					aplayed = 1;
					eg.Play ();
				} else {
					eg.UnPause ();
				}

				bo = boom.GetComponent<AudioSource> ();
				bo.volume = volume;
				di = ding.GetComponent<AudioSource> ();
				di.volume = volume;
				fa = fall.GetComponent<AudioSource> ();
				fa.volume = volume;
                */
                Texture audiooff = Resources.Load("audiooff", typeof(Texture2D)) as Texture;
                RawImage img = (RawImage)oaudio.GetComponent<RawImage>();
                img.texture = audiooff;
                audiooff = Resources.Load("toggleoff", typeof(Texture2D)) as Texture;
                img = (RawImage)audiotog.GetComponent<RawImage>();
                img.texture = audiooff;
            }
        }
    }

    public void Music()
    {
        //print("musicnow1");
        if (set == 1) {
            if (music == 0) {
                PlayerPrefs.SetFloat("music", 1);
                music = 1;
                //bground = background.GetComponent<AudioSource> ();
                //bground.Pause ();
                mainmusic.volume = 0.12f;

                if (mplayed == 0)
                {
                    mplayed = 1;
                    mainmusic.Play();
                }
                else
                {
                    mainmusic.UnPause();
                }
                Texture musicon = Resources.Load("musicon", typeof(Texture2D)) as Texture;
                RawImage img = (RawImage)omusic.GetComponent<RawImage>();
                img.texture = musicon;
                musicon = Resources.Load("toggleon", typeof(Texture2D)) as Texture;
                img = (RawImage)musictog.GetComponent<RawImage>();
                img.texture = musicon;
            } else {
                PlayerPrefs.SetFloat("music", 0);
                music = 0;
                GameObject background = GameObject.Find("background");
                //bground = background.GetComponent<AudioSource> ();
                //bground.volume = volume;
                mainmusic.Pause();

                Texture musicoff = Resources.Load("musicoff", typeof(Texture2D)) as Texture;
                RawImage img = (RawImage)omusic.GetComponent<RawImage>();
                img.texture = musicoff;
                musicoff = Resources.Load("toggleoff", typeof(Texture2D)) as Texture;
                img = (RawImage)musictog.GetComponent<RawImage>();
                img.texture = musicoff;
            }

        }
    }

    // Update is called once per frame
    void Update() {
        if (testing == 1)
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                fpscounter += (Time.deltaTime - fpscounter) * 0.1f;
                float fps = 1.0f / fpscounter;
                if ((int)fps > 0 && (int)fps <= 10)
                {
                    QualitySettings.SetQualityLevel(0);
                    quadropdown.GetComponent<Dropdown>().value = 0;
                    Screen.SetResolution(640, 360, true);
                    testing = 0;
                    settesting = 0;
                    quality = 0;
                }
                else if ((int)fps > 10 && (int)fps <= 30)
                {
                    QualitySettings.SetQualityLevel(1);
                    quadropdown.GetComponent<Dropdown>().value = 1;
                    Screen.SetResolution(800, 450, true);
                    testing = 0;
                    settesting = 0;
                    quality = 1;
                }
                else if ((int)fps > 30 && (int)fps <= 50)
                {
                    QualitySettings.SetQualityLevel(2);
                    quadropdown.GetComponent<Dropdown>().value = 2;
                    Screen.SetResolution(960, 540, true);
                    testing = 0;
                    settesting = 0;
                    quality = 2;
                }
                else
                {
                    QualitySettings.SetQualityLevel(3);
                    quadropdown.GetComponent<Dropdown>().value = 3;
                    Screen.SetResolution(1280, 720, true);
                    testing = 0;
                    settesting = 0;
                    quality = 3;
                }
                PlayerPrefs.SetInt("quality", quality);
            }
        }
        if (dev == 1)
        {
            fpscounter += (Time.deltaTime - fpscounter) * 0.1f;
            float fps = 1.0f / fpscounter;
            artext.GetComponent<Text>().text = "fps " + (int)fps;
        }

        if (cotimer == 1)
        {
            CoinTime2();
        }
        if (cotimer2 == 1)
        {
            CoinTime4();
        }
        fcc += Time.deltaTime * 1;
        if ((int)fcc >= 10)
        {
            fcc = 0;
            if (fcs == 0)
            {
                fcs = 1;
            }
            else if (fcs == 1)
            {
                fcs = 0;
            }
        }
        /*fct -= Time.deltaTime*1;
        string minutes = Mathf.Floor(fct / 60).ToString("00");
        string seconds = (fct % 60).ToString("00");
            if (int.Parse (seconds) == 60) {
            seconds = (0).ToString("00");
            minutes = Mathf.Floor(fct / 60 + 1).ToString("00");
            }
        freecoins.GetComponent<Text> ().text = minutes + ":" + seconds;*/

        if (fcs == 0)
        {
            if (blpha < 1.0f)
            {
                blpha = blpha + 0.01f;
            }
            if (blpha2 > 0)
            {
                blpha2 = blpha2 - 0.01f;
            }
        }
        else if (fcs == 1)
        {
            if (blpha > 0)
            {
                blpha = blpha - 0.01f;
            }
            if (blpha2 < 1.0f)
            {
                blpha2 = blpha2 + 0.01f;
            }
        }

        if (starttimer > 0)
        {
            starttimer -= Time.deltaTime;
            //print("starttimer" + starttimer);
        }
        else
        {
            if (started == 0)
            {
                StartGame();
            }
        }

        if (starttimer2 > 0)
        {
            starttimer2 -= Time.deltaTime;
        }
        else
        {
            //RequestInterstitial();
        }

        float cf = coinc.transform.localPosition.y;
        if (cf < -68)
        {
            cf = ccf;
        }
        coinc.transform.localPosition = new Vector3(coinc.transform.localPosition.x, cf -= UnityEngine.Random.Range(4, 7), coinc.transform.localPosition.z);
        coinc5.transform.localPosition = new Vector3(coinc.transform.localPosition.x, cf -= UnityEngine.Random.Range(4, 7), coinc.transform.localPosition.z);

        float cf2 = coinc2.transform.localPosition.y;
        if (cf2 < -68)
        {
            cf2 = ccf2;
        }
        coinc2.transform.localPosition = new Vector3(coinc2.transform.localPosition.x, cf2 -= UnityEngine.Random.Range(4, 7), coinc2.transform.localPosition.z);
        coinc6.transform.localPosition = new Vector3(coinc2.transform.localPosition.x, cf2 -= UnityEngine.Random.Range(4, 7), coinc2.transform.localPosition.z);

        float cf3 = coinc3.transform.localPosition.y;
        if (cf3 < -68)
        {
            cf3 = ccf3;
        }
        coinc3.transform.localPosition = new Vector3(coinc3.transform.localPosition.x, cf3 -= UnityEngine.Random.Range(4, 7), coinc3.transform.localPosition.z);
        coinc7.transform.localPosition = new Vector3(coinc3.transform.localPosition.x, cf3 -= UnityEngine.Random.Range(4, 7), coinc3.transform.localPosition.z);

        float cf4 = coinc4.transform.localPosition.y;
        if (cf4 < -68)
        {
            cf4 = ccf4;
        }
        coinc4.transform.localPosition = new Vector3(coinc4.transform.localPosition.x, cf4 -= UnityEngine.Random.Range(4, 7), coinc4.transform.localPosition.z);
        coinc8.transform.localPosition = new Vector3(coinc4.transform.localPosition.x, cf4 -= UnityEngine.Random.Range(4, 7), coinc4.transform.localPosition.z);

        treasure.GetComponent<RawImage>().color = new Color(treasure.GetComponent<RawImage>().color.r, treasure.GetComponent<RawImage>().color.g, treasure.GetComponent<RawImage>().color.b, blpha);
        fct2.GetComponent<Text>().color = new Color(fct2.GetComponent<Text>().color.r, fct2.GetComponent<Text>().color.g, fct2.GetComponent<Text>().color.b, blpha2);

        if (notice == 1)
        {
            if (place == 1 && gameover != 4)
            {
                print("ok2 camloading " + camloading);
                if (load == 1 && SceneManager.GetActiveScene().name.Equals("first"))
                {
                    print("ok3 camloading " + camloading);
                    loading.transform.localScale = new Vector3(0, 0, 0);
                    if (camloading == 2)
                    {
                        print("ok4 camloading " + camloading);
                        loading2 = GameObject.Find("loading2");
                        maincamera.GetComponent<Camera>().farClipPlane = 147.21f;
                        loading2.transform.localScale = new Vector3(0, 0, 0);
                        canvas2.GetComponent<Canvas>().enabled = true;
                    }
                    load = 0;
                }
            }
        }

        if (notice == 0)
        {
            /*if (Input.GetMouseButton(0))
            {
                RaycastHit hitt;
                Ray rayy = maincamera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(rayy, out hitt))
                {
                    if (hitt.transform.name == "3dwheel")
                    {
                        //DragWheel();
                    }
                    else
                    {
                        if ((int)wheel.transform.localEulerAngles.z != 0)
                        {
                            //DragWheel2();
                        }
                    }
                }
            }
            else
            {
                if (scene != 2)
                {
                    if ((int)wheel.transform.localEulerAngles.z != 0)
                    {
                        //DragWheel2();
                    }
                }
            }*/

            if (atshop == 1)
            {
                keycam.transform.Rotate(0, 0, -1);
                minecam.transform.Rotate(1, 0, 0);
                extratimepic.transform.Rotate(0, 0, 1);
            }


            if (gospeed > 0)
            {
                float kspeed = GetSpeed();
                float tspeed = 0;
                if (kspeed > 5)
                {
                    kspeed = 5;
                }
                tspeed = (0.1f * kspeed * 2.2f + spinf);
                if (gospeed < 1.5f)
                {
                    if (gospeed < 1)
                    {
                        tspeed = tspeed / 8;
                    }
                    else
                    {
                        tspeed = tspeed / 4;
                    }
                }
                if (level == 12 || level == 13 || level == 15 || level == 17 || level == 18 || level == 19 || level == 28 || level == 32)
                {
                    tspeed = tspeed * 1.2f;
                }
                if (level == 20)
                {
                    tspeed = tspeed * 1.4f;
                }
                if (tr == 1 && gameover == 0)
                {
                    if (wheelupside == 0)
                    {
                        towtruck.transform.Rotate(0, tspeed, 0);
                    }
                    else
                    {
                        towtruck.transform.Rotate(0, -tspeed, 0);
                    }
                    spinf += 0.01f;
                }
                if (tl == 1 && gameover == 0)
                {
                    if (wheelupside == 0)
                    {
                        towtruck.transform.Rotate(0, -tspeed, 0);
                    }
                    else
                    {
                        towtruck.transform.Rotate(0, tspeed, 0);
                    }
                    spinf += 0.01f;
                }
            }

            if (isgo == 1)
            {
                if (audio == 1)
                {
                    if (enginevol == 0)
                    {
                        decengine.volume = 0;
                        enginevol = 1;
                        //gasengine.Play();
                    }
                    else
                    {
                        if (enginevol2 < 1)
                        {
                            enginevol2 += 0.1f;
                        }
                        else if (enginevol2 > 1)
                        {
                            enginevol2 = 1;
                        }
                        gasengine.volume = enginevol2;
                    }
                }
                Gas();
            }
            else if (isgo == 0 && gospeed > 0)
            {
                if (gameover == 0)
                {
                    LowerGas();
                }
                else
                {
                    LowerGas2();
                }
            }
            if (instop == 1)
            {
                Stop();
            }

            if (isgo == 0)
            {
                /* enginevol -= 0.005f;
                 if (enginevol < 0)
                 {
                     enginevol = 0;
                     gasengine.volume = enginevol;
                     gasengine.Stop();
                 }
                 else
                 {
                     gasengine.volume = enginevol;
                 }*/
                if (place == 1)
                {
                    if (audio == 1 && gasengine != null && decengine != null)
                    {
                        if (gospeed * 8 < 1 && decengine.volume > 0)
                        {
                            float dvl = decengine.volume;
                            if (dvl < 0.1f)
                            {
                                dvl = 0.1f;
                            }
                            decengine.volume = dvl - 0.1f;
                        }
                        if (enginevol == 1)
                        {
                            enginevol = 0;
                            gasengine.volume = 0;
                            decengine.volume = enginevol2 * 2;
                        }
                        if (enginevol2 > 0)
                        {
                            enginevol2 -= 0.01f;
                        }
                        else if (enginevol2 < 0)
                        {
                            enginevol2 = 0;
                            gasengine.volume = 0;
                            //decengine.Stop();
                        }
                        //decengine.volume = enginevol2;
                    }
                }
            }

            if (place == 1 && gameover == 0)
            {
                if (gospeed * 8 < minspeed)
                {
                    if (slow == 0)
                    {
                        gospeed += tspeed * 2;
                    }
                    else if (slow == 1)
                    {
                        gospeed += tspeed * 2 / 3;
                    }
                }

                if (gospeed * 8 > maxspeed)
                {
                    gospeed = maxspeed / 8;
                }

                if (exspeed > 0)
                {
                    print("test1");
                    if (exspeednow == 0)
                    {
                        print("test2");
                        if (gospeed * 8 > exspeed)
                        {
                            exspeednow = 1;
                            print("test3");
                        }
                    }
                    else
                    {
                        if (gospeed * 8 < exspeed)
                        {
                            print("test4");
                            goreason.GetComponent<Text>().text = "You drove too slow.";
                            mineon = 1;
                            gameover = 5;
                            Exploder scripte = (Exploder)minenow.GetComponent(typeof(Exploder));
                            scripte.explodenow();
                        }
                    }
                }
            }

            if (place != 0 && mode != 0 && mode != 2)
            {
                //towtruck.transform.position = new Vector3 (towtruck.transform.position.x, towtruck.transform.position.y, towtruck.transform.position.z+gospeed);
                //bbike3.transform.position = new Vector3 (bbike3.transform.position.x, bbike3.transform.position.y, bbike3.transform.position.z+gospeed);
                //maincamera.transform.position = new Vector3 (maincamera.transform.position.x, maincamera.transform.position.y, maincamera.transform.position.z+gospeed);

                if (timeused > 0)
                {
                    tused -= Time.deltaTime;
                }

                if (slowused > 0)
                {
                    sused -= Time.deltaTime;
                }

                Vector3 gospeedv = new Vector3(0, 0, gospeed);
                //print("test " + levtimer);
                if ((int)levtimer > 0)
                {
                    levtimer -= Time.deltaTime;
                    if ((int)levtimer == 0)
                    {
                        Start();
                        UnActive();
                        offset = maincamera2.transform.position - towtruck.transform.position;
                        Level2(levid);
                    }
                }
                if (mode == 1)
                {
                    if (slow == 1 && (int)slowcount > 0)
                    {
                        print("slowcount " + slowcount + " " + (int)Time.deltaTime);
                        slowcount -= Time.deltaTime;
                        slownum.GetComponent<Text>().text = (int)slowcount + "";
                        if ((int)slowcount == 0)
                        {
                            slow = 0;
                            slowback.transform.localScale = new Vector3(0, 0, 0);
                            slownum.transform.localScale = new Vector3(0, 0, 0);
                        }
                    }
                    if (gospeed > 0)
                    {
                        if (towtruck == null)
                        {
                            towtruck = GameObject.Find("towtruck");
                        }
                        if (slow == 0)
                        {
                            towtruck.transform.position += towtruck.transform.forward * (gospeed * Time.deltaTime);
                        }
                        else if (slow == 1)
                        {
                            towtruck.transform.position += towtruck.transform.forward * (gospeed * Time.deltaTime / 3);
                        }
                        if (engineon == 0 && audio == 1)
                        {
                            backengine.Play();
                            engineon = 1;
                        }
                        //maincamera.transform.position += maincamera.transform.forward * gospeed;
                        float mx = towtruck.transform.position.x;
                        float mx2 = towtruck.transform.position.x + 17;
                        float mz = towtruck.transform.position.z + 13;
                        /* if (mx < 302.26f)
                         {
                             mx = 302.26f;
                         }
                         else if (mx > 1160.1f)
                         {
                             mx = 1160.1f;
                         }
                         if (mx2 < 302.26f)
                         {
                             mx2 = 302.26f;
                         }
                         else if (mx2 > 1160.1f)
                         {
                             mx2 = 1160.1f;
                         }
                         if (mz < 38)
                         {
                             mz = 38;
                         }
                         else if (mz > 954.66f)
                         {
                             mz = 954.66f;
                         }*/
                        if ((int)maincamera.transform.localEulerAngles.y == 0)
                        {
                            maincamera.transform.position = new Vector3(mx, maincamera.transform.position.y, towtruck.transform.position.z + 13);
                        }
                        else if ((int)maincamera.transform.localEulerAngles.y == 90)
                        {
                            maincamera.transform.position = new Vector3(mx2, maincamera.transform.position.y, towtruck.transform.position.z);
                        }
                        else if ((int)maincamera.transform.localEulerAngles.y == 180)
                        {
                            maincamera.transform.position = new Vector3(mx, maincamera.transform.position.y, towtruck.transform.position.z - 13);
                        }
                        else if ((int)maincamera.transform.localEulerAngles.y == 270)
                        {
                            maincamera.transform.position = new Vector3(towtruck.transform.position.x - 9, maincamera.transform.position.y, towtruck.transform.position.z);
                        }
                        maincamera2.transform.position = towtruck.transform.position + offset;
                        //maincamera2.transform.localEulerAngles = new Vector3(maincamera2.transform.localEulerAngles.x, towtruck.transform.localEulerAngles.y, maincamera2.transform.localEulerAngles.z);

                        /*if (level == 2)
                        {
                            if (maincamera.transform.position.x < 472.7f)
                            {
                                maincamera.transform.position = new Vector3(472.7f, maincamera.transform.position.y, towtruck.transform.position.z);
                            }
                            if (maincamera.transform.position.z > 153.92f)
                            {
                                maincamera.transform.position = new Vector3(maincamera.transform.position.x, maincamera.transform.position.y, 153.92f);
                            }
                        }*/

                        if (wind == 1)
                        {
                            if (windcount >= windnum)
                            {
                                windcount = 0;
                                windnum = UnityEngine.Random.Range(1, 5) * 10;
                                winddir = UnityEngine.Random.Range(1, 3);
                            }
                            if (winddir == 1)
                            {
                                float wr = UnityEngine.Random.Range(0.001f, 0.01f);
                                towtruck.transform.Rotate(0, wr, 0);
                                owind.transform.Rotate(0, 0, -0.5f);
                            }
                            else
                            {
                                float wr = UnityEngine.Random.Range(0.001f, 0.01f);
                                towtruck.transform.Rotate(0, -wr, 0);
                                owind.transform.Rotate(0, 0, 0.5f);
                            }
                            windcount += Time.deltaTime;
                        }

                        if (count < 9000)
                        {
                            if (count >= int.Parse(starst[2]))
                            {
                                PosStar(3);
                            }
                            else if (count >= int.Parse(starst[1]) && count < int.Parse(starst[2]))
                            {
                                PosStar(2);
                            }
                            else
                            {
                                PosStar(1);
                            }
                        }

                        Vector3 tpos = towtruck.transform.position;
                        RaycastHit hit, hit2, hit3, hit4;
                        Ray ray, ray2, ray3, ray4;

                        float ty, ty2, ty3, ty4;
                        ty = tire.transform.localPosition.y;
                        ty2 = tire2.transform.localPosition.y;
                        ty3 = tire3.transform.localPosition.y;
                        ty4 = tire4.transform.localPosition.y;

                        //print(ty + " " + ty2 + " " + ty3 + " " + ty4);
                        float tflip = -1.18f;
                        if (gameover == 0)
                        {
                            if (ty < tflip)// || ty3 < tflip) // left
                            {
                                //print("fail " + ty + " " + ty3);
                                gameover = 1;
                                alphaf = 0;
                                goreason.GetComponent<Text>().text = "You went off the road.";
                                if (audio == 1)
                                {
                                    backengine.Stop();
                                    gasengine.Stop();
                                    decengine.Stop();
                                    crash.Play();
                                    enginevol = 0;
                                    enginevol2 = 0;
                                }
                            }

                            if (ty2 < tflip)// || ty4 < tflip) // right
                            {
                                //print("fail2 " + ty2 + " " + ty4);
                                gameover = 2;
                                alphaf = 0;
                                goreason.GetComponent<Text>().text = "You went off the road.";
                                print("a2 " + ty2 + " " + ty4 + tflip + " " + level);
                                if (audio == 1)
                                {
                                    backengine.Stop();
                                    gasengine.Stop();
                                    decengine.Stop();
                                    crash.Play();
                                    enginevol = 0;
                                    enginevol2 = 0;
                                }
                            }
                        }


                        /* ray = maincamera.GetComponent<Camera> ().ScreenPointToRay (new Vector3 (tmirror.transform.position.x, Terrain2.transform.position.y, tmirror.transform.position.z));
                         ray2 = maincamera.GetComponent<Camera> ().ScreenPointToRay (new Vector3 (tmirror2.transform.position.x, Terrain2.transform.position.y, tmirror2.transform.position.z));
                         ray3 = maincamera.GetComponent<Camera> ().ScreenPointToRay (new Vector3 (towtruck.transform.localPosition.x+tw, Terrain2.transform.localPosition.y, towtruck.transform.localPosition.z+ty*2));
                         ray4 = maincamera.GetComponent<Camera> ().ScreenPointToRay (new Vector3 (towtruck.transform.localPosition.x+tw, Terrain2.transform.localPosition.y, towtruck.transform.localPosition.z-ty*2));
                         if (!Physics.Raycast (ray, out hit)) {
                             //print ("no " + (int)(DateTime.Now.Second));
                         } else {
                             //print (hit.transform.name + " "  + (int)(DateTime.Now.Second));
                         }
                         if (!Physics.Raycast (ray2, out hit2)) {
                             //print ("no2 " + (int)(DateTime.Now.Second));
                         }
                         else {
                             print (hit.transform.name + " "  + (int)(DateTime.Now.Second));
                         }
                         if (!Physics.Raycast (ray3, out hit3)) {
                             //print ("no3 " + (int)(DateTime.Now.Second));
                         }
                         if (!Physics.Raycast (ray4, out hit4)) {
                             //print ("no4 " + (int)(DateTime.Now.Second));
                         }*/
                    }
                }
            }

            if (gameover == 1)
            {
                inwheel = 0;
                isgo = 0;
                incar.transform.localScale = new Vector3(0, 0, 0);
                tpause.transform.localScale = new Vector3(0, 0, 0);
                dwheel.transform.localScale = new Vector3(0, 0, 0);
                dclock.transform.localScale = new Vector3(0, 0, 0);
                tire.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePosition;
                tire2.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePosition;
                tire3.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePosition;
                tire4.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePosition;
                Vector3 eualer = towtruck.transform.localEulerAngles;
                if (towtruck.transform.localEulerAngles.z < 90)
                {
                    towtruck.transform.Rotate(0, -1f, 100 * Time.deltaTime);
                }
                else
                {
                    gameover = 4;
                    fading = 1;
                    fade.transform.localScale = new Vector3(1, 1, 1);
                    Around script = (Around)dwheel.GetComponent(typeof(Around));
                    script.ResetWheel();
                }
                Stop();
                if (towtruck.transform.localEulerAngles.x != 0)
                {
                    towtruck.transform.localEulerAngles = new Vector3(0, towtruck.transform.localEulerAngles.y, towtruck.transform.localEulerAngles.z);
                }
            }

            else if (gameover == 2)
            {
                inwheel = 0;
                isgo = 0;
                incar.transform.localScale = new Vector3(0, 0, 0);
                dwheel.transform.localScale = new Vector3(0, 0, 0);
                tpause.transform.localScale = new Vector3(0, 0, 0);
                dclock.transform.localScale = new Vector3(0, 0, 0);
                tire.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePosition;
                tire2.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePosition;
                tire3.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePosition;
                tire4.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePosition;
                Vector3 eualer = towtruck.transform.localEulerAngles;
                if (towtruck.transform.localEulerAngles.z > 270 || towtruck.transform.localEulerAngles.z == 0)
                {
                    towtruck.transform.Rotate(0, 1f, -100 * Time.deltaTime);
                }
                else
                {
                    gameover = 4;
                    fading = 1;
                    fade.transform.localScale = new Vector3(1, 1, 1);
                }
                Stop();
                if (towtruck.transform.localEulerAngles.x != 0)
                {
                    towtruck.transform.localEulerAngles = new Vector3(0, towtruck.transform.localEulerAngles.y, towtruck.transform.localEulerAngles.z);
                }
            }
            else if (gameover == 3 || gameover == 5)
            {
                inwheel = 0;
                isgo = 0;
                incar.transform.localScale = new Vector3(0, 0, 0);
                if (dwheel != null)
                {
                    dwheel.transform.localScale = new Vector3(0, 0, 0);
                }
                if (dwheel2 != null)
                {
                    dwheel2.transform.localScale = new Vector3(0, 0, 0);
                }
                tpause.transform.localScale = new Vector3(0, 0, 0);
                dclock.transform.localScale = new Vector3(0, 0, 0);
                if (mode == 1)
                {
                    tire.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePosition;
                    tire2.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePosition;
                    tire3.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePosition;
                    tire4.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePosition;
                    if (towtruck.transform.localEulerAngles.x != 0)
                    {
                        towtruck.transform.localEulerAngles = new Vector3(0, towtruck.transform.localEulerAngles.y, towtruck.transform.localEulerAngles.z);
                    }
                    gameover = 4;
                }
                else if (mode == 2)
                {
                    gameover = 44;
                }
                else if (mode == 3)
                {
                    gameover = 45;
                }
                fading = 1;
                fade.transform.localScale = new Vector3(1, 1, 1);
                Stop();
            }
            else if (gameover == 4)
            {
                if (antimine == 1)
                {
                    shockwhite.GetComponent<ParticleSystem>().Stop();
                }
                if (mineon == 1)
                {
                    towtruck.transform.position = new Vector3(towtruck.transform.position.x, towtruck.transform.position.y + 10, towtruck.transform.position.z);
                }
                PlayerPrefs.SetInt("extracount", extracount);
                PlayerPrefs.SetInt("slowmotion", slowmotion);
                if (fading == 1)
                {
                    if (alphaf < 1)
                    {
                        alphaf += 0.01f;
                        fade.transform.GetComponent<RawImage>().color = new Color(0, 0, 0, alphaf);
                    }
                    else
                    {
                        fading = 2;
                        place = 0;
                        towtruck.transform.localEulerAngles = new Vector3(0, 0, 0);
                        startcamera.GetComponent<Camera>().enabled = true;
                        maincamera.GetComponent<Camera>().enabled = false;
                        camloading = 1;
                        Aspect();
                        mineon = 0;

                        bbikepos();
                        bbike.transform.localPosition = bbikestart;
                        startcamera.transform.localEulerAngles = startang;
                        startcamera.GetComponent<Camera>().orthographicSize = 20f;
                        dclock.transform.localScale = new Vector3(0, 0, 0);
                        times.transform.localScale = new Vector3(0, 0, 0);
                        magicwranchgo.transform.localScale = new Vector3(0, 0, 0);
                        //keycamera2.GetComponent<Camera>().enabled = false;
                        gosign.transform.localScale = new Vector3(1, 1, 1);
                        int keypasscount = PlayerPrefs.GetInt("keypasscount");
                        if (keypasscount > 0 && int.Parse(allstars[level]) == 0)
                        {
                            //keycamera2.GetComponent<Camera>().enabled = true;
                            magicwranchgo.transform.localScale = new Vector3(1, 1, 1);
                        }
                        //gosign.GetComponent<RawImage>().color = new Color(gosign.GetComponent<RawImage>().color.r, gosign.GetComponent<RawImage>().color.g, gosign.GetComponent<RawImage>().color.b, 0);
                    }
                }
                else if (fading == 2)
                {
                    if (alphaf > 0)
                    {
                        alphaf -= 0.01f;
                        fade.transform.GetComponent<RawImage>().color = new Color(0, 0, 0, alphaf);
                    }
                    else
                    {
                        fading = 0;
                        fade.transform.localScale = new Vector3(0, 0, 0);
                    }
                }
            }
            else if (gameover == 44)
            {
                if (wind == 1)
                {
                    wind = 0;
                    owind.transform.localScale = new Vector3(0, 0, 0);
                }
                if (fading == 1)
                {
                    if (alphaf < 1)
                    {
                        alphaf += 0.01f;
                        fade.transform.GetComponent<RawImage>().color = new Color(0, 0, 0, alphaf);
                    }
                    else
                    {
                        fading = 2;
                        place = 0;
                        //towtruck.transform.localEulerAngles = new Vector3(0, 0, 0);
                        startcamera.GetComponent<Camera>().enabled = true;
                        if (mode == 2)
                        {
                            maincamera3.GetComponent<Camera>().enabled = false;
                        }
                        else if (mode == 3)
                        {
                            maincamera5.GetComponent<Camera>().enabled = false;
                        }

                        bbikepos();
                        bbike.transform.localPosition = bbikestart;
                        startcamera.transform.localEulerAngles = startang;
                        startcamera.GetComponent<Camera>().orthographicSize = 20f;
                        camloading = 1;
                        Aspect();

                        dclock.transform.localScale = new Vector3(0, 0, 0);
                        times.transform.localScale = new Vector3(0, 0, 0);
                        if (mode != 3)
                        {
                            gosign2.transform.localScale = new Vector3(1, 1, 1);
                            gosign2.GetComponent<RawImage>().color = new Color(gosign2.GetComponent<RawImage>().color.r, gosign2.GetComponent<RawImage>().color.g, gosign2.GetComponent<RawImage>().color.b, 0);
                        }
                        else
                        {
                            GOReturn();
                        }
                    }
                }
                else if (fading == 2)
                {
                    if (alphaf > 0)
                    {
                        alphaf -= 0.01f;
                        if (mode != 3)
                        {
                            gosign2.GetComponent<RawImage>().color = new Color(gosign2.GetComponent<RawImage>().color.r, gosign2.GetComponent<RawImage>().color.g, gosign2.GetComponent<RawImage>().color.b, 1 - alphaf / 2);
                        }
                        fade.transform.GetComponent<RawImage>().color = new Color(0, 0, 0, alphaf);
                    }
                    else
                    {
                        fading = 0;
                        fade.transform.localScale = new Vector3(0, 0, 0);
                    }
                }
            }
            else if (gameover == 45)
            {
                if (wind == 1)
                {
                    wind = 0;
                    owind.transform.localScale = new Vector3(0, 0, 0);
                }
                if (fading == 1)
                {
                    if (alphaf < 1)
                    {
                        alphaf += 0.01f;
                        fade.transform.GetComponent<RawImage>().color = new Color(0, 0, 0, alphaf);
                    }
                    else
                    {
                        fading = 2;
                        place = 0;
                        //towtruck.transform.localEulerAngles = new Vector3(0, 0, 0);
                        TDL.GetComponent<Light>().enabled = false;
                        TDL2.GetComponent<Light>().enabled = false;
                        TDL3.GetComponent<Light>().enabled = false;
                        TDL4.GetComponent<Light>().enabled = false;
                        startcamera.GetComponent<Camera>().enabled = true;
                        if (mode == 2)
                        {
                            maincamera3.GetComponent<Camera>().enabled = false;
                        }
                        else if (mode == 3)
                        {
                            maincamera5.GetComponent<Camera>().enabled = false;
                        }

                        bbikepos();
                        bbike.transform.localPosition = bbikestart;
                        startcamera.transform.localEulerAngles = startang;
                        startcamera.GetComponent<Camera>().orthographicSize = 20f;
                        camloading = 1;
                        Aspect();

                        dclock.transform.localScale = new Vector3(0, 0, 0);
                        times.transform.localScale = new Vector3(0, 0, 0);
                        if (mode != 3)
                        {
                            gosign2.transform.localScale = new Vector3(1, 1, 1);
                            gosign2.GetComponent<RawImage>().color = new Color(gosign2.GetComponent<RawImage>().color.r, gosign2.GetComponent<RawImage>().color.g, gosign2.GetComponent<RawImage>().color.b, 0);
                        }
                        else
                        {
                            GOReturn();
                        }
                    }
                }
                else if (fading == 2)
                {
                    if (alphaf > 0)
                    {
                        alphaf -= 0.01f;
                        if (mode != 3)
                        {
                            gosign2.GetComponent<RawImage>().color = new Color(gosign2.GetComponent<RawImage>().color.r, gosign2.GetComponent<RawImage>().color.g, gosign2.GetComponent<RawImage>().color.b, 1 - alphaf / 2);
                        }
                        fade.transform.GetComponent<RawImage>().color = new Color(0, 0, 0, alphaf);
                    }
                    else
                    {
                        fading = 0;
                        fade.transform.localScale = new Vector3(0, 0, 0);
                    }
                }
            }

            float fspeed = 0;
            if (gospeed < 0)
            {
                fspeed = gospeed * -1;
            }
            else
            {
                fspeed = gospeed;
            }
            if (fspeed > 1)
            {
                SetSpeed(fspeed * 8);
            }
            else
            {
                SetSpeed(0);
            }
            if (set == 1)
            {
                if (alpha <= 0f)
                {
                    logo.transform.localScale = new Vector3(0, 0, 0);
                    play.transform.localScale = new Vector3(0, 0, 0);
                    shop.transform.localScale = new Vector3(0, 0, 0);
                    setting.transform.localScale = new Vector3(0, 0, 0);
                    settings.transform.localScale = new Vector3(1, 1, 1);
                    gosign.transform.localScale = new Vector3(0, 0, 0);
                    gosign.GetComponent<RawImage>().color = new Color(gosign.GetComponent<RawImage>().color.r, gosign.GetComponent<RawImage>().color.g, gosign.GetComponent<RawImage>().color.b, 1);
                    goreason.GetComponent<Text>().color = new Color(goreason.GetComponent<Text>().color.r, goreason.GetComponent<Text>().color.g, goreason.GetComponent<Text>().color.b, 1);
                    goreplay2.GetComponent<RawImage>().color = new Color(goreplay2.GetComponent<RawImage>().color.r, goreplay2.GetComponent<RawImage>().color.g, goreplay2.GetComponent<RawImage>().color.b, 1);
                    goreturn.GetComponent<RawImage>().color = new Color(goreturn.GetComponent<RawImage>().color.r, goreturn.GetComponent<RawImage>().color.g, goreturn.GetComponent<RawImage>().color.b, 1);
                    gosettings.GetComponent<RawImage>().color = new Color(gosettings.GetComponent<RawImage>().color.r, gosettings.GetComponent<RawImage>().color.g, gosettings.GetComponent<RawImage>().color.b, 1);
                }
                else
                {
                    alpha = alpha - 0.05f;
                    logo.GetComponent<RawImage>().color = new Color(logo.GetComponent<RawImage>().color.r, logo.GetComponent<RawImage>().color.g, logo.GetComponent<RawImage>().color.b, alpha);
                    play.GetComponent<RawImage>().color = new Color(play.GetComponent<RawImage>().color.r, play.GetComponent<RawImage>().color.g, play.GetComponent<RawImage>().color.b, alpha);
                    shop.GetComponent<RawImage>().color = new Color(shop.GetComponent<RawImage>().color.r, shop.GetComponent<RawImage>().color.g, shop.GetComponent<RawImage>().color.b, alpha);
                    setting.GetComponent<RawImage>().color = new Color(setting.GetComponent<RawImage>().color.r, setting.GetComponent<RawImage>().color.g, setting.GetComponent<RawImage>().color.b, alpha);
                    gosign.GetComponent<RawImage>().color = new Color(gosign.GetComponent<RawImage>().color.r, gosign.GetComponent<RawImage>().color.g, gosign.GetComponent<RawImage>().color.b, alpha);
                    goreason.GetComponent<Text>().color = new Color(goreason.GetComponent<Text>().color.r, goreason.GetComponent<Text>().color.g, goreason.GetComponent<Text>().color.b, alpha);
                    goreplay2.GetComponent<RawImage>().color = new Color(goreplay2.GetComponent<RawImage>().color.r, goreplay2.GetComponent<RawImage>().color.g, goreplay2.GetComponent<RawImage>().color.b, alpha);
                    goreturn.GetComponent<RawImage>().color = new Color(goreturn.GetComponent<RawImage>().color.r, goreturn.GetComponent<RawImage>().color.g, goreturn.GetComponent<RawImage>().color.b, alpha);
                    gosettings.GetComponent<RawImage>().color = new Color(gosettings.GetComponent<RawImage>().color.r, gosettings.GetComponent<RawImage>().color.g, gosettings.GetComponent<RawImage>().color.b, alpha);
                }
            }
            else if (set == 2)
            {
                if (alpha >= 1f)
                {
                    //
                }
                else
                {
                    alpha = alpha + 0.05f;
                    logo.GetComponent<RawImage>().color = new Color(logo.GetComponent<RawImage>().color.r, logo.GetComponent<RawImage>().color.g, logo.GetComponent<RawImage>().color.b, alpha);
                    play.GetComponent<RawImage>().color = new Color(play.GetComponent<RawImage>().color.r, play.GetComponent<RawImage>().color.g, play.GetComponent<RawImage>().color.b, alpha);
                    shop.GetComponent<RawImage>().color = new Color(shop.GetComponent<RawImage>().color.r, shop.GetComponent<RawImage>().color.g, shop.GetComponent<RawImage>().color.b, alpha);
                    setting.GetComponent<RawImage>().color = new Color(setting.GetComponent<RawImage>().color.r, setting.GetComponent<RawImage>().color.g, setting.GetComponent<RawImage>().color.b, alpha);
                }
            }
            else if (set == 3)
            {
                if (alpha <= 0f)
                {
                    menu.transform.localScale = new Vector3(0, 0, 0);
                    Play();
                }
                else
                {
                    alpha = alpha - 0.05f;
                    alpha2 = alpha2 + 0.3f;
                    logo.GetComponent<RawImage>().color = new Color(logo.GetComponent<RawImage>().color.r, logo.GetComponent<RawImage>().color.g, logo.GetComponent<RawImage>().color.b, alpha);
                    play.GetComponent<RawImage>().color = new Color(play.GetComponent<RawImage>().color.r, play.GetComponent<RawImage>().color.g, play.GetComponent<RawImage>().color.b, alpha);
                    shop.GetComponent<RawImage>().color = new Color(shop.GetComponent<RawImage>().color.r, shop.GetComponent<RawImage>().color.g, shop.GetComponent<RawImage>().color.b, alpha);
                    setting.GetComponent<RawImage>().color = new Color(setting.GetComponent<RawImage>().color.r, setting.GetComponent<RawImage>().color.g, setting.GetComponent<RawImage>().color.b, alpha);
                    /*choose.GetComponent<RawImage>().color = new Color(choose.GetComponent<RawImage>().color.r, choose.GetComponent<RawImage>().color.g, choose.GetComponent<RawImage>().color.b, alpha2);
                    career.GetComponent<RawImage>().color = new Color(career.GetComponent<RawImage>().color.r, career.GetComponent<RawImage>().color.g, career.GetComponent<RawImage>().color.b, alpha2);
                    creturn.GetComponent<RawImage>().color = new Color(creturn.GetComponent<RawImage>().color.r, creturn.GetComponent<RawImage>().color.g, creturn.GetComponent<RawImage>().color.b, alpha2);
                    freeride.GetComponent<RawImage>().color = new Color(freeride.GetComponent<RawImage>().color.r, freeride.GetComponent<RawImage>().color.g, freeride.GetComponent<RawImage>().color.b, alpha2);
                    extreme.GetComponent<RawImage>().color = new Color(extreme.GetComponent<RawImage>().color.r, extreme.GetComponent<RawImage>().color.g, levreturn.GetComponent<RawImage>().color.b, alpha2);
                    levreturn.GetComponent<RawImage>().color = new Color(levreturn.GetComponent<RawImage>().color.r, levreturn.GetComponent<RawImage>().color.g, levreturn.GetComponent<RawImage>().color.b, alpha2);
                    choosegame.GetComponent<Text>().color = new Color(choosegame.GetComponent<Text>().color.r, choosegame.GetComponent<Text>().color.g, choosegame.GetComponent<Text>().color.b, alpha2);*/
                }
            }
            else if (set == 4)
            {
                if (alpha >= 1f)
                {
                    circle.transform.localScale = new Vector3(0, 0, 0);
                }
                else
                {
                    alpha = alpha + 0.05f;
                    alpha2 = alpha2 - 0.05f;
                    logo.GetComponent<RawImage>().color = new Color(logo.GetComponent<RawImage>().color.r, logo.GetComponent<RawImage>().color.g, logo.GetComponent<RawImage>().color.b, alpha);
                    play.GetComponent<RawImage>().color = new Color(play.GetComponent<RawImage>().color.r, play.GetComponent<RawImage>().color.g, play.GetComponent<RawImage>().color.b, alpha);
                    shop.GetComponent<RawImage>().color = new Color(shop.GetComponent<RawImage>().color.r, shop.GetComponent<RawImage>().color.g, shop.GetComponent<RawImage>().color.b, alpha);
                    setting.GetComponent<RawImage>().color = new Color(setting.GetComponent<RawImage>().color.r, setting.GetComponent<RawImage>().color.g, setting.GetComponent<RawImage>().color.b, alpha);
                    //levels.GetComponent<RawImage> ().color = new Color (levels.GetComponent<RawImage> ().color.r, levels.GetComponent<RawImage> ().color.g, levels.GetComponent<RawImage> ().color.b, alpha2);
                    levreturn.GetComponent<RawImage>().color = new Color(levreturn.GetComponent<RawImage>().color.r, levreturn.GetComponent<RawImage>().color.g, levreturn.GetComponent<RawImage>().color.b, alpha2);
                    circle.GetComponent<RawImage>().color = new Color(levreturn.GetComponent<RawImage>().color.r, levreturn.GetComponent<RawImage>().color.g, levreturn.GetComponent<RawImage>().color.b, alpha2);
                    creturn.GetComponent<RawImage>().color = new Color(creturn.GetComponent<RawImage>().color.r, creturn.GetComponent<RawImage>().color.g, creturn.GetComponent<RawImage>().color.b, alpha2);
                }
            }
            else if (set == 5)
            {
                if (fading == 1)
                {
                    if (alphaf < 1)
                    {
                        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.BlackBerryPlayer || Application.platform == RuntimePlatform.WP8Player)
                        {
                            alphaf += 0.05f;
                        }
                        else
                        {
                            alphaf += 0.01f;
                        }
                        fade.transform.GetComponent<RawImage>().color = new Color(0, 0, 0, alphaf);
                    }
                    else
                    {
                        fading = 2;
                        place = 0;
                        //towtruck.transform.localEulerAngles = new Vector3(0, 0, 0);

                        bbikepos();
                        bbike.transform.localPosition = bbikestart;
                        startcamera.transform.localEulerAngles = startang;
                        startcamera.GetComponent<Camera>().orthographicSize = 20f;
                        Aspect();
                        dclock.transform.localScale = new Vector3(0, 0, 0);
                        times.transform.localScale = new Vector3(0, 0, 0);
                        camloading = 1;
                    }
                }
                else if (fading == 2)
                {
                    if (alphaf > 0 || scorepanel.GetComponent<RectTransform>().offsetMax.x * -1 > rightpanel - 1)
                    {
                        if (alphaf > 0)
                        {
                            if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.BlackBerryPlayer || Application.platform == RuntimePlatform.WP8Player)
                            {
                                alphaf -= 0.05f;
                            }
                            else
                            {
                                alphaf -= 0.01f;
                            }
                        }
                        fade.transform.GetComponent<RawImage>().color = new Color(0, 0, 0, alphaf);

                        //print(scorepanel.GetComponent<RectTransform>().offsetMax.x*-1);
                        if (alpha3 <= 0f && scorepanel.GetComponent<RectTransform>().offsetMax.x * -1 <= rightpanel - 1)
                        {
                        }
                        else
                        {
                            if (gameover == 0 || keypass == 1)
                            {
                                if ((int)(scorepanel.GetComponent<RectTransform>().offsetMax.x * -1) <= 80)
                                {
                                    starr.transform.localScale = new Vector3(1, 1, 1);
                                    if (audio == 1)
                                    {
                                        starsound.volume = 1;
                                        if (starsoundp == 0)
                                        {
                                            starsound.Play();
                                            starsoundp = 1;
                                        }
                                    }
                                    if ((int)(scorepanel.GetComponent<RectTransform>().offsetMax.x * -1) <= 30)
                                    {
                                        starr2.transform.localScale = new Vector3(1, 1, 1);
                                        if (audio == 1)
                                        {
                                            starsound2.volume = 1;
                                            if (starsoundp2 == 0)
                                            {
                                                starsound2.Play();
                                                starsoundp2 = 1;
                                            }
                                        }
                                        if ((int)(scorepanel.GetComponent<RectTransform>().offsetMax.x * -1) <= -20)
                                        {
                                            starr3.transform.localScale = new Vector3(1, 1, 1);
                                            if (audio == 1)
                                            {
                                                starsound3.volume = 1;
                                                if (starsoundp3 == 0)
                                                {
                                                    starsound3.Play();
                                                    starsoundp3 = 1;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            if (scorepanel.GetComponent<RectTransform>().offsetMax.x * -1 > rightpanel - 1)
                            {
                                scorepanel.GetComponent<RectTransform>().offsetMax = new Vector2(scorepanel.GetComponent<RectTransform>().offsetMax.x + 1, scorepanel.GetComponent<RectTransform>().offsetMax.y);
                                scoretext2.transform.localPosition = new Vector3(scoretext2.transform.localPosition.x + 1, scoretext2.transform.localPosition.y, scoretext2.transform.localPosition.z);
                            }
                            if (alpha3 > 0f)
                            {
                                alpha3 = alpha3 - 0.05f;
                                alpha4 = alpha4 + 0.05f;
                            }
                            if (alpha4 < 0.6f)
                            {
                                background.GetComponent<RawImage>().color = new Color(background.GetComponent<RawImage>().color.r, background.GetComponent<RawImage>().color.g, background.GetComponent<RawImage>().color.b, alpha4);
                            }
                            scoreboard.GetComponent<RawImage>().color = new Color(scoreboard.GetComponent<RawImage>().color.r, scoreboard.GetComponent<RawImage>().color.g, scoreboard.GetComponent<RawImage>().color.b, alpha4);
                        }
                        if (xyzsize >= 1f)
                        {

                        }
                        else
                        {
                            xyzsize = xyzsize + 0.05f;
                            sbitems.transform.localScale = new Vector3(xyzsize, xyzsize, xyzsize);
                        }
                    }
                    else
                    {
                        fading = 0;
                        fade.transform.localScale = new Vector3(0, 0, 0);
                        print("fading 0");
                    }
                }
            }
            else if (set == 6)
            {
                if (alpha3 >= 1f)
                {

                }
                else
                {
                    alpha3 = alpha3 + 0.05f;
                    alpha4 = alpha4 - 0.05f;
                    if (alpha4 < 0.6f)
                    {
                        background.GetComponent<RawImage>().color = new Color(background.GetComponent<RawImage>().color.r, background.GetComponent<RawImage>().color.g, background.GetComponent<RawImage>().color.b, alpha4);
                    }
                    scoreboard.GetComponent<RawImage>().color = new Color(scoreboard.GetComponent<RawImage>().color.r, scoreboard.GetComponent<RawImage>().color.g, scoreboard.GetComponent<RawImage>().color.b, alpha4);
                }
                if (xyzsize <= 0f)
                {

                }
                else
                {
                    xyzsize = xyzsize - 0.05f;
                    sbitems.transform.localScale = new Vector3(xyzsize, xyzsize, xyzsize);
                }
            }
            else if (set == 7)
            {
                //if (alpha <= 0f)
                //{
                gosign.transform.localScale = new Vector3(0, 0, 0);
                levels.transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
                if (alevels != 2)
                {
                    nextlevels.transform.localScale = new Vector3(1, 1, 1);
                }
                else if (alevels == 2)
                {
                    prevlevels.transform.localScale = new Vector3(1, 1, 1);
                }
                Levels();
                levelsv.transform.localScale = new Vector3(5, 5, 5);
                levreturn.transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
                goreason.GetComponent<Text>().color = new Color(goreason.GetComponent<Text>().color.r, goreason.GetComponent<Text>().color.g, goreason.GetComponent<Text>().color.b, 1.0f);
                gosettings.GetComponent<RawImage>().color = new Color(gosettings.GetComponent<RawImage>().color.r, gosettings.GetComponent<RawImage>().color.g, gosettings.GetComponent<RawImage>().color.b, 1.0f);
                goreturn.GetComponent<RawImage>().color = new Color(goreturn.GetComponent<RawImage>().color.r, goreturn.GetComponent<RawImage>().color.g, goreturn.GetComponent<RawImage>().color.b, 1.0f);
                goreplay2.GetComponent<RawImage>().color = new Color(goreplay2.GetComponent<RawImage>().color.r, goreplay2.GetComponent<RawImage>().color.g, goreplay2.GetComponent<RawImage>().color.b, 1.0f);
                set = 0;
                alpha = 1;
                alpha2 = 0;
                /* }
                 else
                 {
                     alpha = alpha - 0.05f;
                     alpha2 = alpha2 + 0.05f;
                     goreason.GetComponent<Text>().color = new Color(goreason.GetComponent<Text>().color.r, goreason.GetComponent<Text>().color.g, goreason.GetComponent<Text>().color.b, alpha);
                     gosettings.GetComponent<RawImage>().color = new Color(gosettings.GetComponent<RawImage>().color.r, gosettings.GetComponent<RawImage>().color.g, gosettings.GetComponent<RawImage>().color.b, alpha);
                     goreturn.GetComponent<RawImage>().color = new Color(goreturn.GetComponent<RawImage>().color.r, goreturn.GetComponent<RawImage>().color.g, goreturn.GetComponent<RawImage>().color.b, alpha);
                     goreplay2.GetComponent<RawImage>().color = new Color(goreplay2.GetComponent<RawImage>().color.r, goreplay2.GetComponent<RawImage>().color.g, goreplay2.GetComponent<RawImage>().color.b, alpha);
                     levreturn.GetComponent<RawImage>().color = new Color(levreturn.GetComponent<RawImage>().color.r, levreturn.GetComponent<RawImage>().color.g, levreturn.GetComponent<RawImage>().color.b, alpha2);
                 }*/
            }
            else if (set == 8)
            {
                if (alpha <= 0f)
                {
                }
                else
                {
                    alpha = alpha - 0.05f;
                    alpha2 = alpha2 + 0.05f;
                    creturn.GetComponent<RawImage>().color = new Color(creturn.GetComponent<RawImage>().color.r, creturn.GetComponent<RawImage>().color.g, creturn.GetComponent<RawImage>().color.b, alpha);
                    levreturn.GetComponent<RawImage>().color = new Color(levreturn.GetComponent<RawImage>().color.r, levreturn.GetComponent<RawImage>().color.g, levreturn.GetComponent<RawImage>().color.b, alpha2);
                }
            }
            else if (set == 9)
            {
                if (alpha <= 0f)
                {
                    gosign2.transform.localScale = new Vector3(0, 0, 0);
                    gotext2.GetComponent<Text>().color = new Color(gotext2.GetComponent<Text>().color.r, gotext2.GetComponent<Text>().color.g, gotext2.GetComponent<Text>().color.b, 1.0f);
                    gscore.GetComponent<Text>().color = new Color(gscore.GetComponent<Text>().color.r, gscore.GetComponent<Text>().color.g, gscore.GetComponent<Text>().color.b, 1.0f);
                    gosettings2.GetComponent<RawImage>().color = new Color(gosettings2.GetComponent<RawImage>().color.r, gosettings2.GetComponent<RawImage>().color.g, gosettings2.GetComponent<RawImage>().color.b, 1.0f);
                    goreturn2.GetComponent<RawImage>().color = new Color(goreturn2.GetComponent<RawImage>().color.r, goreturn2.GetComponent<RawImage>().color.g, goreturn2.GetComponent<RawImage>().color.b, 1.0f);
                    goreplay2.GetComponent<RawImage>().color = new Color(goreplay2.GetComponent<RawImage>().color.r, goreplay2.GetComponent<RawImage>().color.g, goreplay2.GetComponent<RawImage>().color.b, 1.0f);
                    set = 0;
                    alpha = 1;
                    alpha2 = 0;
                }
                else
                {
                    alpha = alpha - 0.05f;
                    alpha2 = alpha2 + 0.05f;
                    gotext2.GetComponent<Text>().color = new Color(gotext2.GetComponent<Text>().color.r, gotext2.GetComponent<Text>().color.g, gotext2.GetComponent<Text>().color.b, alpha);
                    gscore.GetComponent<Text>().color = new Color(gscore.GetComponent<Text>().color.r, gscore.GetComponent<Text>().color.g, gscore.GetComponent<Text>().color.b, alpha);
                    gosettings2.GetComponent<RawImage>().color = new Color(gosettings2.GetComponent<RawImage>().color.r, gosettings2.GetComponent<RawImage>().color.g, gosettings2.GetComponent<RawImage>().color.b, alpha);
                    goreturn2.GetComponent<RawImage>().color = new Color(goreturn2.GetComponent<RawImage>().color.r, goreturn2.GetComponent<RawImage>().color.g, goreturn2.GetComponent<RawImage>().color.b, alpha);
                    goreplay2.GetComponent<RawImage>().color = new Color(goreplay2.GetComponent<RawImage>().color.r, goreplay2.GetComponent<RawImage>().color.g, goreplay2.GetComponent<RawImage>().color.b, alpha);
                    creturn.GetComponent<RawImage>().color = new Color(creturn.GetComponent<RawImage>().color.r, creturn.GetComponent<RawImage>().color.g, creturn.GetComponent<RawImage>().color.b, alpha2);
                    levreturn.GetComponent<RawImage>().color = new Color(levreturn.GetComponent<RawImage>().color.r, levreturn.GetComponent<RawImage>().color.g, levreturn.GetComponent<RawImage>().color.b, alpha2);
                }
            }
            else if (set == 10)
            {
                if (fading == 1)
                {
                    if (alphaf < 1)
                    {
                        alphaf += 0.01f;
                        fade.transform.GetComponent<RawImage>().color = new Color(0, 0, 0, alphaf);
                    }
                    else
                    {
                        fading = 2;
                        place = 0;
                        isgo = 0;
                        tire.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePosition;
                        tire2.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePosition;
                        tire3.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePosition;
                        tire4.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePosition;
                        towtruck.transform.position = new Vector3(0, 0, 0);
                        startcamera.GetComponent<Camera>().enabled = true;
                        maincamera.GetComponent<Camera>().enabled = false;
                        camloading = 1;
                        Aspect();
                        mineon = 0;

                        bbikepos();
                        bbike.transform.localPosition = bbikestart;
                        startcamera.transform.localEulerAngles = startang;
                        startcamera.GetComponent<Camera>().orthographicSize = 20f;
                        dclock.transform.localScale = new Vector3(0, 0, 0);
                        times.transform.localScale = new Vector3(0, 0, 0);
                        //gosign.transform.localScale = new Vector3(1, 1, 1);
                        background.transform.localScale = new Vector3(0, 0, 0);
                        scoreboard.transform.localScale = new Vector3(0, 0, 0);
                        dclock.transform.localScale = new Vector3(0, 0, 0);
                        times.transform.localScale = new Vector3(0, 0, 0);
                        menu.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
                        //bbike.transform.localPosition = new Vector3 (bbike.transform.localPosition.x, -12.52f, bbike.transform.localPosition.z);
                        //maincamera.transform.localPosition = new Vector3 (0, 0, maincamera.transform.localPosition.z);
                        maincamera.GetComponent<Camera>().orthographicSize = 15;
                        logo.GetComponent<RawImage>().color = new Color(logo.GetComponent<RawImage>().color.r, logo.GetComponent<RawImage>().color.g, logo.GetComponent<RawImage>().color.b, 1);
                        play.GetComponent<RawImage>().color = new Color(play.GetComponent<RawImage>().color.r, play.GetComponent<RawImage>().color.g, play.GetComponent<RawImage>().color.b, 1);
                        shop.GetComponent<RawImage>().color = new Color(shop.GetComponent<RawImage>().color.r, shop.GetComponent<RawImage>().color.g, shop.GetComponent<RawImage>().color.b, 1);
                        setting.GetComponent<RawImage>().color = new Color(setting.GetComponent<RawImage>().color.r, setting.GetComponent<RawImage>().color.g, setting.GetComponent<RawImage>().color.b, 1);
                        levreturn.GetComponent<RawImage>().color = new Color(levreturn.GetComponent<RawImage>().color.r, levreturn.GetComponent<RawImage>().color.g, levreturn.GetComponent<RawImage>().color.b, 1);
                        background.GetComponent<RawImage>().color = new Color(background.GetComponent<RawImage>().color.r, background.GetComponent<RawImage>().color.g, background.GetComponent<RawImage>().color.b, 0);
                        scoreboard.GetComponent<RawImage>().color = new Color(scoreboard.GetComponent<RawImage>().color.r, scoreboard.GetComponent<RawImage>().color.g, scoreboard.GetComponent<RawImage>().color.b, 0);
                        alpha = 1.0f;
                        alpha2 = 1.0f;
                        alpha3 = 1.0f;
                        alpha4 = 0;
                    }
                }
                else if (fading == 2)
                {
                    if (alphaf > 0)
                    {
                        alphaf -= 0.1f;
                        fade.transform.GetComponent<RawImage>().color = new Color(0, 0, 0, alphaf);
                    }
                    else
                    {
                        fading = 0;
                        fade.transform.localScale = new Vector3(0, 0, 0);
                        place = 0;
                        gameover = 10;
                        set = 0;
                    }
                }
            }
            if (set == 11)
            {
                if (alpha <= 0f)
                {
                    logo.transform.localScale = new Vector3(0, 0, 0);
                    play.transform.localScale = new Vector3(0, 0, 0);
                    shop.transform.localScale = new Vector3(0, 0, 0);
                    setting.transform.localScale = new Vector3(0, 0, 0);
                    shopboard.transform.localScale = new Vector3(1, 1, 1);
                    gosign.transform.localScale = new Vector3(0, 0, 0);
                    gosign.GetComponent<RawImage>().color = new Color(gosign.GetComponent<RawImage>().color.r, gosign.GetComponent<RawImage>().color.g, gosign.GetComponent<RawImage>().color.b, 1);
                    goreason.GetComponent<Text>().color = new Color(goreason.GetComponent<Text>().color.r, goreason.GetComponent<Text>().color.g, goreason.GetComponent<Text>().color.b, 1);
                    goreplay2.GetComponent<RawImage>().color = new Color(goreplay2.GetComponent<RawImage>().color.r, goreplay2.GetComponent<RawImage>().color.g, goreplay2.GetComponent<RawImage>().color.b, 1);
                    goreturn.GetComponent<RawImage>().color = new Color(goreturn.GetComponent<RawImage>().color.r, goreturn.GetComponent<RawImage>().color.g, goreturn.GetComponent<RawImage>().color.b, 1);
                    gosettings.GetComponent<RawImage>().color = new Color(gosettings.GetComponent<RawImage>().color.r, gosettings.GetComponent<RawImage>().color.g, gosettings.GetComponent<RawImage>().color.b, 1);
                }
                else
                {
                    alpha = alpha - 0.05f;
                    logo.GetComponent<RawImage>().color = new Color(logo.GetComponent<RawImage>().color.r, logo.GetComponent<RawImage>().color.g, logo.GetComponent<RawImage>().color.b, alpha);
                    play.GetComponent<RawImage>().color = new Color(play.GetComponent<RawImage>().color.r, play.GetComponent<RawImage>().color.g, play.GetComponent<RawImage>().color.b, alpha);
                    shop.GetComponent<RawImage>().color = new Color(shop.GetComponent<RawImage>().color.r, shop.GetComponent<RawImage>().color.g, shop.GetComponent<RawImage>().color.b, alpha);
                    setting.GetComponent<RawImage>().color = new Color(setting.GetComponent<RawImage>().color.r, setting.GetComponent<RawImage>().color.g, setting.GetComponent<RawImage>().color.b, alpha);
                    gosign.GetComponent<RawImage>().color = new Color(gosign.GetComponent<RawImage>().color.r, gosign.GetComponent<RawImage>().color.g, gosign.GetComponent<RawImage>().color.b, alpha);
                    goreason.GetComponent<Text>().color = new Color(goreason.GetComponent<Text>().color.r, goreason.GetComponent<Text>().color.g, goreason.GetComponent<Text>().color.b, alpha);
                    goreplay2.GetComponent<RawImage>().color = new Color(goreplay2.GetComponent<RawImage>().color.r, goreplay2.GetComponent<RawImage>().color.g, goreplay2.GetComponent<RawImage>().color.b, alpha);
                    goreturn.GetComponent<RawImage>().color = new Color(goreturn.GetComponent<RawImage>().color.r, goreturn.GetComponent<RawImage>().color.g, goreturn.GetComponent<RawImage>().color.b, alpha);
                    gosettings.GetComponent<RawImage>().color = new Color(gosettings.GetComponent<RawImage>().color.r, gosettings.GetComponent<RawImage>().color.g, gosettings.GetComponent<RawImage>().color.b, alpha);
                }
            }
            if (start == 0)
            {
                /*if (Input.GetMouseButtonDown (0)) {
                    Vector2 worldPoint =maincamera.GetComponent<Camera> ().ScreenToWorldPoint (Input.mousePosition);
                    RaycastHit2D hit = Physics2D.Raycast (worldPoint, Vector2.zero);
                    if (hit.collider.name == "bike") {
                        start = 1;
                    }*/

                float cx, cy, bz, cxper = 0, cyper = 0, bxper = 0, byper = 0;
                cx = circle2.transform.localPosition.x;
                cy = circle2.transform.localPosition.y;
                //bz = bike.transform.localEulerAngles.z;

                float fx, fy;

                if (isUp() && isRight())
                {
                    cyper = cy / 37.5f * 100;
                    setz = 270 + 90 * (cyper / 100);
                }
                else if (isDown() && isRight())
                {
                    cyper = cy / -37.5f * 100;
                    setz = 180 + 90 * (1 - (cyper / 100));
                }
                else if (isUp() && isLeft())
                {
                    cyper = cy / 37.5f * 100;
                    setz = 90 - 90 * (cyper / 100);
                }
                else if (isDown() && isLeft())
                {
                    cyper = cy / -37.5f * 100;
                    setz = 180 - 90 * (1 - (cyper / 100));
                }

                //bike.transform.localEulerAngles = new Vector3 (bbike.transform.localEulerAngles.x, bbike.transform.localEulerAngles.y, setz);

                float bikex = 0, bikey = 0;
                if (isUp())
                {
                    bikey = cy / 37.5f / 100;
                }
                else if (isDown())
                {
                    bikey = -(cy / -37.5f / 100);
                }
                if (isRight())
                {
                    bikex = cx / 37.5f / 100;
                }
                else if (isLeft())
                {
                    bikex = -(cx / -37.5f / 100);
                }
                //bike.transform.localPosition = new Vector3 (bike.transform.localPosition.x + bikex, bike.transform.localPosition.y + bikey, bike.transform.localPosition.z);

                if (place == 0)
                {
                    bbike.transform.localPosition = new Vector3(bbike.transform.localPosition.x, bbike.transform.localPosition.y, bbike.transform.localPosition.z + speed);
                    startcamera.transform.localPosition = new Vector3(startcamera.transform.localPosition.x, startcamera.transform.localPosition.y, startcamera.transform.localPosition.z + speed);
                    //maincamera2.transform.localPosition = new Vector3 (maincamera2.transform.localPosition.x, maincamera2.transform.localPosition.y + speed, maincamera2.transform.localPosition.z);
                    if (bbike.transform.localPosition.z >= 1406)
                    {
                        bbike.transform.localPosition = new Vector3(bbike.transform.localPosition.x, bbike.transform.localPosition.y, 92);
                        startcamera.transform.localPosition = new Vector3(startcamera.transform.localPosition.x, startcamera.transform.localPosition.y, 94);
                        //maincamera2.transform.localPosition = new Vector3 (maincamera.transform.localPosition.x, 0, maincamera.transform.localPosition.z);
                    }
                    //Aspect();
                }
                else if (place == 1 && gameover != 4)
                {
                    if (load == 1 && SceneManager.GetActiveScene().name.Equals("first"))
                    {
                        loading.transform.localScale = new Vector3(0, 0, 0);
                        if (camloading == 2)
                        {
                            loading2 = GameObject.Find("loading2");
                            maincamera.GetComponent<Camera>().farClipPlane = 147.21f;
                            loading2.transform.localScale = new Vector3(0, 0, 0);
                            canvas2.GetComponent<Canvas>().enabled = true;
                        }
                        load = 0;
                    }
                    //StartCoroutine(Count ());
                    if (count < 9000)
                    {
                        if (slow == 0)
                        {
                            count -= Time.deltaTime;
                        }
                        else if (slow == 1)
                        {
                            count -= Time.deltaTime / 3;
                        }
                    }
                    seconds += Time.deltaTime * 3.2f;
                    float rotationSeconds = (360.0f / 60.0f) * seconds;
                    //pointerSeconds.transform.localEulerAngles = new Vector3(0.0f, 0.0f, rotationSeconds);
                    if (count < 9000)
                    {
                        dclock.transform.GetComponent<Text>().text = (int)count + "";
                    }
                    //time.GetComponent<Text> ().text = count+"";
                    if (count > 0)
                    {
                        //time.GetComponent<Text> ().text = (int)count + "";
                    }
                    else
                    {
                        print("notime");
                        gameover = 3;
                        goreason.GetComponent<Text>().text = "You've ran out of time.";

                        backengine.Stop();
                    }
                }
            }
            else if (start == 1)
            {
                if (Input.GetMouseButton(0))
                {
                    //maincamera.transform.position = new Vector3(maincamera.transform.position.x,maincamera.transform.position.y+speed,maincamera.transform.position.z);
                    //Vector2 worldPoint = maincamera.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
                    //bike.transform.position = new Vector3(worldPoint.x, worldPoint.y, 0);

                }
                else if (Input.GetMouseButtonUp(0))
                {
                    start = 0;
                }
                //maincamera.transform.position = bike.transform.position + offset;
            }
        }
    }


    void SetSpeed(float speed)
    {
        //270.03 -
        // 120
        if (gameover == 0)
        {
            float speed2 = speed;
            if (minspeed == 60)
            {
                speed2 = speed + 5;
            }
            if (minspeed == 80)
            {
                speed2 = speed + 5;
            }
            if (minspeed == 90)
            {
                speed2 = speed + 5;
            }
            if (minspeed == 100)
            {
                speed2 = speed + 3;
            }
            float currentper = speed2 / 120;
            float shouldbe = 14.3f - (270.3f * currentper);

            //print("setspeed " + speed+ " float " + speedoarrow.transform.localEulerAngles.z + " speedrot " + speedrot + " currentper" + currentper + " shouldbe " + shouldbe);

            if (speedrot > shouldbe)
            {
                speedoarrow.transform.Rotate(0, 0, -1);
                speedrot -= 1;
            }
            else if (speedrot < shouldbe)
            {
                speedoarrow.transform.Rotate(0, 0, 1);
                speedrot += 1;
            }
        }
        else
        {
            if (speedrot != 14.3f)

            {
                speedrot = 14.3f;

                //print("setspeed " + speed+ " float " + speedoarrow.transform.localEulerAngles.z + " speedrot " + speedrot + " currentper" + currentper + " shouldbe " + shouldbe);

                speedoarrow.transform.localEulerAngles = new Vector3(speedoarrow.transform.localEulerAngles.x, speedoarrow.transform.localEulerAngles.y, 14.3f);
            }
        }

        /*if (speed > 0 && speed <= 10)
        {
            if (lsz > -4.06)
            {
                speedoarrow.transform.Rotate(0, 0, -1);
            }

        }
        else if (speed > 10 && speed <= 20)
        {
            if (lsz > -29.8f)
            {
                speedoarrow.transform.Rotate(0, 0, -1);
            }

        }
        else if (speed > 20 && speed <= 30)
        {
            if (lsz > -56.31f)
            {
                speedoarrow.transform.Rotate(0, 0, -1);
            }

        }
        else if (speed > 30 && speed <= 40)
        {
            if (lsz > -79.8f)
            {
                speedoarrow.transform.Rotate(0, 0, -1);
            }

        }
        else if (speed > 40 && speed <= 50)
        {
            if (lsz > -105.4f)
            {
                speedoarrow.transform.Rotate(0, 0, -1);
            }

        }
        else if (speed > 50 && speed <= 60)
        {
            if (lsz > -131.2f)
            {
                speedoarrow.transform.Rotate(0, 0, -1);
            }
 
        }
        else if (speed > 60 && speed <= 70)
        {
            if (lsz > -155.6f)
            {
                speedoarrow.transform.Rotate(0, 0, -1);
            }

        }
        else if (speed > 70 && speed <= 80)
        {
            if (lsz > -178.3f)
            {
                speedoarrow.transform.Rotate(0, 0, -1);
            }

        }
        else if (speed > 80 && speed <= 90)
        {
            if (lsz > -197.7f)
            {
                speedoarrow.transform.Rotate(0, 0, -1);
            }
  
        }
        else if (speed > 90 && speed <= 100)
        {
            if (lsz > -217.8f)
            {
                speedoarrow.transform.Rotate(0, 0, -1);
            }

        }
        else if (speed > 100 && speed <= 110)
        {
            if (lsz > -236.7f)
            {
                speedoarrow.transform.Rotate(0, 0, -1);
            }

        }
        else if (speed > 110 && speed <= 120)
        {
            if (lsz > -256f)
            {
                speedoarrow.transform.Rotate(0, 0, -1);
            }

        }
        else if (speed >= 120)
        {
            if (lsz < -256f)
            {
                speedoarrow.transform.localEulerAngles = new Vector3(speedoarrow.transform.localEulerAngles.x, speedoarrow.transform.localEulerAngles.y, -256f);
            }
        }
        else if (speed <= 1)
        {
            print("reset");

            /*if (speedoarrow.transform.localEulerAngles.z < 14.3f)
            {
                speedoarrow.transform.localEulerAngles = new Vector3(speedoarrow.transform.localEulerAngles.x, speedoarrow.transform.localEulerAngles.y, lsz += 5);
            }
            else if (speedoarrow.transform.localEulerAngles.z > 14.3f)
            {
                speedoarrow.transform.localEulerAngles = new Vector3(speedoarrow.transform.localEulerAngles.x, speedoarrow.transform.localEulerAngles.y, 14.3f);
            }*/
        //speedoarrow.transform.localEulerAngles = new Vector3(speedoarrow.transform.localEulerAngles.x, speedoarrow.transform.localEulerAngles.y, 14.3f);

        //}



    }

    private static float WrapAngle(float angle)
    {
        angle %= 360;
        if (angle > 180)
            return angle - 360;

        return angle;
    }

    void ResetFinish()
    {
        starr.transform.localScale = new Vector3(0, 0, 0);
        starr2.transform.localScale = new Vector3(0, 0, 0);
        starr3.transform.localScale = new Vector3(0, 0, 0);
        alpha3 = 1;
        alpha4 = 0;
        scoretext.GetComponent<Text>().text = "";
        scoretext2.transform.localPosition = new Vector3(-51.9f, -13.29997f, 0);
        scoreboard.transform.localScale = new Vector3(0, 0, 0);
        sbitems.transform.localScale = new Vector3(0, 0, 0);
        xyzsize = 0;
        rightpanel = 0;
        scoreboard.GetComponent<RawImage>().color = new Color(scoreboard.GetComponent<RawImage>().color.r, scoreboard.GetComponent<RawImage>().color.g, scoreboard.GetComponent<RawImage>().color.b, 0);
        scorepanel.GetComponent<RectTransform>().offsetMax = offsetMax;
    }

    public void Mine()
    {
        if (gameover == 0 && antimine == 0)
        {
            if (gm == 0)
            {
                gameover = 5;
                gospeed = 0.000000001f;
                goreason.GetComponent<Text>().text = "You've hit a mine.";

                mineon = 1;

                if (audio == 1)
                {
                    backengine.Stop();
                    gasengine.Stop();
                    decengine.Stop();
                    enginevol = 0;
                    enginevol2 = 0;
                }
            }
            else
            {
                devcount++;
                devtext.GetComponent<Text>().text = "" + devcount;

            }
        }
    }

    public void Choose()
    {
        if (audio == 1)
        {
            click.Play();
        }
        alpha = 0.1f;
        alpha2 = 0;
        set = 3;
        levreturn.transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
        levelsv.transform.localScale = new Vector3(5, 5, 5);
        Aspect();
        //Play();
    }

    public void Play()
    {
        //SceneManager.LoadScene("first");
        levels.transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
        if (alevels != 2)
        {
            nextlevels.transform.localScale = new Vector3(1, 1, 1);
        }
        else if (alevels == 2)
        {
            prevlevels.transform.localScale = new Vector3(1, 1, 1);
        }
        levelsv.transform.localScale = new Vector3(5, 5, 5);
        levreturn.transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
        Levels();
        mode = 1;
        alpha = 1;
        alpha2 = 0;
        set = 8;
        settings.transform.localScale = new Vector3(0, 0, 0);
    }

    public void Play2()
    {
        if (mode != 2)
        {
            // GC.Collect();
            //Resources.UnloadUnusedAssets();
            SceneManager.LoadScene("second");
            mode = 2;
            place = 2;
        }
        else
        {
            scene = 0;
        }
        settings.transform.localScale = new Vector3(0, 0, 0);
    }

    public void Play3()
    {
        //GC.Collect();
        //Resources.UnloadUnusedAssets();
        if (!SceneManager.GetActiveScene().name.Equals("third"))
        {
            SceneManager.LoadScene("third");
            mode = 3;
            place = 3;
            settings.transform.localScale = new Vector3(0, 0, 0);
        }
        else
        {
            scene = 0;
            mode = 3;
            place = 3;
        }
    }

    public void GOReturn()
    {
        if (mode == 1)
        {
            if (audio == 1)
            {
                click.Play();
            }
            alpha = 1;
            alpha2 = 0;
            set = 7;
            fade.transform.GetComponent<RawImage>().color = new Color(0, 0, 0, 0);
            fading = 0;
            fade.transform.localScale = new Vector3(0, 0, 0);
            Active();
        }
        else if (mode == 2 || mode == 3)
        {
            alpha = 1;
            alpha2 = 0;
            set = 9;
            levreturn.transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
            fade.transform.GetComponent<RawImage>().color = new Color(0, 0, 0, 0);
            fading = 0;
            fade.transform.localScale = new Vector3(0, 0, 0);
        }
    }

    void Levels()
    {
        if (alevels == 0)
        {
            Font ArialFont = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
            for (int i = 1; i < 17; i++) {
                GameObject a = new GameObject(i + "");
                a.AddComponent<RectTransform>();
                a.AddComponent<RawImage>();
                a.AddComponent<EventTrigger>();
                a.AddComponent<Selectable>();

                pattern = (RawImage)a.GetComponent<RawImage>();
                a.transform.parent = levels.transform;
                a.transform.localScale = new Vector3(1, 1, 1);
                a.transform.localPosition = new Vector3(25, -25, 186.8f);
                a.transform.localEulerAngles = new Vector3(0, 0, 0);

                if (passed >= i - 1) {
                    pattern.texture = unlocked;

                    GameObject b = new GameObject(i + "b");
                    b.AddComponent<RectTransform>();
                    b.AddComponent<Text>();
                    b.transform.parent = a.transform;
                    b.transform.localScale = new Vector3(1, 1, 1);
                    b.GetComponent<RectTransform>().sizeDelta = a.GetComponent<RectTransform>().sizeDelta;
                    Text text = (Text)b.GetComponent<Text>();
                    text.font = ArialFont;
                    text.fontSize = 25;
                    text.alignment = TextAnchor.MiddleCenter;
                    text.color = new Color32(255, 255, 255, 255);
                    text.text = i + "";
                    b.transform.localPosition = new Vector3(0, 6.9f, 0);
                    b.transform.localEulerAngles = new Vector3(0, 0, 0);

                    Stars(a, i);

                    EventTrigger et = a.GetComponent<EventTrigger>();
                    EventTrigger.Entry entry = new EventTrigger.Entry();
                    entry.eventID = EventTriggerType.PointerClick;
                    entry.callback = new EventTrigger.TriggerEvent();
                    UnityAction<BaseEventData> call = null;
                    call = new UnityAction<BaseEventData>(Level);

                    entry.callback.AddListener(call);
                    et.triggers.Add(entry);
                } else {
                    pattern.texture = locked;
                    if (dev == 1)
                    {
                        EventTrigger et = a.GetComponent<EventTrigger>();
                        EventTrigger.Entry entry = new EventTrigger.Entry();
                        entry.eventID = EventTriggerType.PointerClick;
                        entry.callback = new EventTrigger.TriggerEvent();
                        UnityAction<BaseEventData> call = null;
                        call = new UnityAction<BaseEventData>(Level);

                        entry.callback.AddListener(call);
                        et.triggers.Add(entry);
                    }
                }
                alevels = 1;
            }
        }
    }

    public void NextLevels()
    {
        alevels = 2;
        nextlevels.transform.localScale = new Vector3(0, 0, 0);
        prevlevels.transform.localScale = new Vector3(1, 1, 1);
        foreach (Transform child in levels.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        Font ArialFont = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
        for (int i = 17; i < 33; i++)
        {
            GameObject a = new GameObject(i + "");
            a.AddComponent<RectTransform>();
            a.AddComponent<RawImage>();
            a.AddComponent<EventTrigger>();
            a.AddComponent<Selectable>();

            pattern = (RawImage)a.GetComponent<RawImage>();
            a.transform.parent = levels.transform;
            a.transform.localScale = new Vector3(1, 1, 1);
            a.transform.localPosition = new Vector3(25, -25, 186.8f);
            a.transform.localEulerAngles = new Vector3(0, 0, 0);

            if (passed >= i - 1)
            {
                pattern.texture = unlocked;

                GameObject b = new GameObject(i + "b");
                b.AddComponent<RectTransform>();
                b.AddComponent<Text>();
                b.transform.parent = a.transform;
                b.transform.localScale = new Vector3(1, 1, 1);
                b.GetComponent<RectTransform>().sizeDelta = a.GetComponent<RectTransform>().sizeDelta;
                Text text = (Text)b.GetComponent<Text>();
                text.font = ArialFont;
                text.fontSize = 25;
                text.alignment = TextAnchor.MiddleCenter;
                text.color = new Color32(255, 255, 255, 255);
                text.text = i + "";
                b.transform.localPosition = new Vector3(0, 6.9f, 0);
                b.transform.localEulerAngles = new Vector3(0, 0, 0);

                Stars(a, i);

                EventTrigger et = a.GetComponent<EventTrigger>();
                EventTrigger.Entry entry = new EventTrigger.Entry();
                entry.eventID = EventTriggerType.PointerClick;
                entry.callback = new EventTrigger.TriggerEvent();
                UnityAction<BaseEventData> call = null;
                call = new UnityAction<BaseEventData>(Level);

                entry.callback.AddListener(call);
                et.triggers.Add(entry);
            }
            else
            {
                pattern.texture = locked;
                if (dev == 1)
                {
                    EventTrigger et = a.GetComponent<EventTrigger>();
                    EventTrigger.Entry entry = new EventTrigger.Entry();
                    entry.eventID = EventTriggerType.PointerClick;
                    entry.callback = new EventTrigger.TriggerEvent();
                    UnityAction<BaseEventData> call = null;
                    call = new UnityAction<BaseEventData>(Level);

                    entry.callback.AddListener(call);
                    et.triggers.Add(entry);
                }
            }
        }
    }

    public void PrevLevels()
    {
        prevlevels.transform.localScale = new Vector3(0, 0, 0);
        nextlevels.transform.localScale = new Vector3(1, 1, 1);
        alevels = 1;
        foreach (Transform child in levels.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        Font ArialFont = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
        for (int i = 1; i < 17; i++)
        {
            GameObject a = new GameObject(i + "");
            a.AddComponent<RectTransform>();
            a.AddComponent<RawImage>();
            a.AddComponent<EventTrigger>();
            a.AddComponent<Selectable>();

            pattern = (RawImage)a.GetComponent<RawImage>();
            a.transform.parent = levels.transform;
            a.transform.localScale = new Vector3(1, 1, 1);
            a.transform.localPosition = new Vector3(25, -25, 186.8f);
            a.transform.localEulerAngles = new Vector3(0, 0, 0);

            if (passed >= i - 1)
            {
                pattern.texture = unlocked;

                GameObject b = new GameObject(i + "b");
                b.AddComponent<RectTransform>();
                b.AddComponent<Text>();
                b.transform.parent = a.transform;
                b.transform.localScale = new Vector3(1, 1, 1);
                b.GetComponent<RectTransform>().sizeDelta = a.GetComponent<RectTransform>().sizeDelta;
                Text text = (Text)b.GetComponent<Text>();
                text.font = ArialFont;
                text.fontSize = 25;
                text.alignment = TextAnchor.MiddleCenter;
                text.color = new Color32(255, 255, 255, 255);
                text.text = i + "";
                b.transform.localPosition = new Vector3(0, 6.9f, 0);
                b.transform.localEulerAngles = new Vector3(0, 0, 0);

                Stars(a, i);

                EventTrigger et = a.GetComponent<EventTrigger>();
                EventTrigger.Entry entry = new EventTrigger.Entry();
                entry.eventID = EventTriggerType.PointerClick;
                entry.callback = new EventTrigger.TriggerEvent();
                UnityAction<BaseEventData> call = null;
                call = new UnityAction<BaseEventData>(Level);

                entry.callback.AddListener(call);
                et.triggers.Add(entry);
            }
            else
            {
                pattern.texture = locked;
                if (dev == 1)
                {
                    EventTrigger et = a.GetComponent<EventTrigger>();
                    EventTrigger.Entry entry = new EventTrigger.Entry();
                    entry.eventID = EventTriggerType.PointerClick;
                    entry.callback = new EventTrigger.TriggerEvent();
                    UnityAction<BaseEventData> call = null;
                    call = new UnityAction<BaseEventData>(Level);

                    entry.callback.AddListener(call);
                    et.triggers.Add(entry);
                }
            }
        }
    }

    void Awake()
    {
        DontDestroyOnLoad(GameObject.Find("Canvas"));
        //DontDestroyOnLoad(GameObject.Find("bike"));
        DontDestroyOnLoad(GameObject.Find("bbike"));
        DontDestroyOnLoad(GameObject.Find("bbike2"));
        DontDestroyOnLoad(GameObject.Find("Start Camera"));
        DontDestroyOnLoad(GameObject.Find("Sounds"));
        DontDestroyOnLoad(GameObject.Find("1uroad"));
        DontDestroyOnLoad(GameObject.Find("loading"));
        DontDestroyOnLoad(GameObject.Find("OpenCamera"));

        Start();
    }

    void Stars(GameObject a, int i)
    {
        GameObject c = new GameObject(i + "c");
        c.AddComponent<RectTransform>();
        c.AddComponent<RawImage>();
        pattern = (RawImage)c.GetComponent<RawImage>();
        c.transform.parent = a.transform;
        c.GetComponent<RectTransform>().sizeDelta = new Vector2(12, 12);
        c.transform.localPosition = new Vector3(-15.37f, -14.6f, 0);
        c.transform.localScale = new Vector3(1, 1, 1);
        c.transform.localEulerAngles = new Vector3(0, 0, 0);
        if (int.Parse(allstars[i]) > 0) {
            pattern.texture = star2;
        } else {
            pattern.texture = star;
        }
        GameObject c2 = new GameObject(i + "c2");
        c2.AddComponent<RectTransform>();
        c2.AddComponent<RawImage>();
        pattern = (RawImage)c2.GetComponent<RawImage>();
        c2.GetComponent<RectTransform>().sizeDelta = new Vector2(12, 12);
        c2.transform.parent = a.transform;
        c2.transform.localPosition = new Vector3(-0.15f, -14.6f, 0);
        c2.transform.localScale = new Vector3(1, 1, 1);
        c2.transform.localEulerAngles = new Vector3(0, 0, 0);
        if (int.Parse(allstars[i]) > 1) {
            pattern.texture = star2;
        } else {
            pattern.texture = star;
        }

        GameObject c3 = new GameObject(i + "c3");
        c3.AddComponent<RectTransform>();
        c3.AddComponent<RawImage>();
        c3.GetComponent<RectTransform>().sizeDelta = new Vector2(12, 12);
        pattern = (RawImage)c3.GetComponent<RawImage>();
        c3.transform.parent = a.transform;
        c3.transform.localPosition = new Vector3(15.05f, -14.6f, 0);
        c3.transform.localScale = new Vector3(1, 1, 1);
        c3.transform.localEulerAngles = new Vector3(0, 0, 0);
        if (int.Parse(allstars[i]) > 2) {
            pattern.texture = star2;
        } else {
            pattern.texture = star;
        }
    }

    void Level(BaseEventData anevenetdata)
    {
        if (audio == 1)
        {
            click.Play();
        }
        levreturn.transform.localScale = new Vector3(0, 0, 0);
        levels.transform.localScale = new Vector3(0, 0, 0);
        nextlevels.transform.localScale = new Vector3(0, 0, 0);
        prevlevels.transform.localScale = new Vector3(0, 0, 0);
        levelsv.transform.localScale = new Vector3(0, 0, 0);
        if (camloading == 1)
        {
            Vector3 posnew = startcamera.transform.localPosition;
            startcamera.transform.localPosition = new Vector3(posnew.x + 1000, posnew.y + 1000, posnew.z + 1000);
            loading.transform.localScale = new Vector3(7, 7, 7);
        }
        else if (camloading == 2)
        {
            canvas2.GetComponent<Canvas>().enabled = false;
            loading2 = GameObject.Find("loading2");
            maincamera.GetComponent<Camera>().farClipPlane = 73;
            loading2.transform.localScale = new Vector3(7, 7, 7);
        }
        if (!SceneManager.GetActiveScene().name.Equals("first"))
        {
            //GC.Collect();
            //Resources.UnloadUnusedAssets();
            SceneManager.LoadScene("first");
        }
        count = 30;
        place = 1;
        levtimer = 2;
        levid = int.Parse(anevenetdata.selectedObject.name);
    }

    void Level2(int id)
    {
        gospeed = 0.000000001f;
        gameover = 10;
        nostop = 0;
        minspeed = 0;
        maxspeed = 1000;
        mson = 0;
        mineon = 0;
        fading = 0;
        alphaf = 0;
        upside = 0;
        wheelupside = 0;
        starsoundp = 0;
        starsoundp2 = 0;
        starsoundp3 = 0;
        exspeed = 0;
        exspeednow = 0;
        devcount = 0;
        keypass = 0;
        timeused = 0;
        slowused = 0;
        ismine = 0;
        amused = 0;
        slow = 0;
        slowcount = 0;
        tused = 0;
        sused = 0;
        icused = 0;
        devtext.GetComponent<Text>().text = "" + devcount;
        levreturn.transform.localScale = new Vector3(0, 0, 0);
        epback.transform.localScale = new Vector3(0, 0, 0);
        slowmotiono.transform.localScale = new Vector3(0, 0, 0);
        mineback.transform.localScale = new Vector3(0, 0, 0);
        ResetFinish();


        if (antimine == 1)
        {
            shockwhite.GetComponent<ParticleSystem>().Stop();
        }
        antimine = 0;

        if (backengine == null)
        {
            backengine = GameObject.Find("backengine").GetComponent<AudioSource>();
            gasengine = GameObject.Find("gasengine").GetComponent<AudioSource>();
            decengine = GameObject.Find("decengine").GetComponent<AudioSource>();
            crash = GameObject.Find("crash").GetComponent<AudioSource>();
            finishsound = GameObject.Find("finishsound").GetComponent<AudioSource>();
            starsound = GameObject.Find("starsound").GetComponent<AudioSource>();
            starsound2 = GameObject.Find("starsound2").GetComponent<AudioSource>();
            starsound3 = GameObject.Find("starsound3").GetComponent<AudioSource>();
        }

        gasengine.volume = 0;
        gasengine.Play();
        decengine.volume = 0;
        decengine.Play();

        minedetect = GameObject.Find("minedetect");
        minenow = GameObject.Find("minenow");
        FinishRot script2 = (FinishRot)minedetect.GetComponent(typeof(FinishRot));
        script2.stage = 0;



        slowback.transform.localScale = new Vector3(0, 0, 0);
        slownum.transform.localScale = new Vector3(0, 0, 0);
        dclock.transform.localScale = new Vector3(1, 1, 1);
        incar.transform.localScale = new Vector3(1, 1, 1);
        //dwheel.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        stop.transform.localScale = new Vector3(1, 1, 1);
        //towtruck.transform.localScale = new Vector3(2, 2, 2);
        level = id;
        Aspect();
        load = 1;
        starpos = 0;
        //Around script = (Around)dwheel.GetComponent(typeof(Around));
        //script.wheelupside = 0;
        wheelupside = 0;

        speedo.GetComponent<RawImage>().texture = speedo2;

        starst[0] = "1";
        starst[1] = "5";
        starst[2] = "10";

        if (id == 1)
        {
            towtruck.transform.localPosition = new Vector3(-676.03f, 137.89f, 514.52f);
            towtruck.transform.localEulerAngles = new Vector3(0, 90, 0);
            count = 21;
            starst[0] = "1";
            starst[1] = "8";
            starst[2] = "12";
            if (played[id] == 0)
            {
                lv1sign.transform.localScale = new Vector3(1, 1, 1);
                notice = 1;
            }
        }
        else if (id == 2)
        {
            towtruck.transform.localPosition = new Vector3(42.8f, 137.89f, -1302.56f);
            towtruck.transform.localEulerAngles = new Vector3(0, 90, 0);
            count = 21;
            starst[0] = "1";
            starst[1] = "5";
            starst[2] = "10";
        }
        else if (id == 3)
        {
            towtruck.transform.localPosition = new Vector3(42.8f, 137.89f, -56.08963f);
            towtruck.transform.localEulerAngles = new Vector3(0, 90, 0);
            count = 26;
            starst[0] = "1";
            starst[1] = "5";
            starst[2] = "10";
            if (played[id] == 0)
            {
                mines.transform.localScale = new Vector3(1, 1, 1);
                notice = 1;
            }
            ismine = 1;
        }
        else if (id == 4)
        {
            towtruck.transform.localPosition = new Vector3(888.38f, 137.89f, -224.67f);
            towtruck.transform.localEulerAngles = new Vector3(0, 90, 0);
            count = 26;
            starst[0] = "1";
            starst[1] = "5";
            starst[2] = "10";
            ismine = 1;
        }
        else if (id == 5)
        {
            towtruck.transform.localPosition = new Vector3(15.29f, 137.89f, 978.02f);
            towtruck.transform.localEulerAngles = new Vector3(0, 90, 0);
            count = 26;
            starst[0] = "1";
            starst[1] = "5";
            starst[2] = "10";
            nostop = 1;
            stop.transform.localScale = new Vector3(0, 0, 0);
            if (played[id] == 0)
            {
                nobreak.transform.localScale = new Vector3(1, 1, 1);
                notice = 1;
                count--;
            }
            ismine = 1;
        }
        else if (id == 6)
        {
            towtruck.transform.localPosition = new Vector3(48.47f, 137.89f, 109.74f);
            towtruck.transform.localEulerAngles = new Vector3(0, 0, 0);
            count = 36;
            starst[0] = "1";
            starst[1] = "10";
            starst[2] = "15";
            nostop = 1;
            stop.transform.localScale = new Vector3(0, 0, 0);
            ismine = 1;
        }
        else if (id == 7)
        {
            towtruck.transform.localPosition = new Vector3(248.8f, 137.89f, -1299.06f);
            towtruck.transform.localEulerAngles = new Vector3(0, 90, 0);
            count = 31;
            nostop = 1;
            stop.transform.localScale = new Vector3(0, 0, 0);
            starst[0] = "1";
            starst[1] = "10";
            starst[2] = "15";
            ismine = 1;
        }
        else if (id == 8)
        {
            towtruck.transform.localPosition = new Vector3(-338.74f, 137.89f, -297.75f);
            towtruck.transform.localEulerAngles = new Vector3(0, 90, 0);
            count = 26;
            nostop = 1;
            stop.transform.localScale = new Vector3(0, 0, 0);
            starst[0] = "1";
            starst[1] = "5";
            starst[2] = "10";
        }
        else if (id == 9)
        {
            towtruck.transform.localPosition = new Vector3(-572.74f, 137.89f, 517.78f);
            towtruck.transform.localEulerAngles = new Vector3(0, 180, 0);
            count = 26;
            nostop = 1;
            stop.transform.localScale = new Vector3(0, 0, 0);
            starst[0] = "1";
            starst[1] = "5";
            starst[2] = "10";
        }
        else if (id == 10)
        {
            towtruck.transform.localPosition = new Vector3(962.9f, 137.89f, -738.4f);
            towtruck.transform.localEulerAngles = new Vector3(0, 0, 0);
            count = 36;
            nostop = 1;
            stop.transform.localScale = new Vector3(0, 0, 0);
            starst[0] = "1";
            starst[1] = "10";
            starst[2] = "15";
        }
        else if (id == 11)
        {
            towtruck.transform.localPosition = new Vector3(-109.1f, 137.89f, -1366.79f);
            towtruck.transform.localEulerAngles = new Vector3(0, 90, 0);
            count = 9999;
            nostop = 1;
            stop.transform.localScale = new Vector3(0, 0, 0);
            minspeed = 100;
            speedo.GetComponent<RawImage>().texture = minspeed100;
            if (played[id] == 0)
            {
                minspeedtext.GetComponent<Text>().text = minspeed + "";
                minspeeds.transform.localScale = new Vector3(1, 1, 1);
                notice = 1;
                count--;
            }
        }
        else if (id == 12)
        {
            towtruck.transform.localPosition = new Vector3(-755.47f, 137.89f, 759.39f);
            towtruck.transform.localEulerAngles = new Vector3(0, 0, 0);
            count = 9999;
            nostop = 1;
            stop.transform.localScale = new Vector3(0, 0, 0);
            minspeed = 120;
            speedo.GetComponent<RawImage>().texture = minspeed120;
            if (played[id] == 0)
            {
                minspeedtext.GetComponent<Text>().text = minspeed + "";
                minspeeds.transform.localScale = new Vector3(1, 1, 1);
                notice = 1;
                count--;
            }
        }
        else if (id == 13)
        {
            towtruck.transform.localPosition = new Vector3(933.16f, 137.89f, -456.65f);
            towtruck.transform.localEulerAngles = new Vector3(0, 0, 0);
            count = 9999;
            minspeed = 120;
            speedo.GetComponent<RawImage>().texture = minspeed120;
            if (played[id] == 0)
            {
                minspeedtext.GetComponent<Text>().text = minspeed + "";
                minspeeds.transform.localScale = new Vector3(1, 1, 1);
                notice = 1;
                count--;
            }
        }
        else if (id == 14)
        {
            towtruck.transform.localPosition = new Vector3(105.45f, 137.89f, -1417f);
            towtruck.transform.localEulerAngles = new Vector3(0, 90, 0);
            count = 9999;
            minspeed = 120;
            speedo.GetComponent<RawImage>().texture = minspeed120;
            if (played[id] == 0)
            {
                minspeedtext.GetComponent<Text>().text = minspeed + "";
                minspeeds.transform.localScale = new Vector3(1, 1, 1);
                notice = 1;
                count--;
            }
        }
        else if (id == 15)
        {
            towtruck.transform.localPosition = new Vector3(127.7f, 137.89f, 1182.9f);
            towtruck.transform.localEulerAngles = new Vector3(0, 90, 0);
            count = 9999;
            minspeed = 120;
            speedo.GetComponent<RawImage>().texture = minspeed120;
            if (played[id] == 0)
            {
                minspeedtext.GetComponent<Text>().text = minspeed + "";
                minspeeds.transform.localScale = new Vector3(1, 1, 1);
                notice = 1;
                count--;
            }
        }
        else if (id == 16)
        {
            towtruck.transform.localPosition = new Vector3(1006.72f, 137.89f, -616.91f);
            towtruck.transform.localEulerAngles = new Vector3(0, 90, 0);
            count = 9999;
            minspeed = 120;
            speedo.GetComponent<RawImage>().texture = minspeed120;
            if (played[id] == 0)
            {
                minspeedtext.GetComponent<Text>().text = minspeed + "";
                minspeeds.transform.localScale = new Vector3(1, 1, 1);
                notice = 1;
                count--;
            }
            starst[0] = "1";
            starst[1] = "5";
            starst[2] = "10";
            ismine = 1;
        }
        else if (id == 17)
        {
            towtruck.transform.localPosition = new Vector3(-664.6f, 137.89f, 586.98f);
            towtruck.transform.localEulerAngles = new Vector3(0, 90, 0);
            count = 9999;
            minspeed = 120;
            speedo.GetComponent<RawImage>().texture = minspeed120;
            if (played[id] == 0)
            {
                minspeedtext.GetComponent<Text>().text = minspeed + "";
                minspeeds.transform.localScale = new Vector3(1, 1, 1);
                notice = 1;
                count--;
            }
            ismine = 1;
        }
        else if (id == 18)
        {
            towtruck.transform.localPosition = new Vector3(30.4f, 137.89f, -300.77f);
            towtruck.transform.localEulerAngles = new Vector3(0, 90, 0);
            count = 9999;
            starst[0] = "1";
            starst[1] = "15";
            starst[2] = "20";
            minspeed = 80;
            speedo.GetComponent<RawImage>().texture = minspeed80;
            upside = 1;
            wheelupside = 1;
            if (played[id] == 0)
            {
                upsideo.transform.localScale = new Vector3(1, 1, 1);
                notice = 1;
                count--;
            }
        }
        else if (id == 19)
        {
            towtruck.transform.localPosition = new Vector3(760.3f, 137.89f, -272.59f);
            towtruck.transform.localEulerAngles = new Vector3(0, 180, 0);
            count = 9999;
            starst[0] = "1";
            starst[1] = "15";
            starst[2] = "20";
            minspeed = 80;
            speedo.GetComponent<RawImage>().texture = minspeed80;
            upside = 1;
            wheelupside = 1;
            if (played[id] == 0)
            {
                upsideo.transform.localScale = new Vector3(1, 1, 1);
                notice = 1;
                count--;
            }
        }
        else if (id == 20)
        {
            towtruck.transform.localPosition = new Vector3(276.26f, 137.89f, 899.98f);
            towtruck.transform.localEulerAngles = new Vector3(0, 90, 0);
            count = 51;
            starst[0] = "1";
            starst[1] = "15";
            starst[2] = "20";
            minspeed = 80;
            speedo.GetComponent<RawImage>().texture = minspeed80;
            upside = 1;
            wheelupside = 1;
            if (played[id] == 0)
            {
                upsideo.transform.localScale = new Vector3(1, 1, 1);
                notice = 1;
                count--;
            }
        }
        else if (id == 21)
        {
            towtruck.transform.localPosition = new Vector3(93.44f, 137.89f, -1170.6f);
            towtruck.transform.localEulerAngles = new Vector3(0, 90, 0);
            count = 9999;
            minspeed = 40;
            speedo.GetComponent<RawImage>().texture = minspeed40;
            if (played[id] == 0)
            {
                minspeedtext.GetComponent<Text>().text = minspeed + "";
                minspeeds.transform.localScale = new Vector3(1, 1, 1);
                notice = 1;
                count--;
            }
            starst[0] = "1";
            starst[1] = "15";
            starst[2] = "20";
            ismine = 1;
        }
        else if (id == 22)
        {
            towtruck.transform.localPosition = new Vector3(781.4f, 137.89f, -749.3f);
            towtruck.transform.localEulerAngles = new Vector3(0, 90, 0);
            count = 9999;
            minspeed = 50;
            speedo.GetComponent<RawImage>().texture = minspeed50;
            if (played[id] == 0)
            {
                minspeedtext.GetComponent<Text>().text = minspeed + "";
                minspeeds.transform.localScale = new Vector3(1, 1, 1);
                notice = 1;
                count--;
            }
            ismine = 1;
        }
        else if (id == 23)
        {
            towtruck.transform.localPosition = new Vector3(-795.6f, 137.89f, 864.78f);
            towtruck.transform.localEulerAngles = new Vector3(0, 90, 0);
            count = 9999;
            minspeed = 60;
            speedo.GetComponent<RawImage>().texture = minspeed60;
            if (played[id] == 0)
            {
                minspeedtext.GetComponent<Text>().text = minspeed + "";
                minspeeds.transform.localScale = new Vector3(1, 1, 1);
                notice = 1;
                count--;
            }
            ismine = 1;
        }
        else if (id == 24)
        {
            towtruck.transform.localPosition = new Vector3(-71.32f, 137.89f, -1169.54f);
            towtruck.transform.localEulerAngles = new Vector3(0, 180, 0);
            count = 9999;
            minspeed = 40;
            speedo.GetComponent<RawImage>().texture = minspeed40;
            if (played[id] == 0)
            {
                minspeedtext.GetComponent<Text>().text = minspeed + "";
                minspeeds.transform.localScale = new Vector3(1, 1, 1);
                notice = 1;
                count--;
            }
            ismine = 1;
        }
        else if (id == 25)
        {
            towtruck.transform.localPosition = new Vector3(163.44f, 137.89f, 128.14f);
            towtruck.transform.localEulerAngles = new Vector3(0, 180, 0);
            count = 61;
            starst[0] = "1";
            starst[1] = "5";
            starst[2] = "10";
            exspeed = 30;
            speedo.GetComponent<RawImage>().texture = redspeed30;
            //speedo.GetComponent<RawImage>().texture = minspeed60;
            if (played[id] == 0)
            {
                slowertext.GetComponent<Text>().text = exspeed + "";
                slower.transform.localScale = new Vector3(1, 1, 1);
                notice = 1;
                count--;
            }
            ismine = 1;
        }
        else if (id == 26)
        {
            towtruck.transform.localPosition = new Vector3(-671.04f, 137.89f, 881.99f);
            towtruck.transform.localEulerAngles = new Vector3(0, 90, 0);
            count = 61;
            starst[0] = "1";
            starst[1] = "5";
            starst[2] = "10";
            exspeed = 30;
            speedo.GetComponent<RawImage>().texture = redspeed30;
            //speedo.GetComponent<RawImage>().texture = minspeed60;
            if (played[id] == 0)
            {
                slowertext.GetComponent<Text>().text = exspeed + "";
                slower.transform.localScale = new Vector3(1, 1, 1);
                notice = 1;
                count--;
            }
            ismine = 1;
        }
        else if (id == 27)
        {
            towtruck.transform.localPosition = new Vector3(1051.91f, 137.89f, -749.1f);
            towtruck.transform.localEulerAngles = new Vector3(0, 180, 0);
            count = 61;
            starst[0] = "1";
            starst[1] = "5";
            starst[2] = "10";
            exspeed = 30;
            speedo.GetComponent<RawImage>().texture = redspeed30;
            //speedo.GetComponent<RawImage>().texture = minspeed60;
            if (played[id] == 0)
            {
                slowertext.GetComponent<Text>().text = exspeed + "";
                slower.transform.localScale = new Vector3(1, 1, 1);
                notice = 1;
                count--;
            }
            ismine = 1;
        }
        else if (id == 28)
        {
            towtruck.transform.localPosition = new Vector3(17.65f, 137.89f, -1071.35f);
            towtruck.transform.localEulerAngles = new Vector3(0, 180, 0);
            count = 61;
            starst[0] = "1";
            starst[1] = "5";
            starst[2] = "10";
            exspeed = 30;
            speedo.GetComponent<RawImage>().texture = redspeed30;
            //speedo.GetComponent<RawImage>().texture = minspeed60;
            if (played[id] == 0)
            {
                slowertext.GetComponent<Text>().text = exspeed + "";
                slower.transform.localScale = new Vector3(1, 1, 1);
                notice = 1;
                count--;
            }
            ismine = 1;
        }
        else if (id == 29)
        {
            towtruck.transform.localPosition = new Vector3(-288.43f, 137.89f, -28.92f);
            towtruck.transform.localEulerAngles = new Vector3(0, 180, 0);
            count = 61;
            starst[0] = "1";
            starst[1] = "5";
            starst[2] = "10";
            exspeed = 40;
            speedo.GetComponent<RawImage>().texture = redspeed40;
            //speedo.GetComponent<RawImage>().texture = minspeed60;
            if (played[id] == 0)
            {
                notice++;
                slowertext.GetComponent<Text>().text = exspeed + "";
                slower.transform.localScale = new Vector3(1, 1, 1);
            }
            upside = 1;
            wheelupside = 1;
            if (played[id] == 0)
            {
                notice++;
                upsideo.transform.localScale = new Vector3(1, 1, 1);
            }
            ismine = 1;
        }
        else if (id == 30)
        {
            towtruck.transform.localPosition = new Vector3(261.51f, 137.89f, 250.31f);
            towtruck.transform.localEulerAngles = new Vector3(0, 180, 0);
            count = 61;
            starst[0] = "1";
            starst[1] = "5";
            starst[2] = "10";
            exspeed = 30;
            speedo.GetComponent<RawImage>().texture = redspeed30;
            //speedo.GetComponent<RawImage>().texture = minspeed60;
            if (played[id] == 0)
            {
                notice++;
                slowertext.GetComponent<Text>().text = exspeed + "";
                slower.transform.localScale = new Vector3(1, 1, 1);
            }
            upside = 1;
            wheelupside = 1;
            if (played[id] == 0)
            {
                notice++;
                upsideo.transform.localScale = new Vector3(1, 1, 1);
            }
            ismine = 1;
        }
        else if (id == 31)
        {
            towtruck.transform.localPosition = new Vector3(253.7f, 137.89f, -1138.28f);
            towtruck.transform.localEulerAngles = new Vector3(0, 270, 0);
            count = 61;
            starst[0] = "1";
            starst[1] = "5";
            starst[2] = "10";
            exspeed = 10;
            speedo.GetComponent<RawImage>().texture = redspeed10;
            //speedo.GetComponent<RawImage>().texture = minspeed60;
            if (played[id] == 0)
            {
                notice++;
                slowertext.GetComponent<Text>().text = exspeed + "";
                slower.transform.localScale = new Vector3(1, 1, 1);
            }
            upside = 1;
            wheelupside = 1;
            if (played[id] == 0)
            {
                notice++;
                upsideo.transform.localScale = new Vector3(1, 1, 1);
            }
            ismine = 1;
        }
        else if (id == 32)
        {
            towtruck.transform.localPosition = new Vector3(-796.4f, 137.89f, 533f);
            towtruck.transform.localEulerAngles = new Vector3(0, 180, 0);
            count = 61;
            starst[0] = "1";
            starst[1] = "5";
            starst[2] = "10";
            exspeed = 10;
            speedo.GetComponent<RawImage>().texture = redspeed10;
            //speedo.GetComponent<RawImage>().texture = minspeed60;
            if (played[id] == 0)
            {
                notice++;
                slowertext.GetComponent<Text>().text = exspeed + "";
                slower.transform.localScale = new Vector3(1, 1, 1);
            }
            upside = 1;
            wheelupside = 1;
            if (played[id] == 0)
            {
                notice++;
                upsideo.transform.localScale = new Vector3(1, 1, 1);
            }
            ismine = 1;
        }

        if (minspeed > 0)
        {
            nostop = 1;
            stop.transform.localScale = new Vector3(0, 0, 0);
        }
        PosStar(3);
        maincamera.transform.localEulerAngles = new Vector3(90, towtruck.transform.localEulerAngles.y, towtruck.transform.localEulerAngles.z);
        maincamera.transform.localPosition = new Vector3(towtruck.transform.localPosition.x + mtx, towtruck.transform.localPosition.y + mty, towtruck.transform.localPosition.z);

        float mx = towtruck.transform.position.x + 59;
        float mx2 = towtruck.transform.position.x + 17;
        float mz = towtruck.transform.position.z + 13;

        if ((int)maincamera.transform.localEulerAngles.y == 0)
        {
            maincamera.transform.position = new Vector3(mx, maincamera.transform.position.y, towtruck.transform.position.z + 13);
        }
        else if ((int)maincamera.transform.localEulerAngles.y == 90)
        {
            maincamera.transform.position = new Vector3(mx2, maincamera.transform.position.y, towtruck.transform.position.z);
        }
        else if ((int)maincamera.transform.localEulerAngles.y == 180)
        {
            maincamera.transform.position = new Vector3(mx, maincamera.transform.position.y, towtruck.transform.position.z - 13);
        }
        else if ((int)maincamera.transform.localEulerAngles.y == 270)
        {
            maincamera.transform.position = new Vector3(towtruck.transform.position.x - 9, maincamera.transform.position.y, towtruck.transform.position.z);
        }
        maincamera2.transform.position = towtruck.transform.position + offset;
        //maincamera.GetComponent<Camera>().orthographicSize = 21.37f;
        tire.transform.localEulerAngles = new Vector3(0, 0, 0);
        tire2.transform.localEulerAngles = new Vector3(0, 0, 0);
        tire3.transform.localEulerAngles = new Vector3(0, 0, 0);
        tire4.transform.localEulerAngles = new Vector3(0, 0, 0);

        tire.transform.localPosition = new Vector3(-0.82f, -1.07f, -1.3f);
        tire2.transform.localPosition = new Vector3(1.09f, -1.07f, -1.3f);
        tire3.transform.localPosition = new Vector3(tire3.transform.localPosition.x, -0.009681702f, tire3.transform.localPosition.z);
        tire4.transform.localPosition = new Vector3(tire4.transform.localPosition.x, -0.009681702f, tire4.transform.localPosition.z);

        tire.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.None;
        tire2.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.None;
        tire3.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.None;
        tire4.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.None;

        print("ok");
        canvas2.GetComponent<Canvas>().enabled = false;
        loading2 = GameObject.Find("loading2");
        maincamera.GetComponent<Camera>().farClipPlane = 73;
        loading2.transform.localScale = new Vector3(7, 7, 7);
        maincamera.GetComponent<Camera>().enabled = true;
        startcamera.GetComponent<Camera>().enabled = false;
        camloading = 2;

        Aspect();

        engineon = 0;

        dcount = count;
        Finish script3 = (Finish)tire.GetComponent(typeof(Finish));
        script3.game = 1;
        script3 = (Finish)tire2.GetComponent(typeof(Finish));
        script3.game = 1;
        script3 = (Finish)tire3.GetComponent(typeof(Finish));
        script3.game = 1;
        script3 = (Finish)tire4.GetComponent(typeof(Finish));
        script3.game = 1;

        //RColliders ();
        SetTimes();
        offset = maincamera2.transform.position - towtruck.transform.position;

        //script.play = 1;

        if (count < 9000)
        {
            dclock.transform.GetComponent<Text>().text = (int)count + "";
        }
        else
        {
            dclock.transform.GetComponent<Text>().text = "";
        }

        played[id] = 1;
        gospeed = 0.000000001f;

        if (audio == 1)
        {
            backengine.Play();
        }

        pstart.GetComponent<Text>().text = starst[0];
        pstart2.GetComponent<Text>().text = starst[1];
        pstart3.GetComponent<Text>().text = starst[2];

        gameover = 0;
        finished = 0;

        extracount = PlayerPrefs.GetInt("extracount");
        if (extracount > 0 && count < 9000)
        {
            icused++;
            PlaceIC(epback);
            epback.transform.localScale = new Vector3(1, 1, 1);
        }

        slowmotion = PlayerPrefs.GetInt("slowmotion");
        if (slowmotion > 0)
        {
            icused++;
            PlaceIC(slowmotiono);
            slowmotiono.transform.localScale = new Vector3(1, 1, 1);
        }

        int antiminecount = PlayerPrefs.GetInt("antiminecount");
        if (antiminecount > 0 && ismine == 1)
        {
            icused++;
            PlaceIC(mineback);
            mineback.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    void PlaceIC(GameObject icobject)
    {
        Vector3 icpos = icobject.transform.localPosition;
        if (icused == 1)
        {
            icobject.transform.localPosition = new Vector3(249.5f, icpos.y, icpos.z);
        }
        else if (icused == 2)
        {
            icobject.transform.localPosition = new Vector3(174.3f, icpos.y, icpos.z);
        }
        else if (icused == 3)
        {
            icobject.transform.localPosition = new Vector3(99f, icpos.y, icpos.z);
        }
    }

    public string GetHtmlFromUri(string resource)
    {
        string html = string.Empty;
        HttpWebRequest req = (HttpWebRequest)WebRequest.Create(resource);
        try
        {
            using (HttpWebResponse resp = (HttpWebResponse)req.GetResponse())
            {
                bool isSuccess = (int)resp.StatusCode < 299 && (int)resp.StatusCode >= 200;
                if (isSuccess)
                {
                    using (StreamReader reader = new StreamReader(resp.GetResponseStream()))
                    {
                        //We are limiting the array to 80 so we don't have
                        //to parse the entire html document feel free to 
                        //adjust (probably stay under 300)
                        char[] cs = new char[80];
                        reader.Read(cs, 0, cs.Length);
                        foreach (char ch in cs)
                        {
                            html += ch;
                        }
                    }
                }
            }
        }
        catch
        {
            return "";
        }
        return html;
    }

    bool CheckCoins()
    {
        int no = 0;
        string HtmlText = GetHtmlFromUri("http://google.com");
        if (HtmlText == "")
        {
            no = 1;
            StartCoroutine("getTime");
        }
        if (no == 1)
        {
            popupni.transform.localScale = new Vector3(1, 1, 1);
            return false;
        }
        int h = int.Parse(ctime[3]);
        int m = int.Parse(ctime[4]);
        int s = int.Parse(ctime[5]);
        int d = int.Parse(ctime[2]);
        int mo = int.Parse(ctime[1]);
        int y = int.Parse(ctime[0]);
        if (yescoins == 1)
        {
            return true;
        }
        if (d != day || mo != month || y != year)
        {
            return true;
        }
        else
        {
            if (h != hour)
            {
                return true;
            }
            else
            {
                if (minute - 15 > m)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        return true;
    }

    bool CheckCoins2()
    {
        int no = 0;
        string HtmlText = GetHtmlFromUri("http://google.com");
        if (HtmlText == "")
        {
            no = 1;
            StartCoroutine("getTime2");
        }
        if (no == 1)
        {
            popupni.transform.localScale = new Vector3(1, 1, 1);
            return false;
        }

        int h = int.Parse(ctime2[3]);
        int m = int.Parse(ctime2[4]);
        int s = int.Parse(ctime2[5]);
        int d = int.Parse(ctime2[2]);
        int mo = int.Parse(ctime2[1]);
        int y = int.Parse(ctime2[0]);

        if (yescoins2 == 1)
        {
            return true;
        }

        if (d != day2 || mo != month2 || y != year2)
        {
            return true;
        }
        else
        {
            if (h != hour2)
            {
                return true;
            }
            else
            {
                if (minute2 - 15 > m)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        return true;
    }

    IEnumerator checkInternetConnection(Action<bool> action)
    {
        WWW www = new WWW("http://google.com");
        yield return www;
        if (www.error != null)
        {
            action(false);
        }
        else
        {
            action(true);
        }
    }

    IEnumerator getTime()
    {
        UnityWebRequest www = UnityWebRequest.Get("http://www.tmkapps.com/gettime.php");
        yield return www.SendWebRequest();

        if (www.isNetworkError)
        {
            Debug.Log(www.error);
        }
        else
        {
            // Show results as text
            Debug.Log(www.downloadHandler.text);

            // Or retrieve results as binary data
            //byte[] results = www.downloadHandler.data;

            String ts = www.downloadHandler.text;
            ctime = ts.Split("/"[0]);
            //Debug.Log(ts);
            if (!CheckCoins())
            {
                cotimer = 1;
                CoinTime();
                freecoins.transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }

    IEnumerator getTime2()
    {
        UnityWebRequest www = UnityWebRequest.Get("http://www.tmkapps.com/gettime.php");
        yield return www.SendWebRequest();

        if (www.isNetworkError)
        {
            Debug.Log(www.error);
        }
        else
        {

            String ts = www.downloadHandler.text;
            ctime2 = ts.Split("/"[0]);
            //Debug.Log(ts);
            if (!CheckCoins2())
            {
                cotimer2 = 1;
                CoinTime3();
                videocoins.transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }

    void CoinTime()
    {
        int h = int.Parse(ctime[3]);
        int m = int.Parse(ctime[4]);
        int s = int.Parse(ctime[5]);
        int d = int.Parse(ctime[2]);
        int mo = int.Parse(ctime[1]);
        int y = int.Parse(ctime[0]);
        DateTime departure = new DateTime(y, mo, d, h, m, s);
        DateTime arrival = new DateTime(year, month, day, hour, minute, second);
        TimeSpan travelTime = departure - arrival;

        int totals = 15 * 60 - (travelTime.Seconds + travelTime.Minutes * 60);
        string minutes = Mathf.Floor(totals / 60).ToString("00");
        string seconds = (totals % 60).ToString("00");

        freecoins.GetComponent<Text>().text = minutes + ":" + seconds;
        fsc = int.Parse(minutes) * 60 + int.Parse(seconds);
    }

    void CoinTime2()
    {
        if (fsc > 0)
        {
            fsc -= Time.deltaTime;
            string minutes = Mathf.Floor(fsc / 60).ToString("00");
            string seconds = (fsc % 60).ToString("00");

            freecoins.GetComponent<Text>().text = minutes + ":" + seconds;
        }
        else
        {
            freecoins.transform.localScale = new Vector3(0, 0, 0);
            yescoins = 1;
            cotimer = 0;
        }
    }

    void CoinTime3()
    {
        int h = int.Parse(ctime2[3]);
        int m = int.Parse(ctime2[4]);
        int s = int.Parse(ctime2[5]);
        int d = int.Parse(ctime2[2]);
        int mo = int.Parse(ctime2[1]);
        int y = int.Parse(ctime2[0]);
        DateTime departure = new DateTime(y, mo, d, h, m, s);
        DateTime arrival = new DateTime(year2, month2, day2, hour2, minute2, second2);
        TimeSpan travelTime = departure - arrival;

        int totals = 15 * 60 - (travelTime.Seconds + travelTime.Minutes * 60);
        string minutes = Mathf.Floor(totals / 60).ToString("00");
        string seconds = (totals % 60).ToString("00");

        videocoins.GetComponent<Text>().text = minutes + ":" + seconds;
        fsc2 = int.Parse(minutes) * 60 + int.Parse(seconds);
    }

    void CoinTime4()
    {
        if (fsc2 > 0)
        {
            fsc2 -= Time.deltaTime;
            string minutes = Mathf.Floor(fsc2 / 60).ToString("00");
            string seconds = (fsc2 % 60).ToString("00");

            videocoins.GetComponent<Text>().text = minutes + ":" + seconds;
        }
        else
        {
            videocoins.transform.localScale = new Vector3(0, 0, 0);
            yescoins2 = 1;
            cotimer2 = 0;
        }
    }


    public void FreeCoins()
    {
        if (CheckCoins())
        {
            GiveCoins();
        }
    }

    public void VideoCoins()
    {
        if (CheckCoins2())
        {
            GiveCoins2();
        }
    }

    void GiveCoins()
    {
        yescoins = 0;
        int h = int.Parse(ctime[3]);
        int m = int.Parse(ctime[4]);
        int s = int.Parse(ctime[5]);
        int d = int.Parse(ctime[2]);
        int mo = int.Parse(ctime[1]);
        int y = int.Parse(ctime[0]);

        hour = h;
        minute = m;
        second = s;
        day = d;
        month = mo;
        year = y;
        PlayerPrefs.SetInt("hour", h);
        PlayerPrefs.SetInt("minute", m);
        PlayerPrefs.SetInt("second", s);
        PlayerPrefs.SetInt("day", d);
        PlayerPrefs.SetInt("month", mo);
        PlayerPrefs.SetInt("year", y);

        int r = UnityEngine.Random.Range(0, 3);
        int r2;
        if (r < 2)
        {
            r2 = UnityEngine.Random.Range(2, 5);
        }
        else
        {
            r2 = UnityEngine.Random.Range(5, 11);
        }

        int coins = PlayerPrefs.GetInt("coins");
        coins += r2;
        PlayerPrefs.SetInt("coins", coins);

        pmessagec.GetComponent<Text>().text = r2 + "";
        popupcoin.transform.localScale = new Vector3(1, 1, 1);

        cotimer = 1;
        CoinTime();
        freecoins.transform.localScale = new Vector3(1, 1, 1);

        if (atshop == 1)
        {
            cointext.GetComponent<Text>().text = coins + "";
        }
    }

    void GiveCoins2()
    {
        yescoins2 = 0;
        int h = int.Parse(ctime2[3]);
        int m = int.Parse(ctime2[4]);
        int s = int.Parse(ctime2[5]);
        int d = int.Parse(ctime2[2]);
        int mo = int.Parse(ctime2[1]);
        int y = int.Parse(ctime2[0]);

        hour2 = h;
        minute2 = m;
        second2 = s;
        day2 = d;
        month2 = mo;
        year2 = y;
        PlayerPrefs.SetInt("hour2", h);
        PlayerPrefs.SetInt("minute2", m);
        PlayerPrefs.SetInt("second2", s);
        PlayerPrefs.SetInt("day2", d);
        PlayerPrefs.SetInt("month2", mo);
        PlayerPrefs.SetInt("year2", y);


        cotimer2 = 1;
        CoinTime3();
        videocoins.transform.localScale = new Vector3(1, 1, 1);
    }

    public void OkPopUp()
    {
        popupcoin.transform.localScale = new Vector3(0, 0, 0);
        popupni.transform.localScale = new Vector3(0, 0, 0);
    }

    public void OkBuyMenu()
    {
        buymenu.transform.localScale = new Vector3(0, 0, 0);
    }

    public void InfoPopUp()
    {
        shopinfo.transform.localScale = new Vector3(0, 0, 0);
    }

    public void PurchasePopUp()
    {
        Purchase.transform.localScale = new Vector3(0, 0, 0);
    }

    public void PurchaseFPopUp()
    {
        PurchaseF.transform.localScale = new Vector3(0, 0, 0);
    }

    public void UpdatedPopUp()
    {
        supdated.transform.localScale = new Vector3(0, 0, 0);
    }

    public void CreditsPopUp()
    {
        credits.transform.localScale = new Vector3(0, 0, 0);
    }

    public void ConnectPopup()
    {
        connectmsg.transform.localScale = new Vector3(0, 0, 0);
    }

    public void ConnectPopupShow()
    {
        connectmsg.transform.localScale = new Vector3(1, 1, 1);
    }


    public void ConnectPlay()
    {
#if UNITY_ANDROID
        ConnectPopup();
        gplay = 1;
        PlayerPrefs.SetInt("gplay", 1);
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().EnableSavedGames().Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();
        Social.localUser.Authenticate(success => {
            if (success)
            {
                signin = 1;
                //artext.GetComponent<Text>().text =  "logged";
                if (firsttime == 1)
                {
                    LoadCloud();
                    firsttime = 0;
                }
            }
            else
            {
                //artext.GetComponent<Text>().text = "not logged";

            }
        });
#endif
    }

    public void Credits()
    {
        credits.transform.localScale = new Vector3(1, 1, 1);
    }

    public void GodMode()
    {
        if(gm == 0)
        {
            gm = 1;
        }
        else
        {
            gm = 0;
        }
    }

    public void ResetLevels()
    {
        string scores = "0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0";
        allscores = scores.Split(":"[0]);
        PlayerPrefs.SetString("scores", scores);

        allscores = scores.Split(":"[0]);
        string stars = "0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0";
        allstars = stars.Split(":"[0]);
        PlayerPrefs.SetString("stars", stars);
        passed = 0;
        PlayerPrefs.SetInt("passed",0);
        if (signin == 1)
        {
            SaveCloud();
        }

        AllScores();
        AllStars();
        alevels = 0;
        foreach (Transform child in levels.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        //Application.Quit();
    }


    void PosStar(int id)
    {
        if (count < 9000)
        {
            if (starpos == id)
            {
                return;
            }
            starpos = id;
            if (id == 3)
            {
                //pstar.transform.localPosition = new Vector3(-36.2f, -36.5f, 0f);
                //pstar2.transform.localPosition = new Vector3(-3.099996f, -36.5f, 0f);

                pstar.transform.localScale = new Vector3(1, 1, 1);
                pstar2.transform.localScale = new Vector3(1, 1, 1);
                pstar3.transform.localScale = new Vector3(1, 1, 1);

            }
            else if (id == 2)
            {
                //pstar.transform.localPosition = new Vector3(-20.5f, -36.5f, 0f);
                //pstar2.transform.localPosition = new Vector3(12.6f, -36.5f, 0f);

                pstar.transform.localScale = new Vector3(1, 1, 1);
                pstar2.transform.localScale = new Vector3(1, 1, 1);
                pstar3.transform.localScale = new Vector3(0, 0, 0);
            }
            else if (id == 1)
            {
                //pstar.transform.localPosition = new Vector3(-3.7f, -36.5f, 0f);

                pstar.transform.localScale = new Vector3(1, 1, 1);
                pstar2.transform.localScale = new Vector3(0, 0, 0);
                pstar3.transform.localScale = new Vector3(0, 0, 0);
            }
        }
        else
        {
            pstar.transform.localScale = new Vector3(0, 0, 0);
            pstar2.transform.localScale = new Vector3(0, 0, 0);
            pstar3.transform.localScale = new Vector3(0, 0, 0);
        }
    }

    void Active()
    {
        if (active == 1)
        {
            active = 0;
            /*level1.SetActive(true);
            //LTerrain1.SetActive(true);
            level2.SetActive(true);
            LTerrain2.SetActive(true);
            level3.SetActive(true);
            LTerrain3.SetActive(true);
            level4.SetActive(true);
            LTerrain4.SetActive(true);
            level5.SetActive(true);
            LTerrain5.SetActive(true);
            level6.SetActive(true);
            LTerrain6.SetActive(true);
            level7.SetActive(true);
            LTerrain7.SetActive(true);
            level8.SetActive(true);
            LTerrain8.SetActive(true);
            level9.SetActive(true);
            LTerrain9.SetActive(true);
            level10.SetActive(true);
            LTerrain10.SetActive(true);
            level11.SetActive(true);
            LTerrain11.SetActive(true);
            level12.SetActive(true);
            LTerrain12.SetActive(true);
            level13.SetActive(true);
            LTerrain13.SetActive(true);
            level14.SetActive(true);
            LTerrain14.SetActive(true);
            level15.SetActive(true);
            LTerrain15.SetActive(true);
            level16.SetActive(true);
            LTerrain16.SetActive(true);
            level17.SetActive(true);
            LTerrain17.SetActive(true);
            level18.SetActive(true);
            LTerrain18.SetActive(true);
            level19.SetActive(true);
            LTerrain19.SetActive(true);
            level20.SetActive(true);
            LTerrain20.SetActive(true);*/
        }
    }

    void UnActive()
    {
        if (active == 0)
        {
            active = 1;
            /*level1.SetActive(false);
            //LTerrain1.SetActive(false);
            level2.SetActive(false);
            LTerrain2.SetActive(false);
            level3.SetActive(false);
            LTerrain3.SetActive(false);
            level4.SetActive(false);
            LTerrain4.SetActive(false);
            level5.SetActive(false);
            LTerrain5.SetActive(false);
            level6.SetActive(false);
            LTerrain6.SetActive(false);
            level7.SetActive(false);
            LTerrain7.SetActive(false);
            level8.SetActive(false);
            LTerrain8.SetActive(false);
            level9.SetActive(false);
            LTerrain9.SetActive(false);
            level10.SetActive(false);
            LTerrain10.SetActive(false);
            level11.SetActive(false);
            LTerrain11.SetActive(false);
            level12.SetActive(false);
            LTerrain12.SetActive(false);
            level13.SetActive(false);
            LTerrain13.SetActive(false);
            level14.SetActive(false);
            LTerrain14.SetActive(false);
            level15.SetActive(false);
            LTerrain15.SetActive(false);
            level16.SetActive(false);
            LTerrain16.SetActive(false);
            level17.SetActive(false);
            LTerrain17.SetActive(false);
            level18.SetActive(false);
            LTerrain18.SetActive(false);
            level19.SetActive(false);
            LTerrain19.SetActive(false);
            level20.SetActive(false);
            LTerrain20.SetActive(false);*/
        }
    }

    void LevelTerrain(int id)
    {
        /*if(id == 1)
        {
            level1.SetActive(true);
            //LTerrain1.SetActive(true);
        }
        else if (id == 2)
        {
            level2.SetActive(true);
            LTerrain2.SetActive(true);
        }
        else if (id == 3)
        {
            level3.SetActive(true);
            LTerrain3.SetActive(true);
        }
        else if (id == 4)
        {
            level4.SetActive(true);
            LTerrain4.SetActive(true);
        }
        else if (id == 5)
        {
            level5.SetActive(true);
            LTerrain5.SetActive(true);
        }
        else if (id == 6)
        {
            level6.SetActive(true);
            LTerrain6.SetActive(true);
        }
        else if (id == 7)
        {
            level7.SetActive(true);
            LTerrain7.SetActive(true);
        }
        else if (id == 8)
        {
            level8.SetActive(true);
            LTerrain8.SetActive(true);
        }
        else if (id == 9)
        {
            level9.SetActive(true);
            LTerrain9.SetActive(true);
        }
        else if (id == 10)
        {
            level10.SetActive(true);
            LTerrain10.SetActive(true);
        }
        else if (id == 11)
        {
            level11.SetActive(true);
            LTerrain11.SetActive(true);
        }
        else if (id == 12)
        {
            level12.SetActive(true);
            LTerrain12.SetActive(true);
        }
        else if (id == 13)
        {
            level13.SetActive(true);
            LTerrain13.SetActive(true);
        }
        else if (id == 14)
        {
            level14.SetActive(true);
            LTerrain14.SetActive(true);
        }
        else if (id == 15)
        {
            level15.SetActive(true);
            LTerrain15.SetActive(true);
        }
        else if (id == 16)
        {
            level16.SetActive(true);
            LTerrain16.SetActive(true);
        }
        else if (id == 17)
        {
            level17.SetActive(true);
            LTerrain17.SetActive(true);
        }
        else if (id == 18)
        {
            level18.SetActive(true);
            LTerrain18.SetActive(true);
        }
        else if (id == 19)
        {
            level19.SetActive(true);
            LTerrain19.SetActive(true);
        }
        else if (id == 20)
        {
            level20.SetActive(true);
            LTerrain20.SetActive(true);
        }*/
    }

    void SetTimes()
    {
        n0.GetComponent<Text>().text = "" + count;
        n1.GetComponent<Text>().text = "" + count / 6;
        n2.GetComponent<Text>().text = "" + count / 3;
        n3.GetComponent<Text>().text = "" + count / 2;
    }

    public void Settings()
    {
        //artext.GetComponent<Text>().text = "SETTINGS";
        //settings.transform.localScale = new Vector3(1, 1, 1);
        if (audio == 1)
        {
            click.Play();
        }
        alpha = 1.0f;
        set = 1;
        fade.transform.GetComponent<RawImage>().color = new Color(0, 0, 0, 0);
        fading = 0;
        fade.transform.localScale = new Vector3(0, 0, 0);
    }


    public void GoToShop()
    {
        //keycamera.GetComponent<Camera>().enabled = true;
        //minecamera.GetComponent<Camera>().enabled = true;
        if (audio == 1)
        {
            click.Play();
        }
        int coins = PlayerPrefs.GetInt("coins");
        if (dev == 1 && coins < 200)
        {
            coins = 1000;
            PlayerPrefs.SetInt("coins", coins);
        }
        cointext.GetComponent<Text>().text = coins + "";
        alpha = 1.0f;
        set = 11;
        fade.transform.GetComponent<RawImage>().color = new Color(0, 0, 0, 0);
        fading = 0;
        fade.transform.localScale = new Vector3(0, 0, 0);
        atshop = 1;
    }

    public void SetReturn()
    {
        if (audio == 1)
        {
            click.Play();
        }
        set = 2;
        settings.transform.localScale = new Vector3(0, 0, 0);
        menu.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        logo.transform.localScale = new Vector3(1, 1, 1);
        play.transform.localScale = new Vector3(1, 1, 1);
        shop.transform.localScale = new Vector3(1, 1, 1);
        setting.transform.localScale = new Vector3(1, 1, 1);
    }

    public void ShopReturn()
    {
        if (audio == 1)
        {
            click.Play();
        }
        set = 2;
        shopboard.transform.localScale = new Vector3(0, 0, 0);
        menu.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        logo.transform.localScale = new Vector3(1, 1, 1);
        play.transform.localScale = new Vector3(1, 1, 1);
        shop.transform.localScale = new Vector3(1, 1, 1);
        setting.transform.localScale = new Vector3(1, 1, 1);
        atshop = 0;
        //keycamera.GetComponent<Camera>().enabled = false;
        //minecamera.GetComponent<Camera>().enabled = false;
    }

    public void LevelReturn()
    {
        if (audio == 1)
        {
            click.Play();
        }
        alpha = 0;
        alpha2 = 1;
        set = 4;
        levreturn.transform.localScale = new Vector3(0, 0, 0);
        levels.transform.localScale = new Vector3(0, 0, 0);
        nextlevels.transform.localScale = new Vector3(0, 0, 0);
        prevlevels.transform.localScale = new Vector3(0, 0, 0);
        levelsv.transform.localScale = new Vector3(0, 0, 0);
        menu.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
    }

    public void LevelReturn2()
    {
        if (audio == 1)
        {
            click.Play();
        }
            incar.transform.localScale = new Vector3(0, 0, 0);
            tpause.transform.localScale = new Vector3(0, 0, 0);
            dwheel.transform.localScale = new Vector3(0, 0, 0);
            dclock.transform.localScale = new Vector3(0, 0, 0);
            fading = 1;
            fade.transform.localScale = new Vector3(1, 1, 1);
            set = 10;
        towtruck.transform.localPosition = new Vector3(0, 0, 0);
    }

        public void LevelReturn3()
    {
        if (audio == 1)
        {
            click.Play();
        }
        background.transform.localScale = new Vector3(0, 0, 0);
        scoreboard.transform.localScale = new Vector3(0, 0, 0);
        dclock.transform.localScale = new Vector3(0, 0, 0);
        times.transform.localScale = new Vector3(0, 0, 0);
        menu.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        //bbike.transform.localPosition = new Vector3 (bbike.transform.localPosition.x, -12.52f, bbike.transform.localPosition.z);
        //maincamera.transform.localPosition = new Vector3 (0, 0, maincamera.transform.localPosition.z);
        maincamera.GetComponent<Camera>().orthographicSize = 15;
        place = 0;
        logo.GetComponent<RawImage>().color = new Color(logo.GetComponent<RawImage>().color.r, logo.GetComponent<RawImage>().color.g, logo.GetComponent<RawImage>().color.b, 1);
        play.GetComponent<RawImage>().color = new Color(play.GetComponent<RawImage>().color.r, play.GetComponent<RawImage>().color.g, play.GetComponent<RawImage>().color.b, 1);
        shop.GetComponent<RawImage>().color = new Color(shop.GetComponent<RawImage>().color.r, shop.GetComponent<RawImage>().color.g, shop.GetComponent<RawImage>().color.b, 1);
        setting.GetComponent<RawImage>().color = new Color(setting.GetComponent<RawImage>().color.r, setting.GetComponent<RawImage>().color.g, setting.GetComponent<RawImage>().color.b, 1);
        levreturn.GetComponent<RawImage>().color = new Color(levreturn.GetComponent<RawImage>().color.r, levreturn.GetComponent<RawImage>().color.g, levreturn.GetComponent<RawImage>().color.b, 1);
        background.GetComponent<RawImage>().color = new Color(background.GetComponent<RawImage>().color.r, background.GetComponent<RawImage>().color.g, background.GetComponent<RawImage>().color.b, 0);
        scoreboard.GetComponent<RawImage>().color = new Color(scoreboard.GetComponent<RawImage>().color.r, scoreboard.GetComponent<RawImage>().color.g, scoreboard.GetComponent<RawImage>().color.b, 0);
        alpha = 1.0f;
        alpha2 = 1.0f;
        alpha3 = 1.0f;
        alpha4 = 0;
        set = 0;
        //DestoryRoads ();
        alist.RemoveRange(0, alist.Count);
    }

    public void AddAmount()
    {
        int coins = PlayerPrefs.GetInt("coins");
        int currentprice = int.Parse(buyprice.GetComponent<Text>().text);
        if (buyid == 1)
        {
            if(coins < (currentprice + 10) && dev == 0)
            {
                PurchaseF.transform.localScale = new Vector3(1, 1, 1);
                return;
            }
            buyprice.GetComponent<Text>().text = currentprice + 10+"";
            int currentam = int.Parse(buyamount.GetComponent<Text>().text);
            buyamount.GetComponent<Text>().text = currentam + 1 + "";
        }
        else if (buyid == 2)
        {
            if (coins < (currentprice + 15) && dev == 0)
            {
                PurchaseF.transform.localScale = new Vector3(1, 1, 1);
                return;
            }
            buyprice.GetComponent<Text>().text = currentprice + 15 + "";
            int currentam = int.Parse(buyamount.GetComponent<Text>().text);
            buyamount.GetComponent<Text>().text = currentam + 1 + "";
        }
        else if (buyid == 3)
        {
            if (coins < (currentprice + 20) && dev == 0)
            {
                PurchaseF.transform.localScale = new Vector3(1, 1, 1);
                return;
            }
            buyprice.GetComponent<Text>().text = currentprice + 20 + "";
            int currentam = int.Parse(buyamount.GetComponent<Text>().text);
            buyamount.GetComponent<Text>().text = currentam + 1 + "";
        }
        else if (buyid == 4)
        {
            if (coins < (currentprice + 200) && dev == 0)
            {
                PurchaseF.transform.localScale = new Vector3(1, 1, 1);
                return;
            }
            buyprice.GetComponent<Text>().text = currentprice + 200 + "";
            int currentam = int.Parse(buyamount.GetComponent<Text>().text);
            buyamount.GetComponent<Text>().text = currentam + 1 + "";
        }
    }

    public void ReduceAmount()
    {
        int currentam = int.Parse(buyamount.GetComponent<Text>().text);
        if (currentam > 1)
        {
            int coins = PlayerPrefs.GetInt("coins");
            int currentprice = int.Parse(buyprice.GetComponent<Text>().text);
            if (buyid == 1)
            {
                buyprice.GetComponent<Text>().text = currentprice - 10 + "";
                buyamount.GetComponent<Text>().text = currentam - 1 + "";
            }
            else if (buyid == 2)
            {
                buyprice.GetComponent<Text>().text = currentprice - 15 + "";
                buyamount.GetComponent<Text>().text = currentam - 1 + "";
            }
            else if (buyid == 3)
            {
                buyprice.GetComponent<Text>().text = currentprice - 20 + "";
                buyamount.GetComponent<Text>().text = currentam - 1 + "";
            }
            else if (buyid == 4)
            {
                buyprice.GetComponent<Text>().text = currentprice - 200 + "";
                buyamount.GetComponent<Text>().text = currentam - 1 + "";
            }
        }
    }

    public void BuyMenu(int id)
    {
        buyid = id;
        int coins = PlayerPrefs.GetInt("coins");
        if (id == 1)
        {
            if(coins < 10)
            {
                PurchaseF.transform.localScale = new Vector3(1, 1, 1);
                return;
            }
            buytitle.GetComponent<Text>().text = "Anti Mine";
            buyprice.GetComponent<Text>().text = "10";
            buyamount.GetComponent<Text>().text = "1";
        }
        else if (id == 2)
        {
            if (coins < 15)
            {
                PurchaseF.transform.localScale = new Vector3(1, 1, 1);
                return;
            }
            buytitle.GetComponent<Text>().text = "Extra Time";
            buyprice.GetComponent<Text>().text = "15";
            buyamount.GetComponent<Text>().text = "1";
        }
        else if (id == 3)
        {
            if (coins < 20)
            {
                PurchaseF.transform.localScale = new Vector3(1, 1, 1);
                return;
            }
            buytitle.GetComponent<Text>().text = "Slow Motion";
            buyprice.GetComponent<Text>().text = "20";
            buyamount.GetComponent<Text>().text = "1";
        }
        else if (id == 4)
        {
            if (coins < 200)
            {
                PurchaseF.transform.localScale = new Vector3(1, 1, 1);
                return;
            }
            buytitle.GetComponent<Text>().text = "Magic Key";
            buyprice.GetComponent<Text>().text = "200";
            buyamount.GetComponent<Text>().text = "1";
        }
        buymenu.transform.localScale = new Vector3(1, 1, 1);
    }

    public void FinalBuy()
    {
        buymenu.transform.localScale = new Vector3(0, 0, 0);
        int currentam = int.Parse(buyamount.GetComponent<Text>().text);
        if (buyid == 1)
        {
            BuyMine(currentam);
        }
        else if (buyid == 2)
        {
            BuyTime(currentam);
        }
        else if (buyid == 3)
        {
            BuySlow(currentam);
        }
        else if (buyid == 4)
        {
            BuyKey(currentam);
        }
    }


    public void BuySlow(int amount)
    {
        int coins = PlayerPrefs.GetInt("coins");
        if (coins < 20 * amount && dev == 0)
        {
            PurchaseF.transform.localScale = new Vector3(1, 1, 1);
            return;
        }
        if (dev == 0)
        {
            coins -= 20 * amount;
            PlayerPrefs.SetInt("coins", coins);
            if (atshop == 1)
            {
                cointext.GetComponent<Text>().text = coins + "";
            }
        }
        slowmotion = PlayerPrefs.GetInt("slowmotion");
        slowmotion += amount;
        PlayerPrefs.SetInt("slowmotion", slowmotion);
        purchasem.transform.GetComponent<Text>().text = "You've bought\nSlow Motion!";
        Purchase.transform.localScale = new Vector3(1, 1, 1);
        if (signin == 1)
        {
            SaveCloud();
        }
    }

    public void BuyTime(int amount)
    {
        int coins = PlayerPrefs.GetInt("coins");
        if (coins < 15 * amount && dev == 0)
        {
            PurchaseF.transform.localScale = new Vector3(1, 1, 1);
            return;
        }
        if(dev == 0)
        {
            coins -= 15 * amount;
            PlayerPrefs.SetInt("coins", coins);
            if (atshop == 1)
            {
                cointext.GetComponent<Text>().text = coins + "";
            }
        }
        extracount = PlayerPrefs.GetInt("extracount");
        extracount += amount;
        PlayerPrefs.SetInt("extracount", extracount);
        purchasem.transform.GetComponent<Text>().text = "You've bought\nExtra Time!";
        Purchase.transform.localScale = new Vector3(1, 1, 1);
        if (signin == 1)
        {
            SaveCloud();
        }
    }

    public void BuyMine(int amount)
    {
        int coins = PlayerPrefs.GetInt("coins");
        if (coins < 10 * amount && dev == 0)
        {
            PurchaseF.transform.localScale = new Vector3(1, 1, 1);
            return;
        }
        if (dev == 0)
        {
            coins -= 10 * amount;
            PlayerPrefs.SetInt("coins", coins);
            if (atshop == 1)
            {
                cointext.GetComponent<Text>().text = coins + "";
            }
        }
        int antiminecount = PlayerPrefs.GetInt("antiminecount");
        antiminecount += amount;
        PlayerPrefs.SetInt("antiminecount", antiminecount);
        purchasem.transform.GetComponent<Text>().text = "You've bought\nAnti Mine!";
        Purchase.transform.localScale = new Vector3(1, 1, 1);
        if (signin == 1)
        {
            SaveCloud();
        }
    }

    public void BuyKey(int amount)
    {
        int coins = PlayerPrefs.GetInt("coins");
        if (coins < 200 * amount && dev == 0)
        {
            PurchaseF.transform.localScale = new Vector3(1, 1, 1);
            return;
        }
        if (dev == 0)
        {
            coins -= 200 * amount;
            PlayerPrefs.SetInt("coins", coins);
            if (atshop == 1)
            {
                cointext.GetComponent<Text>().text = coins + "";
            }
        }
        int keypasscount = PlayerPrefs.GetInt("keypasscount");
        keypasscount += amount;
        PlayerPrefs.SetInt("keypasscount", keypasscount);
        purchasem.transform.GetComponent<Text>().text = "You've bought\nMagic Key!";
        Purchase.transform.localScale = new Vector3(1, 1, 1);
        if (signin == 1)
        {
            SaveCloud();
        }
    }

    public void SlowMotion()
    {
        if (slowmotion > 0 && slowused < 3)
        {
                sused = 1;
                slowused++;
                slow = 1;
            if (slowcount == 0)
            {
                slowcount = 10;
            }
            else
            {
                slowcount = slowcount + 10;
            }
                slowmotion -= 1;
                slowback.transform.localScale = new Vector3(1, 1, 1);
                slownum.GetComponent<Text>().text = (int)slowcount + "";
                slownum.transform.localScale = new Vector3(1, 1, 1);
                if (slowmotion == 0)
                {
                    slowmotiono.transform.localScale = new Vector3(0, 0, 0);
                }
        }
    }

    public void ExtraTime()
    {
        if (extracount > 0)
        {
            if (tused < 1)
            {
                tused = 1;
                timeused++;
                if (timeused > 2)
                {
                    epback.transform.localScale = new Vector3(0, 0, 0);
                }
                count += 10;
                extracount -= 1;
                if(extracount == 0)
                {
                    epback.transform.localScale = new Vector3(0, 0, 0);
                }
            }
        }
    }

    public void AntiMine()
    {
        if (amused == 0)
        {
            mineback.transform.localScale = new Vector3(0, 0, 0);
            antimine = 1;
            shockwhite.GetComponent<ParticleSystem>().Play();
            int antiminecount = PlayerPrefs.GetInt("antiminecount");
            antiminecount -= 1;
            PlayerPrefs.SetInt("antiminecount", antiminecount);
            amused = 1;
        }
    }

    public void KeyPass()
    {
        gosign.transform.localScale = new Vector3(0, 0, 0);
        count = 9999;
        keypass = 1;
        FinishLevel();
        int keypasscount = PlayerPrefs.GetInt("keypasscount");
        keypasscount -= 1;
        PlayerPrefs.SetInt("keypasscount", keypasscount);
    }

    public void SlowInfo()
    {
        sititle.transform.GetComponent<Text>().text = "SLOW MOTION";
        youhaveedit.transform.GetComponent<Text>().text = "  SLOW MOTION";
        simessage.transform.GetComponent<Text>().text = "If the speed is\ntoo high, you can use slow\nmotion for 10 seconds.\nYou can use it up to\n3 times per level.";
        slowmotion = PlayerPrefs.GetInt("slowmotion");
        sinum.transform.GetComponent<Text>().text = "" + slowmotion;
        shopinfo.transform.localScale = new Vector3(1, 1, 1);
    }

    public void TimeInfo()
    {
        sititle.transform.GetComponent<Text>().text = "EXTRA TIME";
        youhaveedit.transform.GetComponent<Text>().text = "EXTRA TIME";
        simessage.transform.GetComponent<Text>().text = "If you need more time\nYou can add 10 more\nseconds into your level.\nYou can use it up to\n3 times per level.";
        extracount = PlayerPrefs.GetInt("extracount");
        sinum.transform.GetComponent<Text>().text = "" + extracount;
        shopinfo.transform.localScale = new Vector3(1, 1, 1);
    }

    public void MineInfo()
    {
        sititle.transform.GetComponent<Text>().text = "ANTI MINE";
        youhaveedit.transform.GetComponent<Text>().text = "ANTI MINE";
        simessage.transform.GetComponent<Text>().text = "If the mines are\na problem for you,\nyou can use anti mine\nin order to protect\nthe truck from mines.";
        int antiminecount = PlayerPrefs.GetInt("antiminecount");
        sinum.transform.GetComponent<Text>().text = "" + antiminecount;
        shopinfo.transform.localScale = new Vector3(1, 1, 1);
    }

    public void KeyInfo()
    {
        sititle.transform.GetComponent<Text>().text = "MAGIC KEY";
        youhaveedit.transform.GetComponent<Text>().text = "MAGIC KEY";
        simessage.transform.GetComponent<Text>().text = "If you fail to pass a level\nYou can use a magic key\nto pass it with 3 stars,\nAnd continue to the\nnext level.";
        int keypasscount = PlayerPrefs.GetInt("keypasscount");
        sinum.transform.GetComponent<Text>().text = "" + keypasscount;
        shopinfo.transform.localScale = new Vector3(1, 1, 1);
    }

    public void Replay()
    {
        if (audio == 1)
        {
            click.Play();
        }
        //artext.GetComponent<Text>().text = "REPLAY";
        PlayAd script2 = (PlayAd)bbike2.GetComponent(typeof(PlayAd));
        script2.ShowAd();
        fade.transform.GetComponent<RawImage>().color = new Color(0, 0, 0, 0);
        fading = 0;
        fade.transform.localScale = new Vector3(0, 0, 0);
        //artext.GetComponent<Text>().text = "REPLAY2";
    }

    public void Replay2()
    {
        //artext.GetComponent<Text>().text = "REPLAY3";
        //print("ok");
        if (mode == 1)
        {
            if (audio == 1)
            {
                click.Play();
            }
            background.transform.localScale = new Vector3(0, 0, 0);
            scoreboard.transform.localScale = new Vector3(0, 0, 0);
            //bike.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            //marker.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            //dclock.transform.localScale = new Vector3(1, 1, 1);
            times.transform.localScale = new Vector3(1, 1, 1);
            gosign.transform.localScale = new Vector3(0, 0, 0);
            gospeed = 0.000000001f;
            towtruck.transform.localEulerAngles = new Vector3(0, 0, 0);
            Level2(level);
            maincamera.GetComponent<Camera>().enabled = true;
            startcamera.GetComponent<Camera>().enabled = false;
            camloading = 2;
            Aspect();
            SetTimes();
            place = 1;
            //print("ok2");
        }
        else if (mode == 2)
        {
            gosign2.transform.localScale = new Vector3(0, 0, 0);
            tire6.transform.localPosition = new Vector3(0, -0.186f, 0.023f);
            tire7.transform.localPosition = new Vector3(0, -0.186f, -0.05f);
            tire8.transform.localPosition = new Vector3(0.135f, -0.186f, 0.005f);
            tire9.transform.localPosition = new Vector3(-0.088f, -0.186f, 0);
            Play2();
        }
    }

    public void NextLevel()
    {
        if (audio == 1)
        {
            click.Play();
        }
        background.transform.localScale = new Vector3(0, 0, 0);
        scoreboard.transform.localScale = new Vector3(0, 0, 0);
        //bike.transform.localScale = new Vector3 (0.5f, 0.5f, 0.5f);
        //marker.transform.localScale = new Vector3 (0.5f, 0.5f, 0.5f);
        dclock.transform.localScale = new Vector3(1, 1, 1);
        times.transform.localScale = new Vector3(1, 1, 1);
        //DestoryRoads ();
        alist.RemoveRange(0, alist.Count);
        gospeed = 0.000000001f;
        towtruck.transform.localEulerAngles = new Vector3(0, 0, 0);
        level++;
        Level2(level);
        SetTimes();
        place = 1;

        if (finishplaying == 1)
        {
            if (finishsound.isPlaying == false)
            {
                finishplaying = 0;
                mainmusic.volume = 0.12f;
            }
        }
    }

    public void FinishLevel()
    {
        if (gameover == 0 && finished == 0 || keypass == 1)
        {
            PlayerPrefs.SetInt("extracount", extracount);
            FinishSound();
            gospeed = 0.000000001f;
            place = 0;
            inwheel = 0;
            isgo = 0;
            finished = 1;
            gameover = 0;
            if (passed < level)
            {
                passed++;
                PlayerPrefs.SetInt("passed", passed);
            }
            allscores[level] = "1";
            AllScores();
            string oldstar = allstars[level];
            if (count < 9000)
            {
                if (count >= int.Parse(starst[2]))
                {
                    allstars[level] = "3";
                    rightpanel = -20;
                }
                else if (count >= int.Parse(starst[1]) && count < int.Parse(starst[2]))
                {
                    allstars[level] = "2";
                    rightpanel = 30;
                }
                else
                {
                    allstars[level] = "1";
                    rightpanel = 80;
                }
                if (int.Parse(oldstar) > int.Parse(allstars[level]))
                {
                    allstars[level] = oldstar;
                }
                //print(allstars[level]);
                scoretext.GetComponent<Text>().text = (int)(count) + "";
                leveltext.GetComponent<Text>().text = "" + level;
            }
            else
            {
                allstars[level] = "3";
                rightpanel = -20;
                //print(allstars[level]);
                scoretext.GetComponent<Text>().text = "";
                leveltext.GetComponent<Text>().text = "" + level;
            }

            AllStars();
            if(signin == 1)
            {
                SaveCloud();
            }
            background.transform.localScale = new Vector3(1, 1, 1);
            scoreboard.transform.localScale = new Vector3(1, 1, 1);
            incar.transform.localScale = new Vector3(0, 0, 0);
            dwheel.transform.localScale = new Vector3(0, 0, 0);
            fading = 1;
            fade.transform.localScale = new Vector3(1, 1, 1);
            set = 5;
            alevels = 0;
            foreach (Transform child in levels.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
        }
    }

    void FinishSound()
    {
        if (audio == 1)
        {
            gasengine.Stop();
            decengine.Stop();
            backengine.Stop();
            if (music == 1)
            {
                mainmusic.volume = 1 / 16f;
                finishsound.volume = 0.5f;
                finishsound.Play();
                finishplaying = 1;
            }
            else
            {
                finishsound.volume = 0.5f;
                finishsound.Play();
                finishplaying = 1;
            }
        }
    }

    public bool CheckList(Vector3 pos)
    {
        for (int i = 0; i < alist.Count; i++) {
            if (alist[i].transform.GetComponent<BoxCollider2D>().bounds.Contains(pos)) {
                return true;
            }
        }
        return false;
    }

    public void WheelDown()
    {
        inwheel = 1;
    }

    public void WheelUp()
    {
        inwheel = 0;
    }

    public void GasDown()
    {
        if (isgo == 0)
        {
            //enginevol = 1;
            //gasengine.Play();
        }
        isgo = 1;

    }

    public void GasUp()
    {
        isgo = 0;
    }

    public void TurnRight()
    {
        tr = 1; 
    }

    public void TurnLeft()
    {
        tl = 1;
    }

    public void DeTurnRight()
    {
        tr = 0;
        spinf = 0;
    }

    public void DeTurnLeft()
    {
        tl = 0;
        spinf = 0;
    }

    public void StopDown()
    {
        if (nostop == 0)
        {
            instop = 1;
        }
    }

    public void StopUp()
    {
        instop = 0;
    }


    public void Gas()
    {
        if (pause == 0 && gospeed < 15)
        {
            gospeed += tspeed;
        }
    }
    public void LowerGas()
    {
        if (pause == 0)
        {
            if (minspeed > 0 && gospeed*8 >= minspeed && gospeed*8 <= minspeed + 1)
            {
                return;
            }
            gospeed -= tspeed / 10;
            if (gospeed < 0)
            {
                gospeed = 0.000000001f;
            }
        }
    }

    public void LowerGas2()
    {
        if (pause == 0)
        {
            gospeed -= tspeed;
            if (gospeed < 0)
            {
                gospeed = 0.000000001f;
            }
        }
    }


    public void Stop()
    {
        if (pause == 0)
        {
            if (gospeed > 0)
            {
                gospeed -= tspeed * 2;
            }
                /*gospeed -= tspeed * 2;
                if (gospeed < 0)
                {
                    gospeed -= tspeed;
                }*/
        }
    }

    public float GetSpeed()
    {
        return gospeed;
    }

    void AllScores()
    {
        string scores = "0";
        for (int i = 1; i < allscores.Length; i++) {
            scores = scores + ":" + allscores[i];
        }
        PlayerPrefs.SetString("scores", scores);
    }

    void AllStars()
    {
        string stars = "0";
        for (int i = 1; i < allstars.Length; i++) {
            stars = stars + ":" + allstars[i];
        }
        PlayerPrefs.SetString("stars", stars);
    }

    public void StopCount()
    {
        if (place == 1 && count < 29) {
            place = 2;
            play2.transform.localScale = new Vector3(1, 1, 1);
            marker.transform.localScale = new Vector3(0, 0, 0);
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void TPause()
    {
        pause = 1;
        paused.transform.localScale = new Vector3(1, 1, 1);
    }

    public void TResume()
    {
        pause = 0;
        paused.transform.localScale = new Vector3(0, 0, 0);
    }

    public void TMenu()
    {
        pause = 0;
        paused.transform.localScale = new Vector3(0, 0, 0);
        gameover = 3;
    }


    IEnumerator Count()
    {
        yield return new WaitForSeconds(1);
        count--;
        seconds++;
        float rotationSeconds = (360.0f / 60.0f) * seconds;
        pointerSeconds.transform.localEulerAngles = new Vector3(0.0f, 0.0f, rotationSeconds);
        //time.GetComponent<Text> ().text = count+"";
    }

 
    public void OkNoBreak()
    {
        nobreak.transform.localScale = new Vector3(0, 0, 0);
        notice--;
    }

    public void OkSpeed()
    {
        minspeeds.transform.localScale = new Vector3(0, 0, 0);
        notice--;
    }

    public void OkUpside()
    {
        upsideo.transform.localScale = new Vector3(0, 0, 0);
        notice--;
    }

    public void OkMines()
    {
        mines.transform.localScale = new Vector3(0, 0, 0);
        notice--;
    }

    public void OkSlower()
    {
        slower.transform.localScale = new Vector3(0, 0, 0);
        notice--;
    }

    public void OkLv1()
    {
        lv1sign.transform.localScale = new Vector3(0, 0, 0);
        lv1sign2.transform.localScale = new Vector3(1, 1, 1);
    }

    public void OkLv2()
    {
        lv1sign2.transform.localScale = new Vector3(0, 0, 0);
        lv1sign3.transform.localScale = new Vector3(1, 1, 1);
    }

    public void OkLv3()
    {
        lv1sign3.transform.localScale = new Vector3(0, 0, 0);
        notice--;
    }

    public void OkLv1Re()
    {
        lv1sign2.transform.localScale = new Vector3(0, 0, 0);
        lv1sign.transform.localScale = new Vector3(1, 1, 1);
    }

    public void OkLv2Re()
    {
        lv1sign3.transform.localScale = new Vector3(0, 0, 0);
        lv1sign2.transform.localScale = new Vector3(1, 1, 1);
    }

    void bbikepos()
    {
       int bnum =  UnityEngine.Random.Range(1, 5);

        if (bnum == 1)
        {
            bbikestart = new Vector3(230.1f, 6.42f, 90.26f);
            startcamera.transform.localPosition = new Vector3(startpos.x, startpos.y, 90.4f);
        }
       else if (bnum == 2)
       {
            bbikestart = new Vector3(230.1f, 6.42f, 391.23f);
            startcamera.transform.localPosition = new Vector3(startpos.x, startpos.y, 391.4f);
        }
       else if (bnum == 3)
       {
            bbikestart = new Vector3(230.1f, 6.42f, 758.1f);
            startcamera.transform.localPosition = new Vector3(startpos.x, startpos.y, 758.3f);
        }
       else if (bnum == 4)
       {
            bbikestart = new Vector3(230.1f, 6.42f, 1122.4f);
            startcamera.transform.localPosition = new Vector3(startpos.x, startpos.y, 1122.6f);
        }

    }

    void Aspect()
    {
       /* float windowaspect = (float)Screen.width / (float)Screen.height;

        print("aspact " + windowaspect);

        if (windowaspect > 1.7f && windowaspect < 1.8f)
        {
            canvas2.GetComponent<CanvasScaler>().matchWidthOrHeight = 0.388f;
        }
        else if (windowaspect > 1.3f && windowaspect < 1.4f)
        {
            canvas2.GetComponent<CanvasScaler>().matchWidthOrHeight = 0.4f;
        }
        else if (windowaspect > 1.9f && windowaspect < 2.1f)
        {
            canvas2.GetComponent<CanvasScaler>().matchWidthOrHeight = 0.572f;
        }
        else
        {
            canvas2.GetComponent<CanvasScaler>().matchWidthOrHeight = 0.211f;
        }*/
        
        // set the desired aspect ratio (the values in this example are
        // hard-coded for 16:9, but you could make them into public
        // variables instead so you can set them at design time)
        /*float targetaspect = 16.0f / 9.0f;

        // determine the game window's current aspect ratio
        float windowaspect = (float)Screen.width / (float)Screen.height;

        if(windowaspect == 18f/9f)
        {
            //blackbar.transform.localPosition = new Vector3(blackbar.transform.localPosition.x, -215, blackbar.transform.localPosition.z);
        }

        // current viewport height should be scaled by this amount
        float scaleheight = windowaspect / targetaspect;

        // obtain camera component so we can modify its viewport
        Camera camera = Camera.main;

        // if scaled height is less than current height, add letterbox
        if (scaleheight < 1.0f)
        {
            Rect rect = camera.rect;

            rect.width = 1.0f;
            rect.height = scaleheight;
            rect.x = 0;
            rect.y = (1.0f - scaleheight) / 2.0f;

            camera.rect = rect;
        }
        else // add pillarbox
        {
            float scalewidth = 1.0f / scaleheight;

            Rect rect = camera.rect;

            rect.width = scalewidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scalewidth) / 2.0f;
            rect.y = 0;

            camera.rect = rect;
        }*/
    }

    void SaveCloud()
    {
        if (signin == 1)
        {
#if UNITY_ANDROID
                if (Social.localUser.authenticated)
                {
                    isSaving = true;
                    ((PlayGamesPlatform)Social.Active).SavedGame.OpenWithManualConflictResolution("Stats",
                        DataSource.ReadCacheOrNetwork, true, ResolveConflict, OnSavedGameOpened);
                }
#elif UNITY_IPHONE

#endif
        }
    }

    public void LoadCloud()
    {
        //basically if we're connected to the internet, do everything on the cloud
        if (signin == 1)
        {
#if UNITY_ANDROID
            
                if (Social.localUser.authenticated)
                {
                    isSaving = false;
                    ((PlayGamesPlatform)Social.Active).SavedGame.OpenWithManualConflictResolution("Stats",
                        DataSource.ReadCacheOrNetwork, true, ResolveConflict, OnSavedGameOpened);
                }

#endif
        }
    }

    private void ResolveConflict(IConflictResolver resolver, ISavedGameMetadata original, byte[] originalData,
        ISavedGameMetadata unmerged, byte[] unmergedData)
    {
        if (originalData == null)
            resolver.ChooseMetadata(unmerged);
        else if (unmergedData == null)
            resolver.ChooseMetadata(original);
        else
        {
            //decoding byte data into string
            string originalStr = Encoding.ASCII.GetString(originalData);
            string unmergedStr = Encoding.ASCII.GetString(unmergedData);

            //parsing
            int originalNum = int.Parse(originalStr);
            int unmergedNum = int.Parse(unmergedStr);

            //if original score is greater than unmerged
            if (originalNum > unmergedNum)
            {
                resolver.ChooseMetadata(original);
                return;
            }
            //else (unmerged score is greater than original)
            else if (unmergedNum > originalNum)
            {
                resolver.ChooseMetadata(unmerged);
                return;
            }
            //if return doesn't get called, original and unmerged are identical
            //we can keep either one
            resolver.ChooseMetadata(original);
        }
    }

    private void OnSavedGameOpened(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        //if we are connected to the internet
        if (status == SavedGameRequestStatus.Success)
        {
            //if we're LOADING game data
            if (!isSaving)
                LoadGame(game);
            //if we're SAVING game data
            else
                SaveGame(game);
        }
        //if we couldn't successfully connect to the cloud, runs while on device,
        //the same code that is in else statements in LoadData() and SaveData()
        /*else
        {
            if (!isSaving)
                LoadLocal();
            else
                SaveLocal();
        }*/
    }

    private void LoadGame(ISavedGameMetadata game)
    {
#if UNITY_ANDROID

        ((PlayGamesPlatform)Social.Active).SavedGame.ReadBinaryData(game, OnSavedGameDataRead);
#endif
    }

    private void SaveGame(ISavedGameMetadata game)
    {
#if UNITY_ANDROID
        string stringToSave = GameDataToString();
        //saving also locally (can also call SaveLocal() instead)

        //encoding to byte array
        byte[] dataToSave = Encoding.ASCII.GetBytes(stringToSave);
        //updating metadata with new description
        SavedGameMetadataUpdate update = new SavedGameMetadataUpdate.Builder().Build();
        //uploading data to the cloud
        ((PlayGamesPlatform)Social.Active).SavedGame.CommitUpdate(game, update, dataToSave,
            OnSavedGameDataWritten);
#endif
    }

    private void OnSavedGameDataWritten(SavedGameRequestStatus status, ISavedGameMetadata game)
    {

    }

    private void OnSavedGameDataRead(SavedGameRequestStatus status, byte[] savedData)
    {
        //if reading of the data was successful
        if (status == SavedGameRequestStatus.Success)
        {
            string cloudDataString;
            //if we've never played the game before, savedData will have length of 0
            if (savedData.Length == 0)
            {
                cloudDataString = "0";
            }
            //otherwise take the byte[] of data and encode it to string
            else
            {
                cloudDataString = Encoding.ASCII.GetString(savedData);
                //artext.GetComponent<Text>().text = cloudDataString;
                allstats = cloudDataString.Split(":"[0]);
                PlayerPrefs.SetInt("coins", int.Parse(allstats[0]));
                PlayerPrefs.SetInt("slowmotion", int.Parse(allstats[1]));
                PlayerPrefs.SetInt("extracount", int.Parse(allstats[2]));
                PlayerPrefs.SetInt("antiminecount", int.Parse(allstats[3]));
                PlayerPrefs.SetInt("keypasscount", int.Parse(allstats[4]));
                PlayerPrefs.SetInt("passed", int.Parse(allstats[5]));
                passed = int.Parse(allstats[5]);
                string scores = "";
                string stars = "";
                for (int i = 6; i < 38; i++)
                {
                    if (i < 37)
                    {
                        scores = scores + allstats[i] + ":";
                    }
                    else
                    {
                        scores = scores + allstats[i];
                    }
                }
                for (int i = 38; i < 71; i++)
                {
                    if (i < 70)
                    {
                        stars = stars + allstats[i] + ":";
                    }
                    else
                    {
                        stars = stars + allstats[i];
                    }
                }
                PlayerPrefs.SetString("scores", scores);
                PlayerPrefs.SetString("stars", stars);
                allscores = scores.Split(":"[0]);
                allstars = stars.Split(":"[0]);
                AllScores();
                AllStars();
                if(set == 8)
                {
                    LevelReturn();
                }
                alevels = 0;
                foreach (Transform child in levels.transform)
                {
                    GameObject.Destroy(child.gameObject);
                }
                supdated.transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }

    string GameDataToString()
    {
        //SAVING
        string final = null;
        passed = PlayerPrefs.GetInt("passed");
        string scores = PlayerPrefs.GetString("scores");
        stars = PlayerPrefs.GetString("stars");
        slowmotion = PlayerPrefs.GetInt("slowmotion");
        extracount = PlayerPrefs.GetInt("extracount");
        int antiminecount = PlayerPrefs.GetInt("antiminecount");
        int keypasscount = PlayerPrefs.GetInt("keypasscount");
        int coins = PlayerPrefs.GetInt("coins");
        final = coins + ":"  + slowmotion + ":" + extracount  + ":" + antiminecount + ":" + keypasscount + ":" + passed + ":" + scores + ":" + stars;
        //artext.GetComponent<Text>().text = final;
        return final;
    }

}

