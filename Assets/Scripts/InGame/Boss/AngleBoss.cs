using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleBoss : Controller
{
    public float rotationSpeed = 1f;

    public SkillMain[] bossSkills;

    private void AttachSkills()
    {
        bossSkills = new SkillMain[4];
        bossSkills[0] = (UtillHelper.AddSkill<AngleImpact>(transform, "AngleImpact"));
        bossSkills[0].SetStartCoolTime(45f);
        bossSkills[1] = (UtillHelper.AddSkill<SpaceSeparation>(transform, "SpaceSeparation"));
        bossSkills[1].SetStartCoolTime(70f);
        bossSkills[2] = (UtillHelper.AddSkill<RegionControl>(transform, "RegionControl"));
        bossSkills[2].SetStartCoolTime(1f);
        bossSkills[3] = null;

        SkillManager.Instance.ActivateSkill(bossSkills[0].CoolTimeUpdate);
        SkillManager.Instance.ActivateSkill(bossSkills[1].CoolTimeUpdate);
        SkillManager.Instance.ActivateSkill(bossSkills[2].CoolTimeUpdate);
    }

    public override void Init(Dictionary<string, object> data = null)
    {
        base.Init(data);
        AttachSkills();
    }

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
        //bossSkills�� �ִ� ��ų ���, ��Ÿ�ӳ� ���� �Ұ�
        //FollowTarget
        if (IsAttacking || curTarget == null)
            return;

        foreach(SkillMain skill in bossSkills)
            skill?.SkillCheck(this, true, false);

        if (hp / maxHp <= 0.3f && bossSkills[3] == null)
            bossSkills[3] = (UtillHelper.AddSkill<RegionControlImplace>(transform, "RegionControlImplace"));

        FollowTarget();
    }
}
