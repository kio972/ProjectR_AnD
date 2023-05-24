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
    //public float baseDamage = 0;
    public AttackType attackType;
    private int curStack = 1;
    public int Curstack { get { return curStack; } }
    public int maxStack = 1;
    private float coolTimeElapsed = 0f;
    public float coolTime = 0f;
    [SerializeField]
    protected CCType ccType = CCType.Stiff;
    protected float ccTime = 0.8f;

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

    public void SetStartCoolTime(float coolTime)
    {
        this.coolTimeElapsed = this.coolTime - coolTime;
        curStack = 0;
    }

    public bool CoolTimeUpdate()
    {
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
    

    public virtual void SkillCheck(Controller attacker, bool coolCheck = true, bool rotateToMouse = true)
    {
        if(coolCheck)
        {
            if (curStack >= 1)
            {
                curStack--;
                SkillManager.Instance.ActivateSkill(CoolTimeUpdate);
                PrepareSkill(attacker);
                if (coroutine != null)
                    StopCoroutine(coroutine);
                coroutine = StartCoroutine(ISkillFunc(attacker, rotateToMouse));
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
        if (attacker.isDead || (object)attacker.CurState == FSMCC.Instance)
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
        if(coroutine != null)
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
        //스킬테이블에서 스킬정보 받아오는 함수 추가예정

    }
}
