using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleBoss : Controller
{
    public float rotationSpeed = 1f;

    public SkillMain[] bossSkills = new SkillMain[4];

    private void AttachSkills()
    {
        bossSkills[0] = (UtillHelper.AddSkill<AngleImpact>(transform, "AngleImpact"));
        bossSkills[0].coolTime = 45f;
        bossSkills[1] = (UtillHelper.AddSkill<AngleImpact>(transform, "AngleImpact"));
        bossSkills[1].coolTime = 70f;
        bossSkills[2] = (UtillHelper.AddSkill<AngleImpact>(transform, "AngleImpact"));
        bossSkills[3] = (UtillHelper.AddSkill<AngleImpact>(transform, "AngleImpact"));
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

        // 타겟 방향 계산
        Vector3 direction = curTarget.transform.position - transform.position;
        direction.y = 0f; // 수평 방향으로만 회전하도록 y 축 값은 0으로 설정

        if (direction.magnitude > 0f)
        {
            // 타겟 방향으로 회전
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    public override void Attack()
    {
        //bossSkills에 있는 스킬 사용, 쿨타임내 재사용 불가
        //
        //FollowTarget

    }

}
