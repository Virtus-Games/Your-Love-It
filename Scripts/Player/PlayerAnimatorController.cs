using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    public string catchWalk = "catchWalk";
    public string walk = "walk";
    public string idle = "idle";
    public string isKiss = "isKiss";

    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();

    }

    public void AnimatorController(string val)
    {
        animator.SetBool(catchWalk, false);
        animator.SetBool(idle, false);
        animator.SetBool(walk, false);

        animator.SetBool(val, true);
    }
}
