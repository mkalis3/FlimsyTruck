// Converted from UnityScript to C# at http://www.M2H.nl/files/js_to_c.php - by Mike Hergaarden
// Do test the code! You usually need to change a few small bits.

using UnityEngine;
using System.Collections;

public class WheelRotate : MonoBehaviour
{
    float rotateSpeed = 90;
    float maxRotation = 360;
    GameObject towtruck, bike;

	void Start()
    {
        bike = GameObject.Find("bbike2");
        towtruck = GameObject.Find("towtruck");
    }

    void Update()
    {
        Vector3 forward = Camera.main.transform.TransformDirection(Vector3.forward);


        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButton(0))
        {
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.name == "3dwheel")
                {
                    // Get the point along the ray that hits the calculated distance.
                    Vector3 targetPoint = ray.GetPoint(0);

                    // Compute relative point and get the angle towards it
                    Vector3 relative = transform.InverseTransformPoint(targetPoint);
                    float angle = Mathf.Atan2(relative.x, relative.z) * Mathf.Rad2Deg;

                    float maxRotation = rotateSpeed * Time.deltaTime;
                    float clampedAngle = Mathf.Clamp(angle, -maxRotation, maxRotation);

                    if (Mathf.Abs(angle) > 10) //takes out some jitters, but loses a small amount of accuracy.
                    {
                        transform.Rotate(0, 0, -clampedAngle);
                        towtruck.transform.Rotate(0, clampedAngle/100, 0);
                    }
                }
                else
                {
                    Back();
                }
            }
        }
        else
        {
            Back();
        }
    }

    void Back()
    {
        float bspeed = 0.1f;
        int wz = (int)transform.localEulerAngles.z;
        Bike bike2 = (Bike)bike.GetComponent(typeof(Bike));
        float kspeed = bike2.GetSpeed();
        if (wz >= 181 && wz <= 360)
        {
            transform.Rotate(0, 0, bspeed);
            if (kspeed > 5)
            {
                towtruck.transform.Rotate(0, -bspeed, 0);
            }
        }
        else if (wz >= 0 && wz <= 179)
        {
            transform.Rotate(0, 0, -bspeed);
            if(kspeed > 5)
            {
                towtruck.transform.Rotate(0, bspeed, 0);
            }
        }
    }
}