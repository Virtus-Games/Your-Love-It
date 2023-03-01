
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AroundRotation : MonoBehaviour
{
    public Image arroundImage;
    public AroundSO aroundSO;
    public Transform pos;

    public float timeForClose = 2;


    public bool isStatus;


    public Vector3 GetRotation()
    {
        return aroundSO.AroundRotation;
    }

    private void Start()
    {
        arroundImage.sprite = aroundSO.AroundSprite;
    }

    IEnumerator WaitForClose(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }

    public Vector3 GetPosition()
    {
        isStatus = true;
        pos.SetParent(null);
        return pos.position;
    }




}
