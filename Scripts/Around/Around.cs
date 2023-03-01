using UnityEngine;
using System.Collections;

public class Around : MonoBehaviour
{
    private const string _placesTag = "Places";

    private MeshRenderer rend;
    private Color startColor;
    public bool isPlaces = false;
    public bool isBreak = false;
    bool isGo = false;

    public Color HoverColor;

    public bool GetPlaces()
    {
        return isPlaces;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 10);
    }

    public void Start()
    {
        rend = GetComponent<MeshRenderer>();
        startColor = rend.material.color;
    }

    private void Update()
    {
        if (BuidingManager.Instance.isCompledTimer && !isPlaces && !isBreak)
        {
            gameObject.SetActive(false);
        }
    }


    public void DeactiveValue()
    {
        isPlaces = false;
        isBreak = false;
    }


    public void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag(_placesTag) && !other.gameObject.GetComponent<Node>().isHave)
        {
            other.GetComponent<Node>().GetArround(gameObject);
            GetComponent<MeshRenderer>().material.color = Color.green;
            isPlaces = true;
        }
        else
            GetComponent<MeshRenderer>().material.color = startColor;

    }

    Node lastNode;
    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.CompareTag(_placesTag) && isBreak
                     && !other.gameObject.GetComponent<Node>().isHave)
        {
            lastNode = other.gameObject.GetComponent<Node>();

            isPlaces = true;

            other.gameObject.GetComponent<Node>().isTime = true;

            transform.localPosition = Vector3.zero;

            other.gameObject.GetComponent<Node>().CloseRendImage(true);
        }

    }

    public void GoPlaces(GameObject other)
    {
        transform.SetParent(other.gameObject.transform);

        Vector3 pos = new Vector3(0, -4.31f, 0);
        isGo = true;
        StartCoroutine(GoPosition(pos, 0, false, null, 10));
    }


    IEnumerator GoPosition(Vector3 pos, float waitTimer, bool isDeActive, GameObject obj, float speed)
    {

        yield return new WaitForSeconds(waitTimer);

        while (transform.localPosition != pos && isGo)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, pos, Time.deltaTime * speed);

            if (transform.localPosition == pos)
            {
                lastNode.ClosePlaka();
                GetComponent<BoxCollider>().isTrigger = false;

                isGo = false;

                if (isDeActive) gameObject.SetActive(false);
                if (obj != null) obj.SetActive(false);
            }

            yield return null;
        }
    }

}


