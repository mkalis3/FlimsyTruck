using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Exploder2 : MonoBehaviour
{

    public float explosionTime = 0;
    public float randomizeExplosionTime = 0;
    public float radius = 15;
    public float power = 1;
    public int probeCount = 150;
    public float explodeDuration = 0.5f;

    protected bool exploded = false;

    GameObject boom, bbike2;
    AudioSource boom2;

    public virtual IEnumerator explode()
    {
        Bike script = (Bike)bbike2.GetComponent(typeof(Bike));
            if (script.audio == 1)
            {
                boom2.Play();
            }
            ExploderComponent2[] components = GetComponents<ExploderComponent2>();
            foreach (ExploderComponent2 component in components)
            {
                if (component.enabled)
                {
                    component.onExplosionStarted(this);
                }
            }
            while (explodeDuration > Time.time - explosionTime)
            {
                disableCollider();
                for (int i = 0; i < probeCount; i++)
                {
                    shootFromCurrentPosition();
                }
                enableCollider();
                yield return new WaitForFixedUpdate();
            }
    }

    protected virtual void shootFromCurrentPosition()
    {
        Vector3 probeDir = Random.onUnitSphere;
        Ray testRay = new Ray(transform.position, probeDir);
        shootRay(testRay, radius);
    }

    protected bool wasTrigger;
    public virtual void disableCollider()
    {
        if (GetComponent<Collider>())
        {
            wasTrigger = GetComponent<Collider>().isTrigger;
            GetComponent<Collider>().isTrigger = true;
        }
    }

    public virtual void enableCollider()
    {
        if (GetComponent<Collider>())
        {
            GetComponent<Collider>().isTrigger = wasTrigger;
        }
    }


    protected virtual void init()
    {
        power *= 500000;

        if (randomizeExplosionTime > 0.01f)
        {
            explosionTime += Random.Range(0.0f, randomizeExplosionTime);
        }
    }

    void Start()
    {
        init();

        bbike2 = GameObject.Find("bbike2");
        boom = GameObject.Find("boom");
        boom2 = boom.GetComponent<AudioSource>();
    }

    /*void FixedUpdate() {
		if (Time.time > explosionTime && !exploded) {
			exploded = true;
			StartCoroutine("explode");
		}
	}*/

    public void explodenow()
    {
        StartCoroutine("explode");
    }

    private void shootRay(Ray testRay, float estimatedRadius)
    {
        RaycastHit hit;
        if (Physics.Raycast(testRay, out hit, estimatedRadius))
        {
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForceAtPosition(power * Time.deltaTime * testRay.direction / probeCount, hit.point);
                estimatedRadius /= 2;
            }
            else
            {
                Vector3 reflectVec = Random.onUnitSphere;
                if (Vector3.Dot(reflectVec, hit.normal) < 0)
                {
                    reflectVec *= -1;
                }
                Ray emittedRay = new Ray(hit.point, reflectVec);
                shootRay(emittedRay, estimatedRadius - hit.distance);
            }
        }
    }
}
