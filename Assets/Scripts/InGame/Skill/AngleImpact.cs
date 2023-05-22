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
        //마우스 방향으로 회전 실행, 회전이 끝날때까지 대기
        if (mouseRotate)
        {
            Vector3 direction = UtillHelper.GetMouseWorldPosition(attacker.transform.position);
            yield return StartCoroutine(UtillHelper.RotateTowards(attacker.transform, direction, attacker.rotateTime, () => { }));
            //공격모션 시행
            TriggerAnimation(attacker);
        }

        //원점으로부터 전방으로 투사체 발사
        EffectManager.Instance.PlayEffect("Effect1", transform, Vector3.forward + (Vector3.up * 1.5f));
        AudioManager.Instance.Play3DSound("Hand1", transform.position, 1);

        yield return StartCoroutine(IAfterDelay(() => { }));
        SkillEnd(attacker);
    }
}
