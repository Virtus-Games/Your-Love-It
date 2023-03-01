using UnityEngine;

[System.Serializable]
public enum RayType
{
    BACK,
    FRONT,
    LEFT,
    RIGHT
}

public class RayController : MonoBehaviour
{

    public RayType rayType;
    public float rayLength;
    public LayerMask GrassLayer;
    public bool isActive = false;

    public PlayerController playerController;




    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector3.down * rayLength);
    }



    private void Update()
    {
        if (GameManager.Instance.Gamestate == GameManager.GAMESTATE.Ingame && BuidingManager.Instance.isCompledTimer)
        {
            if (!isDeactive)
            {
                RaycastHit hit;

                if (Physics.Raycast(transform.position, Vector3.down, out hit, rayLength, GrassLayer))
                {
                    if (hit.collider.gameObject.TryGetComponent<AutoRotate>(out AutoRotate autoRotate) && rayType == RayType.FRONT)
                    {
                        Debug.Log("Front");
                        playerController.AutoRotating(autoRotate.GetRotation());
                    }
                    else
                        isActive = true;
                }

                else
                    isActive = false;
            }
        }
    }

    public bool isDeactive = false;

    public bool isGetRay
    {
        get
        {
            if (!isDeactive && isActive)
                return true;
            else
                return false;
        }
    }


}
