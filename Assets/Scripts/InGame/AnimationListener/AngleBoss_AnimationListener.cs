using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleBoss_AnimationListener : MonoBehaviour
{
    [SerializeField]
    private Animator bossAnimator;
    [SerializeField]
    private List<WingAnimationController> wingAnimators;
    [SerializeField]
    private ParticleSystem escapePortal;

    public void SetWingIdle()
    {
        foreach (WingAnimationController wing in wingAnimators)
        {
            wing.SetBool("isFlapping", false);
        }
    }

    public void SetWingFlap()
    {
        foreach (WingAnimationController wing in wingAnimators)
        {
            wing.SetBool("isFlapping", true);
        }
    }

    void Wing_Default()
    {
        bool isShield = false;
        float wingSpread = -1f;
        foreach (WingAnimationController wing in wingAnimators)
        {
            wing.ChangeBlend("wingSpread", wingSpread, 0.1f);
            wing.SetBool("isShielding", isShield);
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

    void Wing_Stun()
    {
        bool isShield = true;
        float wingSpread = 1f;

        foreach (WingAnimationController wing in wingAnimators)
        {
            wing.ChangeBlend("wingSpread", wingSpread, 0.1f);
            wing.SetBool("isShielding", isShield);
        }

        Invoke("SetWingFlap", 1f);
        Invoke("Wing_Default", 1f);
    }
}
