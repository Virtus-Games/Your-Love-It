using UnityEngine;
using System.Collections;

public class CameraController : Singleton<CameraController>
{
    private Transform _FarTransformPoint;
    public GameObject Player;
    public Vector3 NearOffset;
    public Vector3 FarTransformPointOffset;
    public float speed = 2.0f;
    private Transform _Target;
    private bool isStart = false;


    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        _FarTransformPoint = GameObject.FindWithTag("CameraFarPoint").transform;
        _FarTransformPoint = GameObject.FindWithTag("GrassBuilder").transform;
        isStart = true;
    }




    public void StartCamera()
    {
        PlayerController playerController = Player.GetComponent<PlayerController>();

        playerController.GetComponent<Collider>().enabled = true;

        playerController.GetComponent<PlayerAnimatorController>().AnimatorController("walk");

        //   playerController.GetComponent<Rigidbody>().isKinematic = false;

        playerController.isStop = false;

        isStart = false;
        isPlay = true;


    }




    public void KissPointOffset(Transform kissPoint)
    {
        isStart = false;
        isPlay = false;

        StartCoroutine(GoPosition(kissPoint, true));
        ManKissPoint.Instance.PlayParticle();

    }


    private bool isPlay;
    private void LateUpdate()
    {
        if (isStart)
            PositionRotationController(_FarTransformPoint.transform.position + FarTransformPointOffset, new Vector3(90, 0, 0));
        if (isPlay)
            PositionRotationController(Player.transform.position + NearOffset, new Vector3(30, 0, 0));

    }
    public void PositionRotationController(Vector3 pos, Vector3 rot)
    {
        transform.position = Vector3.Lerp(transform.position, pos, speed * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(rot), speed * Time.deltaTime);
    }

    IEnumerator GoPosition(Transform point, bool isKissPoint)
    {
        _Target = point;

        while (_Target)
        {
            PositionRotationController(_Target.position, _Target.rotation.eulerAngles);

            if (Vector3.Distance(transform.position, _Target.position) < 0.1f)
            {
                if (isKissPoint)
                {

                }

                _Target = null;

                yield break;
            }
            yield return null;
        }
    }




}
