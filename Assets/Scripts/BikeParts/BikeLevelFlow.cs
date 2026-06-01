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

    }

    public void Play()
    {

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

        stop.transform.localScale = new Vector3(1, 1, 1);

        level = id;
        Aspect();
        load = 1;
        starpos = 0;

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

        SetTimes();
        offset = maincamera2.transform.position - towtruck.transform.position;

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

                pstar.transform.localScale = new Vector3(1, 1, 1);
                pstar2.transform.localScale = new Vector3(1, 1, 1);
                pstar3.transform.localScale = new Vector3(1, 1, 1);

            }
            else if (id == 2)
            {

                pstar.transform.localScale = new Vector3(1, 1, 1);
                pstar2.transform.localScale = new Vector3(1, 1, 1);
                pstar3.transform.localScale = new Vector3(0, 0, 0);
            }
            else if (id == 1)
            {

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

        }
    }

    void UnActive()
    {
        if (active == 0)
        {
            active = 1;

        }
    }

    void LevelTerrain(int id)
    {

    }

    void SetTimes()
    {
        n0.GetComponent<Text>().text = "" + count;
        n1.GetComponent<Text>().text = "" + count / 6;
        n2.GetComponent<Text>().text = "" + count / 3;
        n3.GetComponent<Text>().text = "" + count / 2;
    }

    public void Replay()
    {
        if (audio == 1)
        {
            click.Play();
        }

        PlayAd script2 = (PlayAd)bbike2.GetComponent(typeof(PlayAd));
        script2.ShowAd();
        fade.transform.GetComponent<RawImage>().color = new Color(0, 0, 0, 0);
        fading = 0;
        fade.transform.localScale = new Vector3(0, 0, 0);

    }

    public void Replay2()
    {

        if (mode == 1)
        {
            if (audio == 1)
            {
                click.Play();
            }
            background.transform.localScale = new Vector3(0, 0, 0);
            scoreboard.transform.localScale = new Vector3(0, 0, 0);

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

        dclock.transform.localScale = new Vector3(1, 1, 1);
        times.transform.localScale = new Vector3(1, 1, 1);

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
                scoretext.GetComponent<Text>().text = (int)(count) + "";
                leveltext.GetComponent<Text>().text = "" + level;
            }
            else
            {
                allstars[level] = "3";
                rightpanel = -20;
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
}
