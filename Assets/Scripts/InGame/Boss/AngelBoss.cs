using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngelBoss : Controller
{
    private SkillMain[] bossSkills;

    private SkillMain rageSkill;

    public ParticleSystem auraEffect;

    private void AttachSkills()
    {
        bossSkills = new SkillMain[3];
        bossSkills[0] = (UtillHelper.AddSkill<AngelImpact>(transform, "AngleImpact"));
        bossSkills[0].SetStartCoolTime(45f);
        bossSkills[0].coolTime = 30f;
        bossSkills[1] = (UtillHelper.AddSkill<SpaceSeparation>(transform, "SpaceSeparation"));
        bossSkills[1].SetStartCoolTime(70f);
        bossSkills[1].coolTime = 90f;
        bossSkills[2] = (UtillHelper.AddSkill<RegionControl>(transform, "RegionControl"));
        bossSkills[2].SetStartCoolTime(1f);
        bossSkills[2].coolTime = 2f;

        SkillManager.Instance.ActivateSkill(bossSkills[0].CoolTimeUpdate);
        SkillManager.Instance.ActivateSkill(bossSkills[1].CoolTimeUpdate);
        SkillManager.Instance.ActivateSkill(bossSkills[2].CoolTimeUpdate);
    }

    public override void Init(Dictionary<string, object> data = null)
    {
        base.Init(data);
        AttachSkills();
    }

    public override void Attack()
    {
        //bossSkills에 있는 스킬 사용, 쿨타임내 재사용 불가
        if (IsAttacking || curTarget == null)
            return;

        foreach(SkillMain skill in bossSkills)
        {
            if(skill.Curstack >= 1)
            {
                skill.SkillCheck(this, true, false);
                return;
            }
        }

        if (hp / maxHp <= 0.3f && rageSkill == null)
        {
            rageSkill = UtillHelper.AddSkill<RegionControlImplace>(transform, "RegionControlImplace");
            rageSkill.SkillCheck(this);
        }

    }
}
