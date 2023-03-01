using UnityEngine;


[System.Serializable]
public enum WASHER
{
    Dirt,
    Water,
}

public class Washer : MonoBehaviour
{

    public WASHER washerType;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<CharackterChanged>().WaterController(washerType);
        }
    }
}
