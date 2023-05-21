using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleBoss : Controller
{
    public float rotationSpeed = 1f;

    public List<SkillMain> bossSkills = new List<SkillMain>();

    public override void FollowTarget()
    {
        if (curTarget == null)
            return;

        // Ÿ�� ���� ���
        Vector3 direction = curTarget.transform.position - transform.position;
        direction.y = 0f; // ���� �������θ� ȸ���ϵ��� y �� ���� 0���� ����

        if (direction.magnitude > 0f)
        {
            // Ÿ�� �������� ȸ��
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    public override void Attack()
    {
        //bossSkills�� �ִ� ��ų ���,

    }

}
