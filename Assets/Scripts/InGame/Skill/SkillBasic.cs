using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBasic : SkillMain
{
    public override void TriggerAnimation(Controller attacker)
    {
        attacker.animator.SetTrigger("Attack");
    }

    public override IEnumerator ISkillFunc(Controller attacker)
    {
        //마우스 방향으로 회전 실행, 회전이 끝날때까지 대기
        Vector3 direction = UtillHelper.GetMouseWorldPosition(attacker.transform.position);
        print(direction);
        yield return StartCoroutine(UtillHelper.RotateTowards(attacker.transform, direction, attacker.rotateTime, () => {}));
        //공격모션 시행
        TriggerAnimation(attacker);
        yield return new WaitForSeconds(0.4f);
        //attacker 전방 10도범위의 2f거리 내의 Controller를 가진 적들을 받아옴
        Collider[] hitColliders = Physics.OverlapSphere(attacker.transform.position, 2.5f);
        foreach (Collider hitCollider in hitColliders)
        {
            Controller controller = hitCollider.GetComponentInParent<Controller>();
            if (controller != null && controller.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                print(controller.gameObject.name);
                Vector3 dirToTarget = (controller.transform.position - attacker.transform.position).normalized;
                if (Vector3.Angle(attacker.transform.forward, dirToTarget) < 60f)
                {
                    // 해당 적 공격 처리
                    ExecuteDamage(attacker, controller);
                }
            }
        }

        yield return StartCoroutine(IAfterDelay(() => { }));
        SkillEnd(attacker);
    }
}
