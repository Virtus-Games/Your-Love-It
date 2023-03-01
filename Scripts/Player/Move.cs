using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Move : MonoBehaviour
{
    public float tolerens;
    public float ZSpeed = 10f;
    public float rotateSpeed = 2f;
    public float lerpSpeed = 2f;
    public float WallWaitingTimer = 2f;
    private Waypoint waypoint;
    public int currentWaypointIndex;
    public bool isLastPosition;
    private NavMeshAgent agent;
    private PlayerController playerController;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        playerController = GetComponent<PlayerController>();

        waypoint = FindObjectOfType<Waypoint>();
    }

    void Update()
    {
        if (GameManager.Instance.Gamestate == GameManager.GAMESTATE.Ingame && BuidingManager.Instance.isCompledTimer)
        {
            if (!playerController.isRayActive)
            {
                if ((playerController.isArround) || playerController.isStopChrackter)
                {
                    transform.Translate(0, 0, ZSpeed * Time.deltaTime);
                }
                else
                    WaypointMove();
            }

        }
    }

    public void WaypointMove()
    {
        if (!playerController.isStop && agent.enabled && !playerController.isWaypointStop && !playerController.isStopChrackter)
        {
            if (AtWaypoint())
            {
                playerController.isStop = true;

                playerController.isWaypointStop = true;

                playerController.RotateionType(RotationTypes.WAYPOINT, NextRotation);

                isLastPosition = waypoint.lastPosition(currentWaypointIndex);

                GetTransfromWaypoint(currentWaypointIndex).gameObject.SetActive(false);
            }

            if (waypoint.GetLastIndex())
            {
                GetComponent<RayManager>().enabled = false;
            }
            if (isLastPosition)
            {
                agent.enabled = true;
                playerController.isStopChrackter = false;
                playerController.LastPosition();
            }
            else
            {
                Vector3 nextPosition = GetWaypoint(currentWaypointIndex);
                agent.SetDestination(nextPosition);
            }
        }
    }
    public Vector3 GetWaypoint(int point) => waypoint.GetPosition(point).position;

    public Quaternion NextRotation => waypoint.GetPosition(currentWaypointIndex).rotation;


    public Transform GetTransfromWaypoint(int point) => waypoint.GetPosition(point);

    public void CycleWaypoints() => currentWaypointIndex = waypoint.GetNextIndex(currentWaypointIndex);

    public bool AtWaypoint()
    {
        float distanceToWaypoint = Vector3.Distance(transform.position, GetWaypoint(currentWaypointIndex));

        return distanceToWaypoint < tolerens;
    }

    public int GetChildIndex(Transform child)
    {
        return waypoint.GetChildIndex(child);
    }

    public void SetWaypoint()
    {
        if (playerController.typeRotate == RotationTypes.Arround)
            playerController.WalkingController(RotationTypes.WAYPOINT);
            
    }
}
