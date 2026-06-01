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
            }
        }

        if (notice == 0)
        {

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

                        }

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
                    if (exspeednow == 0)
                    {
                        if (gospeed * 8 > exspeed)
                        {
                            exspeednow = 1;
                        }
                    }
                    else
                    {
                        if (gospeed * 8 < exspeed)
                        {
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

                if (timeused > 0)
                {
                    tused -= Time.deltaTime;
                }

                if (slowused > 0)
                {
                    sused -= Time.deltaTime;
                }

                Vector3 gospeedv = new Vector3(0, 0, gospeed);
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

                        float mx = towtruck.transform.position.x;
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

                        float tflip = -1.18f;
                        if (gameover == 0)
                        {
                            if (ty < tflip)
                            {
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

                            if (ty2 < tflip)
                            {
                                gameover = 2;
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
                        }

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

                        gosign.transform.localScale = new Vector3(1, 1, 1);
                        int keypasscount = PlayerPrefs.GetInt("keypasscount");
                        if (keypasscount > 0 && int.Parse(allstars[level]) == 0)
                        {

                            magicwranchgo.transform.localScale = new Vector3(1, 1, 1);
                        }

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

                        background.transform.localScale = new Vector3(0, 0, 0);
                        scoreboard.transform.localScale = new Vector3(0, 0, 0);
                        dclock.transform.localScale = new Vector3(0, 0, 0);
                        times.transform.localScale = new Vector3(0, 0, 0);
                        menu.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);

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

                float cx, cy, bz, cxper = 0, cyper = 0, bxper = 0, byper = 0;
                cx = circle2.transform.localPosition.x;
                cy = circle2.transform.localPosition.y;

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

                if (place == 0)
                {
                    bbike.transform.localPosition = new Vector3(bbike.transform.localPosition.x, bbike.transform.localPosition.y, bbike.transform.localPosition.z + speed);
                    startcamera.transform.localPosition = new Vector3(startcamera.transform.localPosition.x, startcamera.transform.localPosition.y, startcamera.transform.localPosition.z + speed);

                    if (bbike.transform.localPosition.z >= 1406)
                    {
                        bbike.transform.localPosition = new Vector3(bbike.transform.localPosition.x, bbike.transform.localPosition.y, 92);
                        startcamera.transform.localPosition = new Vector3(startcamera.transform.localPosition.x, startcamera.transform.localPosition.y, 94);

                    }

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

                    if (count < 9000)
                    {
                        dclock.transform.GetComponent<Text>().text = (int)count + "";
                    }

                    if (count > 0)
                    {

                    }
                    else
                    {
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

                }
                else if (Input.GetMouseButtonUp(0))
                {
                    start = 0;
                }

            }
        }
    }

    void SetSpeed(float speed)
    {

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

                speedoarrow.transform.localEulerAngles = new Vector3(speedoarrow.transform.localEulerAngles.x, speedoarrow.transform.localEulerAngles.y, 14.3f);
            }
        }

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
}
