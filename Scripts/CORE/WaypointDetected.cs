using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointDetected : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerController playerController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Waypoint"))
        {
            playerController.WaypointDetected(other.transform);
            playerController.GetComponent<Move>().SetWaypoint();
            other.GetComponent<WaypointChild>().StartCollider();
        }
    }
}
