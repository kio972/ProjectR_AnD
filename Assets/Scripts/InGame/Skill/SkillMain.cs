using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackType
{
    Basic,
    Dash,
    Range,
    Projectile,
    Chase,
    Buff,
    Debuff,
}

public enum DebuffType
{
    Slow,
    Bleed,
}

public abstract class SkillMain : MonoBehaviour
{
    public float baseDamage = 0;
    public AttackType attackType;
    public abstract IEnumerator ISkillFunc(Controller attacker);
    //public abstract void SkillCheck(Controller attacker);

    public virtual void TriggerAnimation(Controller attacker) { }

    protected void ExcuteDamage(Controller attacker, Controller victim)
    {
        float damage = attacker.baseDamage;
        damage = attacker.DamageModify(damage);
        damage = victim.TakeDamage(damage);
        attacker.DrainHp(damage);
    }

    protected void SkillEnd(Controller attacker)
    {
        attacker.IsAttacking = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
