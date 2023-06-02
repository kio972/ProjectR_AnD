using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleBoss_AnimationListener : MonoBehaviour
{
    [SerializeField]
    private Animator bossAnimator;
    [SerializeField]
    private List<WingAnimationController> wingAnimators;


    void Wing_Default()
    {
        bool isShield = false;
        float wingSpread = -1f;
        bool isFlap = true;
        foreach (WingAnimationController wing in wingAnimators)
        {
            wing.ChangeBlend("wingSpread", wingSpread, 0.1f);
            wing.SetBool("isShielding", isShield);
            wing.SetBool("isFlapping", isFlap);
        }
    }

    void Wing_Attack_Ready()
    {
        bool isShield = false;
        float wingSpread = 1f;
        bool isFlap = false;
        foreach (WingAnimationController wing in wingAnimators)
        {
            wing.ChangeBlend("wingSpread", wingSpread, 0.2f);
            wing.SetBool("isShielding", isShield);
            wing.SetBool("isFlapping", isFlap);
        }
    }

    void Wing_Attack()
    {
        bool isShield = true;
        float wingSpread = 1f;
        
        foreach (WingAnimationController wing in wingAnimators)
        {
            wing.ChangeBlend("wingSpread", wingSpread, 0.1f);
            wing.SetBool("isShielding", isShield);
        }
    }
}
