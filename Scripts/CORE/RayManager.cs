using System.Collections.Generic;
using UnityEngine;

public class RayManager : MonoBehaviour
{
    public RayController Front;
    public RayController Left;
    public RayController Right;

    private List<RayController> frontAndLeft = new List<RayController>();
    private List<RayController> frontAndRight = new List<RayController>();
    private List<RayController> allRayList = new List<RayController>();

    private PlayerController playerController;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        Addding();
    }

    void Addding()
    {
        frontAndLeft.Add(Front);
        frontAndLeft.Add(Left);

        frontAndRight.Add(Front);
        frontAndRight.Add(Right);

        allRayList.Add(Front);
        allRayList.Add(Left);
        allRayList.Add(Right);
    }

    private void Update()
    {
        if (GameManager.Instance.Gamestate == GameManager.GAMESTATE.Ingame && BuidingManager.Instance.isCompledTimer)
        {

            if (!playerController.isRayActive)
            {
                if (Front.isGetRay && Left.isGetRay && Right.isGetRay)
                {
                    playerController.isRayActive = true;

                    float yRotate = transform.eulerAngles.y;

                    yRotate = Mathf.Abs(yRotate);


                    if (yRotate >= 0 && yRotate <= 89)
                        yRotate = 180;
                    else if (yRotate > 90 && yRotate <= 181)
                        yRotate = 0;
                    else if (yRotate > 181 && yRotate <= 271)
                        yRotate = 90;
                    else
                        yRotate += 90;

                    Front.isDeactive = true;
                    Left.isDeactive = true;
                    Right.isDeactive = true;


                    Quaternion playerRotateBack = Quaternion.Euler(transform.rotation.x, yRotate, transform.rotation.z);
                    playerController.enumuratorController.RayAutoRotating(allRayList, playerRotateBack, 180);
                }
                else
                {
                    if (Front.isGetRay && Left.isGetRay)
                    {


                        float yRotate = transform.eulerAngles.y;


                        yRotate = Mathf.Abs(yRotate);

                        if (yRotate >= 0 && yRotate <= 89)
                            yRotate = 90;
                        else if (yRotate > 90 && yRotate <= 179)
                            yRotate = 180;
                        else if (yRotate > 179 && yRotate <= 269)
                            yRotate = 270;
                        else
                            yRotate += 90;

                        playerController.isRayActive = true;

                        Front.isDeactive = true;
                        Left.isDeactive = true;
                        Quaternion toplam = Quaternion.Euler(transform.rotation.x, yRotate, transform.rotation.z);
                        playerController.enumuratorController.RayAutoRotating(frontAndLeft, toplam, 15f);

                    }

                    if (Front.isGetRay && Right.isGetRay)
                    {
                        float yRotate = transform.eulerAngles.y;

                        yRotate = Mathf.Abs(yRotate);

                        if (yRotate >= 0 && yRotate <= 89)
                            yRotate = -90;
                        else if (yRotate > 90 && yRotate <= 179)
                            yRotate = -180;
                        else if (yRotate > 179 && yRotate <= 269)
                            yRotate = -270;
                        else
                            yRotate -= 90;



                        playerController.isRayActive = true;
                        Front.isDeactive = true;
                        Right.isDeactive = true;

                        Quaternion rotating = Quaternion.Euler(transform.rotation.x, yRotate, transform.rotation.z);
                        playerController.enumuratorController.RayAutoRotating(frontAndRight, rotating, 15f);

                    }
                }
            }
        }


    }
    public float YRotateController()
    {
        float yRotate = transform.eulerAngles.y;
        yRotate = Mathf.Abs(yRotate);

        if (yRotate >= 0 && yRotate <= 89)
            yRotate = 90;
        else if (yRotate > 90 && yRotate <= 179)
            yRotate = 180;
        else if (yRotate > 180 && yRotate <= 269)
            yRotate = 270;
        return yRotate;

    }
}
