using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBasic : SkillMain
{
    public override void TriggerAnimation(Controller attacker)
    {
        attacker.animator.SetTrigger("Attack");
    }

    public override IEnumerator ISkillFunc(Controller attacker, bool mouseRotate = false)
    {
        //���콺 �������� ȸ�� ����, ȸ���� ���������� ���
        if(mouseRotate)
        {
            Vector3 direction = UtillHelper.GetMouseWorldPosition(attacker.transform.position);
            yield return StartCoroutine(UtillHelper.RotateTowards(attacker.transform, direction, attacker.rotateTime, () => { }));
            //���ݸ�� ����
            TriggerAnimation(attacker);
        }
        yield return new WaitForSeconds(0.4f);
        //attacker ���� x�������� x�Ÿ� ���� Controller�� ���� ������ �޾ƿ�
        Collider[] hitColliders = Physics.OverlapSphere(attacker.transform.position, attacker.attackRange);
        foreach (Collider hitCollider in hitColliders)
        {
            Controller controller = hitCollider.GetComponentInParent<Controller>();
            if (controller != null && 1 << controller.gameObject.layer == attacker.enemyLayer)
            {
                Vector3 dirToTarget = (controller.transform.position - attacker.transform.position).normalized;
                if (Vector3.Angle(attacker.transform.forward, dirToTarget) < 60f)
                {
                    // �ش� �� ���� ó��
                    ExecuteDamage(attacker, controller);
                    AudioManager.Instance.Play2DSound("attack_blunt07");
                }
            }
        }

        yield return StartCoroutine(IAfterDelay(() => { }));
        SkillEnd(attacker);
    }
}
