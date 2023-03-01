using UnityEngine;

[System.Serializable]
public enum ShoesType
{
    Left,
    Right
}
[System.Serializable]
public struct Shoesed
{
    public GameObject LeftShoes;
    public ShoesType type;
}

public class Shoes : MonoBehaviour
{
    public Shoesed LeftShoese;
    public Shoesed RightShose;

    public int beatifulValue;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<CharackterChanged>(out CharackterChanged chr))
        {
            chr.ChangesShoes(LeftShoese.type, LeftShoese.LeftShoes);
            chr.ChangesShoes(RightShose.type, RightShose.LeftShoes);
            chr.BeatifulValue(beatifulValue);
            gameObject.SetActive(false);
        }
    }
}