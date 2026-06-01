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

                if (firsttime == 1)
                {
                    LoadCloud();
                    firsttime = 0;
                }
            }
            else
            {

            }
        });
#endif
    }

    public void Credits()
    {
        credits.transform.localScale = new Vector3(1, 1, 1);
    }

    public void ToggleDiagnosticMode()
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

    }

    public void GoToShop()
    {

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
}
