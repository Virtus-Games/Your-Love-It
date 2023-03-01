using UnityEngine;

public class AutoRotate : MonoBehaviour
{
    Quaternion rotate;

    void Start() => rotate = transform.rotation;


    public Quaternion GetRotation()
    {
        return rotate;
    }
    
}
