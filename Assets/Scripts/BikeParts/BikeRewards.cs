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
        string HtmlText = GetHtmlFromUri("https://www.google.com/generate_204");
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
        string HtmlText = GetHtmlFromUri("https://www.google.com/generate_204");
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
        WWW www = new WWW("https://www.google.com/generate_204");
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
        UnityWebRequest www = UnityWebRequest.Get("https://www.tmkapps.com/gettime.php");
        yield return www.SendWebRequest();

        if (www.isNetworkError)
        {
        }
        else
        {
            String ts = www.downloadHandler.text;
            ctime = ts.Split("/"[0]);
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
        UnityWebRequest www = UnityWebRequest.Get("https://www.tmkapps.com/gettime.php");
        yield return www.SendWebRequest();

        if (www.isNetworkError)
        {
        }
        else
        {

            String ts = www.downloadHandler.text;
            ctime2 = ts.Split("/"[0]);
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
}
