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
        //���콺 �������� ȸ�� ����, ȸ���� ���������� ���
        Vector3 direction = UtillHelper.GetMouseWorldPosition(attacker.transform.position);
        print(direction);
        yield return StartCoroutine(UtillHelper.RotateTowards(attacker.transform, direction, attacker.rotateTime, () => {}));
        //���ݸ�� ����
        TriggerAnimation(attacker);
        yield return new WaitForSeconds(0.4f);
        //attacker ���� 10�������� 2f�Ÿ� ���� Controller�� ���� ������ �޾ƿ�
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
                    // �ش� �� ���� ó��
                    ExecuteDamage(attacker, controller);
                }
            }
        }

        yield return StartCoroutine(IAfterDelay(() => { }));
        SkillEnd(attacker);
    }
}
