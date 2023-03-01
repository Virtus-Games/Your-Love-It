using UnityEngine;

public class Supportes : MonoBehaviour
{
    Transform PlayerTransofrm;
    public float rotateSpeed = 10;
    private void Start()
    {
        PlayerTransofrm = FindObjectOfType<PlayerController>().transform;
    }
    public void SetAnimatation()
    {
        GetComponent<Animator>().SetBool("isStarted", true);
    }
    public void ResetAnimatation()
    {
        GetComponent<Animator>().SetBool("isStarted", false);
    }

    private void Update()
    {
        Quaternion ditance = Quaternion.LookRotation(PlayerTransofrm.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, ditance, Time.deltaTime * rotateSpeed);
    }
}
