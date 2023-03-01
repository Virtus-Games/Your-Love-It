using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointChild : MonoBehaviour
{
    public float timer;

    public void StartCollider()
    {
        StartCoroutine(SphereColliderWaiting());
    }
    IEnumerator SphereColliderWaiting()
    {
        GetComponent<SphereCollider>().enabled = false;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            yield return null;
        }

        GetComponent<SphereCollider>().enabled = true;
    }
}
