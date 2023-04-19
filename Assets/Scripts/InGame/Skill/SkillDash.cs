using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDash : SkillMain
{
    public override IEnumerator ISkillFunc(Controller attacker)
    {
        //마우스 방향으로 회전 실행, 회전이 끝날때까지 대기
        Vector3 direction = UtillHelper.GetMouseWorldPosition(attacker.transform.position);
        yield return StartCoroutine(UtillHelper.RotateTowards(attacker.transform, direction, attacker.rotateTime, () => { }));
        //공격모션 시행
        TriggerAnimation(attacker);

        //전방으로 x미터 이동, 이동 중 플레이어의 collider가 다른 collider에 부딪힐 시 정지
        //부딪힌 collider가 "Enemy" 레이어일 경우, 해당 collider에서 Controller를 가져와 ExcuteDamage(attacker, controller); 처리
        float moveDistance = 5f;
        float interval = 0.1f;
        Vector3 startPosition = attacker.transform.position;
        Vector3 endPosition = attacker.transform.position + attacker.transform.forward * moveDistance;
        float currentDistance = 0f;
        while (currentDistance < moveDistance)
        {
            Vector3 currentPosition = Vector3.Lerp(startPosition, endPosition, currentDistance / moveDistance);
            RaycastHit hit;
            if (Physics.Linecast(attacker.transform.position, currentPosition, out hit))
            {
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                {
                    Controller controller = hit.collider.GetComponent<Controller>();
                    ExecuteDamage(attacker, controller);
                }
                break;
            }
            attacker.transform.position = currentPosition;
            currentDistance += interval;
            yield return new WaitForSeconds(interval);
        }

        SkillEnd(attacker);
    }
}
