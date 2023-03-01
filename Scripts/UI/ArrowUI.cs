using UnityEngine;
using UnityEngine.EventSystems;

public class ArrowUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public GameObject arrow;
    private GameObject _arrow;
    public float positionZ = 40.0f;
    private bool _isActive = false;
    private float yPosition;

    private void Start()
    {

       
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
        if (GameManager.Instance.Gamestate == GameManager.GAMESTATE.Ingame)
        {
             yPosition = FindObjectOfType<PlayerController>().transform.position.y;
            if (_arrow != null)
            {
                _arrow.SetActive(true);
                _isActive = true;
            }
            else
            {
                _arrow = Instantiate(arrow, new Vector3(0, 1, positionZ), Quaternion.identity, transform);
                _arrow.transform.SetParent(null);
                _isActive = true;
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (GameManager.Instance.Gamestate != GameManager.GAMESTATE.Ingame && _arrow != null)
        {
            _arrow.SetActive(false);
        }
        else
        {
            if (_arrow != null && _isActive)
            {
                RaycastHit hit = GetHit();

                yPosition = FindObjectOfType<PlayerController>().transform.position.y;
                _arrow.transform.position = new Vector3(hit.point.x, yPosition, hit.point.z);
            }
        }


    }

    public RaycastHit GetHit()
    {
        Vector3 screenMousePosFar = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.farClipPlane);
        Vector3 screenMousePosNear = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane);
        Vector3 mousePosFar = Camera.main.ScreenToWorldPoint(screenMousePosFar);
        Vector3 mousePosNear = Camera.main.ScreenToWorldPoint(screenMousePosNear);
        RaycastHit hit;
        Physics.Raycast(mousePosNear, mousePosFar - mousePosNear, out hit);
        return hit;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (_arrow.GetComponent<Around>().GetPlaces())
        {
            _arrow.GetComponent<Around>().isBreak = true;
            _arrow.gameObject.tag = "Around";
            _arrow = null;
            _isActive = false;
        }

        if (_arrow != null)
        {
            _arrow.gameObject.SetActive(false);
            _isActive = false;
        }
    }
}