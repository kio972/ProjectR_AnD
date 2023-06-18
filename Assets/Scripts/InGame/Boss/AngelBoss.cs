using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngelBoss : Controller
{
    private SkillMain[] bossSkills;

    private SkillMain rageSkill;

    public ParticleSystem auraEffect;

    void BloodEffect()
    {
        float randomRange = 2f;
        float x = Random.Range(-randomRange, randomRange);
        float y = Random.Range(-randomRange, randomRange);
        float z = Random.Range(-randomRange, randomRange);
        Vector3 randomModify = new Vector3(x, y, z);
        EffectManager.Instance.PlayEffect("Bloodaura", transform, Vector3.up * 3f + randomModify, 5f);
    }

    public override void Dead()
    {
        animator.SetBool("Dead", true);
        isDead = true;
        agent.enabled = false;
        Collider collider = GetComponentInChildren<Collider>();
        if (collider != null)
            collider.enabled = false;

        foreach(SkillMain skill in bossSkills)
        {
            skill.StopSkill(this);
        }
        auraEffect.Stop();

        Invoke("BloodEffect", 0.1f);
        Invoke("BloodEffect", 0.3f);
        Invoke("BloodEffect", 0.5f);

        GameManager.Instance.PlayCutScene(true);

        Destroy(curTarget);
    }

    public override void GetCC(CCType ccType, float ccDuration)
    {
        if (ccType == CCType.Stiff)
            return;

        curCCState = ccType;
        this.ccDuration = ccDuration;
        ccElapsed = 0f;

        if ((object)CurState != FSMCC.Instance)
            ChangeState(FSMCC.Instance);
    }

    private void AttachSkills()
    {
        bossSkills = new SkillMain[3];
        bossSkills[0] = (UtillHelper.AddSkill<AngelImpact>(transform, "AngleImpact"));
        bossSkills[0].coolTime = 20f;
        bossSkills[0].SetStartCoolTime(20f);
        bossSkills[1] = (UtillHelper.AddSkill<SpaceSeparation>(transform, "SpaceSeparation"));
        bossSkills[1].coolTime = 30f;
        bossSkills[1].SetStartCoolTime(30f);
        bossSkills[2] = (UtillHelper.AddSkill<RegionControl>(transform, "RegionControl"));
        bossSkills[2].coolTime = 2f;
        bossSkills[2].SetStartCoolTime(2f);

        SkillManager.Instance.ActivateSkill(bossSkills[0].CoolTimeUpdate);
        SkillManager.Instance.ActivateSkill(bossSkills[1].CoolTimeUpdate);
        SkillManager.Instance.ActivateSkill(bossSkills[2].CoolTimeUpdate);
    }

    public override void Init(Dictionary<string, object> data = null)
    {
        base.Init(data);
        AttachSkills();
    }

    private bool RageSkillCheck()
    {
        if (rageSkill != null && rageSkill.Curstack >= 1)
        {
            rageSkill.SkillCheck(this, true, false);
            return true;
        }
        return false;
    }

    private bool BasicSkillCheck()
    {
        foreach (SkillMain skill in bossSkills)
        {
            if (skill.Curstack >= 1)
            {
                skill.SkillCheck(this, true, false);
                return true;
            }
        }
        return false;
    }

    private void RageConditionCheck()
    {
        if (hp / maxHp <= 0.3f && rageSkill == null)
        {
            rageSkill = UtillHelper.AddSkill<RegionControlImplace>(transform, "RegionControlImplace");
            rageSkill.coolTime = 19f;
        }
    }

    public override void Attack()
    {
        //bossSkills에 있는 스킬 사용, 쿨타임내 재사용 불가
        if (IsAttacking || curTarget == null)
            return;

        RageConditionCheck();

        if (RageSkillCheck())
            return;

        if (BasicSkillCheck())
            return;
    }
}
