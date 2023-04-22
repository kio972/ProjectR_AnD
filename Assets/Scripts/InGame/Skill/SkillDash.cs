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
        Vector3 startPos = transform.position;
        Vector3 dir = (attacker.transform.forward).normalized * dashDist;
        Vector3 endPos = transform.position + dir;
        float elapsedTime = 0f;
        while (elapsedTime < lerpTime)
        {
            attacker.agent.Move(dir.normalized * attacker.agent.speed * Time.deltaTime);
            elapsedTime += Time.deltaTime;

            // 이동 중 충돌하는 물체에서 Controller 컴포넌트를 받아옴
            Collider[] colliders = Physics.OverlapSphere(transform.position, (attacker.agent.radius) + 0.1f);
            foreach (Collider c in colliders)
            {
                if (c.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                {
                    Controller controller = c.GetComponent<Controller>();
                    if (controller != null)
                    {
                        ExecuteDamage(attacker, controller);
                    }
                }
            }

            yield return null;
        }

        //while (currentTime < lerpTime)
        //{
        //    Vector3 currentPosition = Vector3.Lerp(startPosition, endPosition, currentTime / lerpTime);
        //    RaycastHit hit;
        //    if (Physics.Linecast(attacker.transform.position, currentPosition, out hit))
        //    {
        //        if (hit.collider.gameObject.layer == 1 << attacker.enemyLayer)
        //        {
        //            Controller controller = hit.collider.GetComponentInParent<Controller>();
        //            ExecuteDamage(attacker, controller);
        //        }
        //        break;
        //    }
        //    attacker.transform.position = currentPosition;
        //    currentTime += interval;
        //    yield return null;
        //}

        yield return StartCoroutine(IAfterDelay(() => { }));
        SkillEnd(attacker);
    }
}
