using UnityEngine;
using System.Collections;

public class Finish : MonoBehaviour
{
    public int game, rot;
    GameObject bbike2, towtruck, minedetect;

    // Use this for initialization
    void Start()
    {
        bbike2 = GameObject.Find("bbike2");
        towtruck = GameObject.Find("towtruck");
        minedetect = GameObject.Find("minedetect");
    }


    void OnCollisionEnter(Collision col)
    {
        if (game == 1)
        {
            if (col.gameObject.name == "garage")
            {
                game = 0;
                Bike script = (Bike)bbike2.GetComponent(typeof(Bike));
                script.FinishLevel();
                    if (col.gameObject.transform.localEulerAngles.y == 0)
                    {
                        rot = 1;
                    }
                    else if (col.gameObject.transform.localEulerAngles.y == 90)
                    {
                        rot = 2;
                    }
                    else if (col.gameObject.transform.localEulerAngles.y == 180)
                    {
                        rot = 3;
                    }
                    else if (col.gameObject.transform.localEulerAngles.y == 270)
                    {
                        rot = 4;
                    }
                FinishRot script2 = (FinishRot)minedetect.GetComponent(typeof(FinishRot));
                script2.Finished(rot);
            }
        }
    }
}