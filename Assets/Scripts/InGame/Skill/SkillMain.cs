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
    
    public void PrepareSkill(Controller attacker)
    {
        attacker.IsAttacking = true;
        attacker.agent.updateRotation = false;
        attacker.agent.isStopped = true;
    }

    public virtual void SkillCheck(Controller attacker)
    {
        PrepareSkill(attacker);
        StartCoroutine(ISkillFunc(attacker));
    }



    public virtual void TriggerAnimation(Controller attacker) { }

    protected void ExecuteDamage(Controller attacker, Controller victim)
    {
        float damage = attacker.baseDamage;
        damage = attacker.DamageModify(damage);
        damage = victim.TakeDamage(damage);
        attacker.DrainHp(damage);
    }

    protected void SkillEnd(Controller attacker)
    {
        attacker.IsAttacking = false;
        attacker.agent.updateRotation = true;
        attacker.agent.isStopped = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
