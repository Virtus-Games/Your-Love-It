
using UnityEngine;

public class Models : MonoBehaviour
{
    public ItemType type;
    public float BeatifulBarVal;

    public GameObject Model;

    private void Start()
    {
        if (Model != null){
            Model.SetActive(true);
        }
         
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.TryGetComponent<CharackterChanged>(out CharackterChanged chr))
        {
            if (Model != null)
                chr.Changing(type, Model);
            else
                chr.Changing(type, gameObject);

            chr.BeatifulValue(BeatifulBarVal);
            gameObject.SetActive(false);
        }
    }

    public void SetType(int val, ItemType types)
    {
        type = types;
    }
}
