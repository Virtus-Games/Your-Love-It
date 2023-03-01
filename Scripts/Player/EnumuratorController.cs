using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnumuratorController : MonoBehaviour
{
    PlayerController playerController;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
    }
    public IEnumerator Walking(Quaternion rotate)
    {

        Vector3 to = transform.position + transform.forward * 0.5f;

        while (Vector3.Distance(transform.position, to) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, to, Time.deltaTime * playerController.move.lerpSpeed);

         
            yield return null;
        }
        playerController.isStop = true;
        StartCoroutine(AroundRotate(rotate));

    }
    public IEnumerator AroundRotate(Quaternion rotate)
    {
        while (playerController.isStop)
        {
            bool isTarget = QuertionAngle(rotate, 0.1f, playerController.move.rotateSpeed);

            if (isTarget)
            {
                playerController.move.CycleWaypoints();
                playerController.WalkingController(RotationTypes.Arround);
                GetComponent<Move>().enabled = true;
                break;

            }

            yield return null;
        }
    }


    public IEnumerator WaypointRotation(Quaternion rotate)
    {

        while (playerController.isWaypointStop)
        {
            bool isTarget = QuertionAngle(rotate, 0.1f, playerController.move.rotateSpeed);

            if (isTarget)
            {
                playerController.isWaypointStop = false;
                playerController.isStop = false;
                playerController.move.CycleWaypoints();
                playerController.WalkingController(RotationTypes.WAYPOINT);
                break;
            }

            yield return null;
        }
    }

    public IEnumerator RayAutoRotate(List<RayController> rays, Quaternion rotate, float speed)
    {
        while (playerController.isRayActive)
        {
            bool isTarget = QuertionAngle(rotate, 0.1f, speed);

            if (isTarget)
            {
                playerController.WalkingController(RotationTypes.Arround);
                playerController.isRayActive = false;
                foreach (var ray in rays)
                    ray.isDeactive = false;
                break;

            }

            yield return null;
        }
    }


    public bool QuertionAngle(Quaternion rotate, float angle, float speed)
    {
        if (Quaternion.Angle(transform.rotation, rotate) < angle)
            return true;
        else
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, rotate, speed * Time.deltaTime);
            return false;
        }
    }

    public void RayAutoRotating(List<RayController> rays, Quaternion quaternion, float speed)
    {
        playerController.isArround = false;
        playerController.isStop = true;
        playerController.isWaypointStop = true;
        playerController.agent.enabled = false;
        StartCoroutine(RayAutoRotate(rays, quaternion, speed));

    }
    public IEnumerator AutoRate(Quaternion rotate)
    {
        while (playerController.isRayActive)
        {
            bool isTarget = QuertionAngle(rotate, 0.1f, playerController.move.rotateSpeed);

            if (isTarget)
            {
                playerController.isRayActive = false;
                playerController.WalkingController(RotationTypes.Arround);
                playerController.agent.enabled = false;
                break;
            }

            yield return null;
        }
    }
}
