using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationListener : MonoBehaviour
{
    void Attack_Swing()
    {
        AudioManager.Instance.Play2DSound("attack_blunt05", 0.8f);
    }
}
