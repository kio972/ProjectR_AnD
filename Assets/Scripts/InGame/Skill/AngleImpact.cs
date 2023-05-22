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
        //���콺 �������� ȸ�� ����, ȸ���� ���������� ���
        if (mouseRotate)
        {
            Vector3 direction = UtillHelper.GetMouseWorldPosition(attacker.transform.position);
            yield return StartCoroutine(UtillHelper.RotateTowards(attacker.transform, direction, attacker.rotateTime, () => { }));
            //���ݸ�� ����
            TriggerAnimation(attacker);
        }

        //�������κ��� �������� ����ü �߻�
        EffectManager.Instance.PlayEffect("Effect1", transform, Vector3.forward + (Vector3.up * 1.5f));
        AudioManager.Instance.Play3DSound("Hand1", transform.position, 1);

        yield return StartCoroutine(IAfterDelay(() => { }));
        SkillEnd(attacker);
    }
}
