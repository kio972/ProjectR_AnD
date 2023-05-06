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
    private int curStack = 1;
    public int maxStack = 1;
    private float coolTimeElapsed = 0f;
    public float coolTime = 0f;

    protected CCType ccType = CCType.Stiff;
    protected float ccTime = 0.3f;

    protected float after_Delay = 0.3f;

    protected Coroutine coroutine = null;

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

    private bool CoolTimeUpdate()
    {
        print(coolTimeElapsed);
        coolTimeElapsed += Time.deltaTime;
        if(coolTimeElapsed >= coolTime)
        {
            curStack++;
            coolTimeElapsed = 0f;
            if(curStack >= maxStack)
            {
                curStack = maxStack;
                return true;
            }
        }
        return false;
    }

    
    public void PrepareSkill(Controller attacker)
    {
        attacker.IsAttacking = true;
        attacker.agent.updateRotation = false;
        attacker.agent.isStopped = true;
    }
    

    public virtual void SkillCheck(Controller attacker, bool isPlayer = true)
    {
        if(isPlayer)
        {
            if (curStack >= 1)
            {
                curStack--;
                SkillManager.Instance.ActivateSkill(CoolTimeUpdate);
                PrepareSkill(attacker);
                if (coroutine != null)
                    StopCoroutine(coroutine);
                coroutine = StartCoroutine(ISkillFunc(attacker, isPlayer));
            }
            else
            {
                //��ų ��Ÿ�� ����, ���� ǥ�� ����Լ� ����
            }
        }
        else
        {
            if (coroutine != null)
                StopCoroutine(coroutine);
            coroutine = StartCoroutine(ISkillFunc(attacker));
        }
    }

    protected void ExecuteDamage(Controller attacker, Controller victim)
    {
        if (attacker == null || victim == null)
            return;

        float damage = attacker.baseDamage;
        damage = attacker.DamageModify(damage);
        damage = victim.TakeDamage(damage);
        if (ccType != CCType.None)
            victim.GetCC(ccType, ccTime);
        attacker.DrainHp(damage);
    }

    public void StopSkill(Controller attacker)
    {
        StopCoroutine(coroutine);
        SkillEnd(attacker);
    }

    protected void SkillEnd(Controller attacker)
    {
        attacker.IsAttacking = false;
        if(attacker.agent.enabled)
        {
            attacker.agent.updateRotation = true;
            attacker.agent.isStopped = false;
        }
    }

    public virtual void Init()
    {
        //��ų���̺��� ��ų���� �޾ƿ��� �Լ� �߰�����

    }
}
