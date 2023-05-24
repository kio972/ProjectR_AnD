using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleImpact : SkillMain
{
    public override void TriggerAnimation(Controller attacker)
    {
        attacker.animator.SetTrigger("Attack");
    }

    public override IEnumerator ISkillFunc(Controller attacker, bool mouseRotate = false)
    {
        //attacker.curTarget방향으로 공격 3회, 공격간 텀은 x초
        float attackDelay = 1f;
        for(int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(attackDelay);
            EffectManager.Instance.PlayEffect("Projectile_19", transform, Vector3.forward + (Vector3.up * 1.5f));
            //AudioManager.Instance.Play3DSound("Hand1", transform.position, 1);
        }

        yield return StartCoroutine(IAfterDelay(() => { }));
        SkillEnd(attacker);
    }
}
