using UnityEngine;
using UnityEngine.AI;


[System.Serializable]
public enum RotationTypes
{
    WAYPOINT,
    Arround,
    RAYCAST
}


[RequireComponent(typeof(NavMeshAgent))]
public class PlayerController : MonoBehaviour
{
    [TextArea(3, 10)]
    public string sentences;
    [HideInInspector]
    public NavMeshAgent agent;
    public string wall = "Wall";
    public float WallWaitingTimer;
    Quaternion goRotation;

    [HideInInspector]
    public bool isCatchWalk = false;

    [HideInInspector]
    public Move move;
    Vector3 kissPoint;
    [SerializeField] private float time = 3;
    public Vector3 offset;
    public float waitTimeForAround;
    private PlayerAnimatorController playerAnimatorController;
    
    [HideInInspector]
    public EnumuratorController enumuratorController;

    #region  Value Settings
    public bool isStop = false;
    public bool isStopChrackter = false;
    public bool isWaypointStop = false;
    public bool isArround = false;
    public bool isRayActive = false;

    #endregion



    private void Start()
    {
        move = GetComponent<Move>();
        playerAnimatorController = GetComponent<PlayerAnimatorController>();
        enumuratorController = GetComponent<EnumuratorController>();
        agent = GetComponent<NavMeshAgent>();
        isStop = true;
        agent.updateRotation = false;
    }



    #region On Collider Hit
    public void AnimatorKiss()
    {
        agent.enabled = false;
        isStopChrackter = false;
        isStop = true;
        CameraController.Instance.KissPointOffset(ManKissPoint.Instance.Point);
        playerAnimatorController.AnimatorController(playerAnimatorController.isKiss);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (GameManager.Instance.Gamestate == GameManager.GAMESTATE.Ingame)
        {
            PointFinishTrigger(other);
            PlacesAndNodens(other);
        }
    }


    public void WaypointDetected(Transform other)
    {
        move.currentWaypointIndex = move.GetChildIndex(other.transform);

    }

    private void PlacesAndNodens(Collider other)
    {

        if (other.gameObject.CompareTag("Places") && other.gameObject.GetComponent<Node>().isHave)
        {
            other.gameObject.GetComponent<Node>().CloseArround(waitTimeForAround);
        }

        else
        {
            if (other.CompareTag("Waypoint") && isArround)
            {
                move.currentWaypointIndex = move.GetChildIndex(other.transform);
            }
        }

        if (other.gameObject.TryGetComponent<AroundRotation>(out var around) && !around.isStatus)
        {
            Quaternion arrowRotation = Quaternion.Euler(around.GetRotation());



            if (Quaternion.Angle(arrowRotation, move.NextRotation) < 5)
            {
                move.enabled = false;
                goRotation = move.NextRotation;
                isStop = true;
                typeRotate = RotationTypes.Arround;
                WalkingController(typeRotate);
                RotateionType(RotationTypes.Arround, arrowRotation);
            }
            else
            {
                if (!around.isStatus)
                {
                    move.enabled = false;
                    isStop = true;
                    goRotation = arrowRotation;
                    RotateionType(RotationTypes.Arround, arrowRotation);
                }

                else if (around.isStatus)
                {
                    isStop = true;
                    goRotation = move.NextRotation;
                    RotateionType(RotationTypes.WAYPOINT, arrowRotation);
                }
            }
        }
    }

    public void LastPosition()
    {
        move.enabled = false;
        GetComponent<RayManager>().enabled = false;
        isStopChrackter = true;
        StopAllCoroutines();
        agent.enabled = true;
        kissPoint = FindObjectOfType<ManPoint>().kissingPoint;
        agent.SetDestination(kissPoint);
    }


    private void PointFinishTrigger(Collider other)
    {
        if (other.gameObject.CompareTag("Point"))
            LastPosition();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            agent.enabled = false;

            time -= Time.deltaTime;

            if (time <= 0)
            {
                BuidingManager.Instance.ResetValue();
                GameManager.Instance.Gamestate = GameManager.GAMESTATE.Finish;
            }

        }
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag(wall))
        {
            WallWaitingTimer -= Time.deltaTime;

            if (WallWaitingTimer <= 0)
            {
                GameManager.Instance.Gamestate = GameManager.GAMESTATE.GameOver;
                Destroy(gameObject);
            }
        }
    }
    #endregion

    public void AutoRotating(Quaternion quaternion)
    {
        isArround = false;
        isStop = true;
        isWaypointStop = true;
        agent.enabled = false;
        isRayActive = true;
        StartCoroutine(enumuratorController.AutoRate(quaternion));

    }

    [HideInInspector]
    public RotationTypes typeRotate;
    public void WalkingController(RotationTypes type)
    {

        switch (type)
        {
            case RotationTypes.WAYPOINT:
                isStop = false;
                isWaypointStop = false;
                isArround = false;
                isRayActive = false;
                agent.enabled = true;
                break;
            case RotationTypes.Arround:
                isStop = false;
                isWaypointStop = false;
                isArround = true;
                isRayActive = false;
                agent.enabled = false;
                break;
            case RotationTypes.RAYCAST:
                isStop = false;
                isWaypointStop = false;
                isArround = false;
                isRayActive = true;
                agent.enabled = false;
                break;
        }

        typeRotate = type;

        if (!isCatchWalk)
            playerAnimatorController.AnimatorController(playerAnimatorController.walk);
        else
            playerAnimatorController.AnimatorController(playerAnimatorController.catchWalk);
    }


    public void RotateionType(RotationTypes type, Quaternion quaternion)
    {
        StopAllCoroutines();

        switch (type)
        {
            case RotationTypes.WAYPOINT:
                StartCoroutine(enumuratorController.WaypointRotation(quaternion));
                break;
            case RotationTypes.Arround:
                StartCoroutine(enumuratorController.Walking(quaternion));
                agent.enabled = false;
                isArround = true;
                break;
            case RotationTypes.RAYCAST:
                StartCoroutine(enumuratorController.Walking(quaternion));
                isRayActive = true;
                break;
        }
    }

}

