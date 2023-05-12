using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationListener : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem slash;
    [SerializeField]
    private ParticleSystem dash;
    void Attack_Basic()
    {
        AudioManager.Instance.Play2DSound("20119_Atk_Juro_ShotA_01", 1f);
        slash.Play();
    }

    public void Attack_Dash()
    {
        AudioManager.Instance.Play2DSound("volgue_dash_02", 0.7f);
        dash.Play();
    }

    public void Stop_Dash()
    {
        dash.Stop();
    }

}
