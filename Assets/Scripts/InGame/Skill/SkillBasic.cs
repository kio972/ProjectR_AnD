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
        yield return StartCoroutine(UtillHelper.RotateTowards(attacker.transform, direction, attacker.rotateTime, () => {}));
        //���ݸ�� ����
        TriggerAnimation(attacker);
        //attacker ���� 10�������� 1f�Ÿ� ���� Controller�� ���� ������ �޾ƿ�
        Collider[] hitColliders = Physics.OverlapSphere(attacker.transform.position, 1f);
        foreach (Collider hitCollider in hitColliders)
        {
            Controller controller = hitCollider.GetComponent<Controller>();
            if (controller != null && controller.gameObject.layer != LayerMask.NameToLayer("Player"))
            {
                Vector3 dirToTarget = (controller.transform.position - attacker.transform.position).normalized;
                if (Vector3.Angle(attacker.transform.forward, dirToTarget) < 10f)
                {
                    // �ش� �� ���� ó��
                    ExcuteDamage(attacker, controller);
                }
            }
        }
    }

    //public override void SkillCheck(Controller attacker)
    //{

    //}
}
