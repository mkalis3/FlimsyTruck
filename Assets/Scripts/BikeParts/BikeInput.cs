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

        }
    }

    public float GetSpeed()
    {
        return gospeed;
    }
}
