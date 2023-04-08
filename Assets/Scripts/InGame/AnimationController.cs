using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public string attackPram = "Attack";
    public string movePram = "Move";
    public string deathPram = "Death";
    public Animator animator;

    private void SetBool(string pramName, bool value)
    {
        if (animator == null)
            return;

        animator.SetBool(pramName, value);
    }

    private void SetTrigger(string pramName)
    {
        if (animator == null)
            return;

        animator.SetTrigger(pramName);
    }

    private void Attack()
    {
        if (animator == null)
            return;

        animator.SetTrigger(attackPram);
    }

    private void Move(bool value)
    {
        if (animator == null)
            return;

        animator.SetBool(movePram, value);
    }

    private void Death(bool value)
    {
        if (animator == null)
            return;

        animator.SetBool(deathPram, value);
    }

    private void Start()
    {
        if (animator != null)
            return;

        animator = GetComponent<Animator>();
    }
}
