
using UnityEngine;

public class ManPoint : MonoBehaviour
{
    private Animator _Animator;
    public GameObject KissPoint;


    public Vector3 kissingPoint
    {
        get { return KissPoint.transform.position; }

    }
}
