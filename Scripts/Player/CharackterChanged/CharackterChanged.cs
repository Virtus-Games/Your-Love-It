using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum ItemType
{
    HAIR,
    BODY,
    HEAD,
    SHOES
}


public class CharackterChanged : MonoBehaviour
{
    private const string catchWalk = "catchWalk";

    [Range(0, 0.7f)]
    public float minBeatifulBarValue = 0.2f;
    public Transform[] bodyParent;
    public Transform[] HairParent;
    public Transform[] LeftShoes;
    public Transform[] RightShoes;
    public ParticleSystem DirtParticle;
    public ParticleSystem WaterParticle;
    public ParticleSystem WearParticle;

    private bool isPassMinValue;

    public void Changing(ItemType type, GameObject obj)
    {

        switch (type)
        {
            case ItemType.HAIR:
                ActiveObject(HairParent, obj);
                break;
            case ItemType.BODY:
                ActiveObject(bodyParent, obj);
                break;
        }
    }


    public void ChangesShoes(ShoesType type, GameObject obj)
    {
        switch (type)
        {
            case ShoesType.Left:
                ActiveObject(LeftShoes, obj);
                Debug.Log("Left");
                break;
            case ShoesType.Right:
                ActiveObject(RightShoes, obj);

                break;
        }
    }

    PlayerController player;
    private void Start()
    {
        player = GetComponent<PlayerController>();
    }

    public void BeatifulValue(float val)
    {

        float beatifulValueBar = GameManager.Instance.BeatfiulBarController(val);

        if (beatifulValueBar >= minBeatifulBarValue)
        {
            isPassMinValue = true;
            DirtParticle.Stop();

            BuidingManager.Instance.SetSupportes(false);

            player.isCatchWalk = true;

            GetComponent<PlayerAnimatorController>().AnimatorController(catchWalk);
        }
    }

    List<Material> mat = new List<Material>();

    public void ActiveObject(Transform[] parent, GameObject comeObj)
    {
        WearParticle.Play();

        mat.Clear();

        foreach (Transform item in parent)
        {
            item.gameObject.SetActive(false);
        }

        foreach (Transform item in parent)
        {

            string[] splits = comeObj.name.Split(' ');
            if (item.name == splits[0])
            {

                item.gameObject.SetActive(true);

                if (item.gameObject.activeInHierarchy)
                {
                    if (item.GetComponent<SkinnedMeshRenderer>() != null)
                    {
                        SkinneddMeshConttroller(item.gameObject, comeObj);
                    }
                    else if (item.GetComponent<MeshRenderer>() != null)
                    {
                        MeshMatController(item.gameObject, comeObj);
                    }
                }
            }
            else
                item.gameObject.SetActive(false);
        }


    }


    void SkinneddMeshConttroller(GameObject item, GameObject comeObj)
    {
        Material[] cObj = comeObj.GetComponent<SkinnedMeshRenderer>().materials;
        Material[] dontDestroy = item.GetComponent<MatController>().GetMaterials();
        Material[] itemMat = item.GetComponent<SkinnedMeshRenderer>().materials;

        foreach (Material itemMatItem in itemMat)
            Destroy(itemMatItem);

        if (dontDestroy != null)
        {
            foreach (Material dontDestroyItem in dontDestroy)
                mat.Add(dontDestroyItem);
        }

        foreach (Material cbo in cObj)
            mat.Add(cbo);

        Material[] newMat = new Material[mat.Count];


        for (int i = 0; i < mat.Count; i++)
            itemMat[i] = mat[i];

        item.GetComponent<SkinnedMeshRenderer>().materials = itemMat;
        item.GetComponent<SkinnedMeshRenderer>().sharedMaterials = itemMat;
    }


    void MeshMatController(GameObject item, GameObject comeObj)
    {

        Material[] cObj = comeObj.GetComponent<MeshRenderer>().materials;
        Material[] dontDestroy = item.GetComponent<MatController>().GetMaterials();
        Material[] itemMat = item.GetComponent<MeshRenderer>().materials;

        foreach (Material itemMatItem in itemMat)
            Destroy(itemMatItem);

        if (dontDestroy != null)
        {
            foreach (Material dontDestroyItem in dontDestroy)
                mat.Add(dontDestroyItem);
        }

        foreach (Material cbo in cObj)
            mat.Add(cbo);

        Material[] newMat = new Material[mat.Count];

        for (int i = 0; i < newMat.Length; i++)
            itemMat[i] = mat[i];

        item.GetComponent<MeshRenderer>().materials = itemMat;
    }
    public void WaterController(WASHER washer)
    {


        switch (washer)
        {
            case WASHER.Dirt:
                WaterParticle.gameObject.SetActive(false);
                DirtParticle.gameObject.SetActive(true);
                BeatifulValue(-5);
                DirtParticle.Play();
                break;
            case WASHER.Water:
                DirtParticle.gameObject.SetActive(false);
                WaterParticle.gameObject.SetActive(true);
                WaterParticle.Play();
                break;

        }
    }

}

