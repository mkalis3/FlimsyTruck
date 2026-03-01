using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Around : MonoBehaviour
{
    public float fRadius = 3.0f;
    public int play, wheelupside;
    int right, left, dir, mobile, intouch, thetouch = 10;
    float spin, lastangle, maxspin = 574, spinc;
    Vector2 startf;
    GameObject maincamera, wheel, towtruck, bike, artext;

    void Start()
    {
        maincamera = GameObject.Find("Main Camera");
        wheel = GameObject.Find("2dwheel");
        towtruck = GameObject.Find("towtruck");
        bike = GameObject.Find("bbike2");
        artext = GameObject.Find("artext");

        ResetWheel();

        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.BlackBerryPlayer || Application.platform == RuntimePlatform.WP8Player)
        {
            mobile = 1;
        }
    }

    void Update()
    {
        if (play == 1)
        {
            if (mobile == 0)
            {
                if (Input.GetMouseButton(0))
                {
                    RaycastHit hit;
                    Ray ray = maincamera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);

                    if (Physics.Raycast(ray, out hit) && new Vector2(Input.mousePosition.x, Input.mousePosition.y) != startf)
                    {
                        if (hit.transform.name == "2dwheel" && spinc < 1079 && spinc > -1079)
                        {
                            Vector3 v3Pos = Camera.main.WorldToScreenPoint(transform.position);
                            v3Pos = Input.mousePosition - v3Pos;
                            float angle2 = (Mathf.Atan2(v3Pos.y, v3Pos.x) * Mathf.Rad2Deg) - 95;
                            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, angle2);

                            float angle = transform.localEulerAngles.z;

                            if (spinc >= maxspin)
                            {
                                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 0);
                            }
                            if ((int)lastangle == 0)
                            {
                                if (right == 0 && left == 0)
                                {
                                    //print("angle " + angle);
                                    if (angle > 300)
                                    {
                                        right = 1;
                                    }
                                    else
                                    {
                                        left = 1;
                                    }
                                }
                            }

                            if (right == 1)
                            {
                                if ((int)lastangle != 360)
                                {
                                    if (angle < lastangle)
                                    {
                                        if (lastangle - angle < 200)
                                        {
                                            dir = 1;
                                            if (spinc <= maxspin)
                                            {
                                                spinc += (int)(lastangle - angle);
                                            }
                                        }
                                    }
                                    else if (angle > lastangle)
                                    {
                                        if (angle - lastangle < 200)
                                        {
                                            dir = 2;
                                            spinc += (int)(lastangle - angle);
                                        }
                                    }

                                    if (spinc <= 0)
                                    {
                                        right = 0;
                                        left = 1;
                                    }
                                }
                            }

                            if (left == 1)
                            {
                                if ((int)lastangle != 360)
                                {
                                    if (angle > lastangle)
                                    {
                                        if (angle - lastangle < 200)
                                        {
                                            dir = 2;
                                            if (spinc <= maxspin)
                                            {
                                                spinc += (int)(angle - lastangle);
                                            }
                                        }
                                    }
                                    else if (angle < lastangle)
                                    {
                                        if (lastangle - angle < 200)
                                        {
                                            dir = 1;
                                            spinc -= (int)(lastangle - angle);
                                        }
                                    }

                                    if (spinc <= 0)
                                    {
                                        left = 0;
                                        right = 1;
                                    }
                                }
                            }
                            Bike bike2 = (Bike)bike.GetComponent(typeof(Bike));
                            float kspeed = bike2.GetSpeed();
                            float tspeed = 0;
                            //print("kspeed " + kspeed);
                            if (kspeed < 1)
                            {
                                tspeed = spinc * 0.0005f;
                            }
                            else
                            {
                                tspeed = spinc * 0.005f;
                            }
                            if (mobile == 1)
                            {
                                tspeed = tspeed * 2;
                            }
                            if (right == 1)
                            {
                                //print("right " + spinc + " " + Time.fixedTime);
                                if (wheelupside == 0)
                                {
                                    towtruck.transform.Rotate(0, tspeed, 0);
                                }
                                else
                                {
                                    towtruck.transform.Rotate(0, -tspeed, 0);
                                }
                            }
                            if (left == 1)
                            {
                                //print("left " + spinc + " " + Time.fixedTime);
                                if (wheelupside == 0)
                                {
                                    towtruck.transform.Rotate(0, -tspeed, 0);
                                }
                                else
                                {
                                    towtruck.transform.Rotate(0, tspeed, 0);
                                }
                            }
                            lastangle = angle;

                        }
                        else
                        {
                            GoBack();
                        }
                    }
                }
                else
                {
                    GoBack();
                }
            }
            else
            {
                int tcount = Input.touchCount;
                if(thetouch+1 > tcount)
                {
                    intouch = 0;
                    GoBack();
                    thetouch = 10;
                }
                if (tcount > 0)
                {
                    for (int i = 0; i < tcount; ++i)
                    {
                        if (intouch == 1)
                        {
                            if (Input.GetTouch(thetouch).phase == TouchPhase.Ended || Input.GetTouch(thetouch).phase == TouchPhase.Canceled)
                                {
                                    intouch = 0;
                                    GoBack();
                                    thetouch = 10;
                                }
                        }
                        else
                        {
                            if ((int)transform.localEulerAngles.z != 0)
                            {
                                GoBack();
                            }

                        }
                        RaycastHit hit;
                        Ray ray = maincamera.GetComponent<Camera>().ScreenPointToRay(Input.GetTouch(i).position);

                        if (Physics.Raycast(ray, out hit) && Input.GetTouch(i).position != startf)
                        {
                            if (hit.transform.name == "2dwheel" && spinc < 1079 && spinc > -1079)
                            {
                                if (intouch == 0)
                                {
                                    intouch = 1;
                                    startf = Input.GetTouch(i).position;
                                    thetouch = i;
                                }
                                Vector2 v3Pos = Camera.main.WorldToScreenPoint(transform.position);
                                v3Pos = Input.GetTouch(i).position - v3Pos;
                                float angle2 = (Mathf.Atan2(v3Pos.y, v3Pos.x) * Mathf.Rad2Deg) - 95;
                                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, angle2);

                                float angle = transform.localEulerAngles.z;

                                if (spinc >= maxspin)
                                {
                                    transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 0);
                                }
                                if ((int)lastangle == 0)
                                {
                                    if (right == 0 && left == 0)
                                    {
                                        //print("angle " + angle);
                                        if (angle > 300)
                                        {
                                            right = 1;
                                        }
                                        else
                                        {
                                            left = 1;
                                        }
                                    }
                                }

                                if (right == 1)
                                {
                                    if ((int)lastangle != 360)
                                    {
                                        if (angle < lastangle)
                                        {
                                            if (lastangle - angle < 200)
                                            {
                                                dir = 1;
                                                if (spinc <= maxspin)
                                                {
                                                    spinc += (int)(lastangle - angle);
                                                }
                                            }
                                        }
                                        else if (angle > lastangle)
                                        {
                                            if (angle - lastangle < 200)
                                            {
                                                dir = 2;
                                                spinc += (int)(lastangle - angle);
                                            }
                                        }

                                        if (spinc <= 0)
                                        {
                                            right = 0;
                                            left = 1;
                                        }
                                    }
                                }

                                if (left == 1)
                                {
                                    if ((int)lastangle != 360)
                                    {
                                        if (angle > lastangle)
                                        {
                                            if (angle - lastangle < 200)
                                            {
                                                dir = 2;
                                                if (spinc <= maxspin)
                                                {
                                                    spinc += (int)(angle - lastangle);
                                                }
                                            }
                                        }
                                        else if (angle < lastangle)
                                        {
                                            if (lastangle - angle < 200)
                                            {
                                                dir = 1;
                                                spinc -= (int)(lastangle - angle);
                                            }
                                        }

                                        if (spinc <= 0)
                                        {
                                            left = 0;
                                            right = 1;
                                        }
                                    }
                                }
                                Bike bike2 = (Bike)bike.GetComponent(typeof(Bike));
                                float kspeed = bike2.GetSpeed();
                                float tspeed = 0;
                                if (kspeed < 1)
                                {
                                    tspeed = spinc * 0.00005f;
                                }
                                else
                                {
                                    if(kspeed > 5)
                                    {
                                        kspeed = 5;
                                    }
                                    tspeed = spinc * (0.001f*kspeed);
                                }
                                if (mobile == 1)
                                {
                                    //tspeed = tspeed * 2;
                                }
                                if (right == 1)
                                {
                                    //print("right " + spinc + " " + Time.fixedTime);
                                    if (wheelupside == 0)
                                    {
                                        towtruck.transform.Rotate(0, tspeed, 0);
                                    }
                                    else
                                    {
                                        towtruck.transform.Rotate(0, -tspeed, 0);
                                    }
                                }
                                if (left == 1)
                                {
                                    //print("left " + spinc + " " + Time.fixedTime);
                                    if (wheelupside == 0)
                                    {
                                        towtruck.transform.Rotate(0, -tspeed, 0);
                                    }
                                    else
                                    {
                                        towtruck.transform.Rotate(0, tspeed, 0);
                                    }
                                }
                                lastangle = angle;

                            }
                            else
                            {
                                if (thetouch == i)
                                {
                                    GoBack();
                                }
                            }
                        }
                        else
                        {
                            if (thetouch == i)
                            {
                                GoBack();
                            }
                        }
                    }
                }
                else
                {
                    GoBack();
                }
            }
        }
    }

    void OnMouseDown()
    {
        if (mobile == 0)
        {
            startf = Input.mousePosition;
        }
    }

    void GoBack()
    {
        if (spinc > 0)
        {
            float tspeed = 0;
            if (spinc < 60)
            {
                spinc -= 20;
                tspeed = 20;
            }
            else
            {
                spinc -= 30;
                tspeed = 30;
            }
            if (right == 1)
            {
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z + tspeed);
            }
            if (left == 1)
            {
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z - tspeed);
            }

            lastangle = transform.localEulerAngles.z;
        }
        else
        {
            spinc = 0;
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 0);
            right = 0;
            left = 0;
            dir = 0;
            lastangle = 0;
        }
        /*float bspeed = 1;
        float xpos = wheel.GetComponent<RectTransform>().sizeDelta.x / 2;
        float wz = wheel.transform.localEulerAngles.z;
        if (spinc > 0)
        {
            wheel.transform.Rotate(0, 0, bspeed);
            spinc -= 1;

        }
        else if (spinc < 0)
        {
            wheel.transform.Rotate(0, 0, -bspeed);
            spinc += 1;
        }

       // print(spinc);

        spin = 0;


        lastangle = wz;*/
    }

    public void ResetWheel()
    {
        //print("reset");
        play = 0;
        right = 0;
        left = 0;
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 0);
        spinc = 0;
        dir = 0;
        lastangle = 0;
    }
}