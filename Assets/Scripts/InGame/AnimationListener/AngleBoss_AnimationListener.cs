using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AngleBoss_AnimationListener : MonoBehaviour
{
    [SerializeField]
    private Animator bossAnimator;
    [SerializeField]
    private List<WingAnimationController> wingAnimators;
    [SerializeField]
    private ParticleSystem escapePortal;

    [SerializeField]
    private string nextSceneName = "Stage2Scene";

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
            //wing.ChangeBlend("wingSpread", wingSpread, 0.1f);
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
            //wing.ChangeBlend("wingSpread", wingSpread, 0.2f);
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
            //wing.ChangeBlend("wingSpread", wingSpread, 0.1f);
            wing.SetBool("isShielding", isShield);
        }
    }

    void Wing_Stun()
    {
        bool isShield = true;
        float wingSpread = 1f;
        foreach (WingAnimationController wing in wingAnimators)
        {
            //wing.ChangeBlend("wingSpread", wingSpread, 0.1f);
            wing.SetBool("isShielding", isShield);
        }
        Controller controller = GetComponent<Controller>();
        float invokeTime = controller.CCDuration - 0.3f;
        Invoke("SetWingFlap", invokeTime);
        Invoke("Wing_Default", invokeTime);
    }

    void BossBattleEnd()
    {
        SceneController.Instance.MoveScene(nextSceneName, 1f);
    }
}
