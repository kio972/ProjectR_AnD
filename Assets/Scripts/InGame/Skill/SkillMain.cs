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
    public int stack = 1;
    public float coolTime = 0;

    protected float after_Delay = 0.3f;
    
    public abstract IEnumerator ISkillFunc(Controller attacker, bool mouseRotate = false);
    public virtual void TriggerAnimation(Controller attacker) { }

    protected IEnumerator IAfterDelay(System.Action callback = null)
    {
        float elapsedTime = 0f;
        while(elapsedTime < after_Delay)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        callback?.Invoke();
    }

    
    public void PrepareSkill(Controller attacker)
    {
        attacker.IsAttacking = true;
        attacker.agent.updateRotation = false;
        attacker.agent.isStopped = true;
    }

    public virtual void SkillCheck(Controller attacker)
    {
        PrepareSkill(attacker);
        StartCoroutine(ISkillFunc(attacker, true));
    }

    protected void ExecuteDamage(Controller attacker, Controller victim)
    {
        if (attacker == null || victim == null)
            return;

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
