using UnityEngine;
using System.Collections;

public class MineDetect : MonoBehaviour {

    GameObject bbike2;

    // Use this for initialization
    void Start()
    {
        bbike2 = GameObject.Find("bbike2");
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.collider.name.Contains("mine"))
        {
            Bike script = (Bike)bbike2.GetComponent(typeof(Bike));
            script.Mine();
        }
    }
}
