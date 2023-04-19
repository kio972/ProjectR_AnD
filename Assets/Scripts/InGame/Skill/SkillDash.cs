using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDash : SkillMain
{
    public override IEnumerator ISkillFunc(Controller attacker)
    {
        //���콺 �������� ȸ�� ����, ȸ���� ���������� ���
        Vector3 direction = UtillHelper.GetMouseWorldPosition(attacker.transform.position);
        yield return StartCoroutine(UtillHelper.RotateTowards(attacker.transform, direction, attacker.rotateTime, () => { }));
        //���ݸ�� ����
        TriggerAnimation(attacker);

        //�������� x���� �̵�, �̵� �� �÷��̾��� collider�� �ٸ� collider�� �ε��� �� ����
        //�ε��� collider�� "Enemy" ���̾��� ���, �ش� collider���� Controller�� ������ ExcuteDamage(attacker, controller); ó��
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
