using System;
using System.Collections;
using UnityEngine;

public class Node : MonoBehaviour
{
    private GameObject arrow;
    public Vector3 Offest;
    public float time = 0.5f;
    public bool isHave = false;
    private Renderer rend;
    public Color hoverColor;
    public Color closeColor;
    private Color startColor;
    public LayerMask AroundMask;

    public GameObject Plaka;
    //public GameObject AutoRotate;

    public GameObject lwaypoint;

    void Start()
    {
        rend = GetComponentInParent<Renderer>();
        startColor = rend.material.color;
        rend.material.color = hoverColor;

        Collider[] waypoints = Physics.OverlapSphere(transform.position, 0.3f);

        foreach (Collider waypoint in waypoints)
        {
            if (waypoint.gameObject.tag == "Waypoint")
            {
                lwaypoint = waypoint.gameObject;
                waypoint.gameObject.SetActive(false);
                Debug.Log(lwaypoint.name); lwaypoint = waypoint.gameObject;
            }
        }
    }

    public bool isTime = true;

    private void Update()
    {

        if (!Around) return;

        if (Vector3.Distance(transform.position, Around.transform.position) < 2f
                    && Around.GetComponent<Around>().isBreak && isTime)
        {
            Around.GetComponent<Around>().GoPlaces(gameObject);
            isTime = false;
        }

        if (Vector3.Distance(transform.position, Around.transform.position) > 2f
            && GameManager.Instance.Gamestate == GameManager.GAMESTATE.Ingame)
        {
            Around.GetComponent<Around>().DeactiveValue();
            isHave = false;
            Around = null;
        }

    }
    public GameObject Around = null;

    public void GetArround(GameObject arround)
    {
        Around = arround;
    }

    public void CloseRendImage(bool isStatus)
    {

        bool isChildCount = transform.childCount > 0 ? true : false;

        if (!isChildCount)
        {
            transform.parent.GetComponent<MeshRenderer>().material.color = closeColor;
            this.enabled = false;
        }

        isHave = isStatus;

        if (isHave)
            rend.material.color = startColor;
        else
        {
            rend.material.color = startColor;
            ClosePlaka();
        }

    }

    public void ClosePlaka() => Plaka.SetActive(false);

    public void CloseArround(float timer)
    {

        if (lwaypoint != null)
        {
            lwaypoint.SetActive(true);
        }

        if (GetComponentInChildren<Around>() != null)
            Close(timer);

    }



    public void Close(float timer)
    {
        StartCoroutine(Closed(timer));
    }

    IEnumerator Closed(float waitTimer)
    {

        while (waitTimer > 0)
        {
            waitTimer -= Time.deltaTime;
            yield return null;
        }

        transform.parent.gameObject.SetActive(false);
    }
}
