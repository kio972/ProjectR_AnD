using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackType
{
    Point,
    Directional,
    Projectile,

}

public abstract class SkillMain : MonoBehaviour
{
    public float baseDamage = 0;
    public AttackType attackType;
    public abstract void SkillCheck(Controller attacker, Controller victim);

    // Update is called once per frame
    void Update()
    {
        
    }
}
