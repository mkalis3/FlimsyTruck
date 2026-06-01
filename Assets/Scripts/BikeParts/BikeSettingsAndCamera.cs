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
        else
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

        if (id != quality) {
            if (quality == 0) {

            }
            else if (quality == 1) {

            }
            else if (quality == 2) {

            }
            else if (quality == 3) {

            }
            if (id == 0) {

                QualitySettings.SetQualityLevel(0);
            }
            else if (id == 1) {

                QualitySettings.SetQualityLevel(2);
            }
            else if (id == 2) {

                QualitySettings.SetQualityLevel(3);
            }
            else if (id == 3) {

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

                Texture audioon = Resources.Load("audioon", typeof(Texture2D)) as Texture;
                RawImage img = (RawImage)oaudio.GetComponent<RawImage>();
                img.texture = audioon;
                audioon = Resources.Load("toggleon", typeof(Texture2D)) as Texture;
                img = (RawImage)audiotog.GetComponent<RawImage>();
                img.texture = audioon;
            } else {
                audio = 0;
                PlayerPrefs.SetFloat("audio", audio);

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
        if (set == 1) {
            if (music == 0) {
                PlayerPrefs.SetFloat("music", 1);
                music = 1;

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

    public void Settings()
    {

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

    void Aspect()
    {

    }
}
