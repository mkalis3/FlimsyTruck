using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishRot : MonoBehaviour
{

    public int stage, rot;
    GameObject bbike2, towtruck;
    float rotf = 0.26f;

    void Start()
    {
        bbike2 = GameObject.Find("bbike2");
        towtruck = GameObject.Find("towtruck");
    }

    void Update()
    {
        if (stage == 1 && rot != 0)
        {
            //print("now " + rot + " " + towtruck.transform.localEulerAngles.y);
            if (rot == 1)
            {
                if (towtruck.transform.localEulerAngles.y > 91)
                {
                    towtruck.transform.Rotate(0, -rotf, 0);
                }
                else if (towtruck.transform.localEulerAngles.y < 89)
                {
                    towtruck.transform.Rotate(0, rotf, 0);
                }
                towtruck.transform.position += towtruck.transform.forward * (1 * Time.deltaTime);
            }
            else if (rot == 2)
            {
                if (towtruck.transform.localEulerAngles.y > 181)
                {
                    towtruck.transform.Rotate(0, -rotf, 0);
                }
                else if (towtruck.transform.localEulerAngles.y < 179)
                {
                    towtruck.transform.Rotate(0, rotf, 0);
                }
                towtruck.transform.position += towtruck.transform.forward * (1 * Time.deltaTime);
            }
            else if (rot == 3)
            {
                if (towtruck.transform.localEulerAngles.y > 271)
                {
                    towtruck.transform.Rotate(0, -rotf, 0);
                }
                else if (towtruck.transform.localEulerAngles.y < 269)
                {
                    towtruck.transform.Rotate(0, rotf, 0);
                }
                towtruck.transform.position += towtruck.transform.forward * (1 * Time.deltaTime);
            }
            else if (rot == 4)
            {
                if (WrapAngle(towtruck.transform.localEulerAngles.y) > 1)
                {
                    towtruck.transform.Rotate(0, -rotf, 0);
                }
                else if (WrapAngle(towtruck.transform.localEulerAngles.y) < 359)
                {
                    towtruck.transform.Rotate(0, rotf, 0);
                }
                towtruck.transform.position += towtruck.transform.forward * (1 * Time.deltaTime);
            }
        }
    }

    public void Finished(int rot2)
    {
        if (stage == 0)
        {
            rot = rot2;
            rotf = 0.26f;
            stage = 1;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        Bike script = (Bike)bbike2.GetComponent(typeof(Bike));
        if (col.gameObject.name == "garage2")
        {
            stage = 0;
        }
        if (col.gameObject.name == "garage3")
        {
            rotf = 1.5f;
        }
        if (col.gameObject.name == "garage4")
        {
            rotf = 0.5f;
        }
    }

    private static float WrapAngle(float angle)
    {
        angle %= 360;
        if (angle > 180)
            return angle - 360;

        return angle;
    }
}
