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

public partial class Bike
{
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

        if (SceneManager.GetActiveScene().name.Equals("third"))
        {

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
}
