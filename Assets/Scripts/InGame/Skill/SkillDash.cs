using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkillDash : SkillMain
{
    float dashDist = 5f;
    public override void TriggerAnimation(Controller attacker)
    {
        attacker.animator.SetTrigger("Dash");
    }

    public override IEnumerator ISkillFunc(Controller attacker)
    {
        //마우스 방향으로 회전 실행, 회전이 끝날때까지 대기
        Vector3 direction = UtillHelper.GetMouseWorldPosition(attacker.transform.position);
        yield return StartCoroutine(UtillHelper.RotateTowards(attacker.transform, direction, attacker.rotateTime, () => { }));
        //공격모션 시행
        TriggerAnimation(attacker);

        //전방으로 x미터 이동, 이동 중 플레이어의 collider가 다른 collider에 부딪힐 시 정지
        //부딪힌 collider가 "Enemy" 레이어일 경우, 해당 collider에서 Controller를 가져와 데미지 처리
        //float moveDistance = 3f;
        //float interval = 0.01f;
        float lerpTime = 0.5f;
        Vector3 startPos = attacker.transform.position;
        Vector3 dir = (attacker.transform.forward).normalized * dashDist;
        Vector3 endPos = attacker.transform.position + dir;
        float distance = Vector3.Distance(startPos, endPos);
        float speed = distance / lerpTime;
        float elapsedTime = 0f;
        while (elapsedTime < lerpTime)
        {
            attacker.agent.Move(dir.normalized * speed * Time.deltaTime);
            elapsedTime += Time.deltaTime;

            bool isStop = false;
            // 이동 중 충돌하는 물체에서 Controller 컴포넌트를 받아옴
            Collider[] colliders = Physics.OverlapSphere(attacker.transform.position, (attacker.agent.radius) + 0.2f);
            foreach (Collider c in colliders)
            {
                Controller controller = c.GetComponentInParent<Controller>();
                if (controller == null || controller.gameObject.layer == attacker.gameObject.layer)
                    continue;

                if (controller != null && controller.gameObject.layer == 1 << attacker.enemyLayer)
                {
                    ExecuteDamage(attacker, controller);
                    isStop = true;
                }
            }
            if (isStop)
                break;
            yield return null;
        }

        yield return StartCoroutine(IAfterDelay(() => { }));
        SkillEnd(attacker);
    }
}
