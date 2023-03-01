using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManKissPoint : Singleton<ManKissPoint>
{

    public ParticleSystem[] conlicly;
    public Transform Point;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GetComponentInParent<Animator>().SetBool("isKiss", true);
            other.gameObject.GetComponent<PlayerController>().AnimatorKiss();
        }
    }

    public void PlayParticle()
    {
        foreach (var item in conlicly)
        {
            item.Play();
        }
    }
}
