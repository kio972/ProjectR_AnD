using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate bool SkillUpdater();

public class SkillManager : Singleton<SkillManager>
{
    public SkillMain basicSkill;
    public SkillMain dashSkill;
    public SkillMain specialSkill;

    public SkillMain curSkill;

    public List<SkillUpdater> activatedSkills = new List<SkillUpdater>();

    private void Update()
    {
        List<SkillUpdater> needRemoveSkills = new List<SkillUpdater>();
        foreach(SkillUpdater skill in activatedSkills)
        {
            //스킬 업데이트 수행
            bool needRemove = skill();
            //스킬 쿨타임 완료시 제거리스트에 추가
            if (needRemove)
                needRemoveSkills.Add(skill);
        }
        //업데이트 제거 수행
        foreach(SkillUpdater skill in needRemoveSkills)
        {
            activatedSkills.Remove(skill);
        }
    }

    public void ActivateSkill(SkillUpdater updater)
    {
        activatedSkills.Add(updater);
    }

    public void UseBasicSkill(Controller attacker)
    {
        basicSkill.SkillCheck(attacker);
        curSkill = basicSkill;
    }

    public void UseDashSkill(Controller attacker)
    {
        dashSkill.SkillCheck(attacker);
        curSkill = dashSkill;
    }

    public void UseSpecialSkill(Controller attacker)
    {
        specialSkill.SkillCheck(attacker);
        curSkill = specialSkill;
    }


    public void Init()
    {
        // 생성자에서 필요한 초기화 작업을 수행합니다.
        GameObject basicSkillObject = new GameObject();
        basicSkillObject.name = "BasicSkill";
        basicSkillObject.transform.SetParent(transform);
        basicSkill = basicSkillObject.AddComponent<SkillBasic>();
        GameObject dashSkillObject = new GameObject();
        dashSkillObject.name = "DashSkill";
        dashSkillObject.transform.SetParent(transform);
        dashSkill = dashSkillObject.gameObject.AddComponent<SkillDash>();
    }
}
