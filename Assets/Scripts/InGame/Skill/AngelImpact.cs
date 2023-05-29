using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngelImpact : SkillMain
{
    public override void TriggerAnimation(Controller attacker)
    {
        attacker.animator.SetTrigger("Attack");
    }

    public override IEnumerator ISkillFunc(Controller attacker, bool mouseRotate = false)
    {
        AngelBoss boss = attacker.GetComponent<AngelBoss>();
        if (boss != null)
            boss.auraEffect.Play();
        //attacker.curTarget방향으로 공격 3회, 공격간 텀은 x초
        float attackDelay = 1f;
        for(int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(attackDelay);
            EffectManager.Instance.PlayEffect("Projectile_19", attacker.transform, (Vector3.up * 1.5f), 8);
            //AudioManager.Instance.Play3DSound("Hand1", transform.position, 1);
        }

        after_Delay = 1f;
        yield return StartCoroutine(IAfterDelay(() => { }));
        SkillEnd(attacker);
        if (boss != null)
            boss.auraEffect.Stop();
    }
}
