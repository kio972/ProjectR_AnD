using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : Singleton<SkillManager>
{
    public SkillMain basicSkill;
    public SkillMain dashSkill;
    public SkillMain specialSkill;

    public void UseBasicSkill(Controller attacker)
    {
        basicSkill.SkillCheck(attacker);
    }

    public void UseDashSkill(Controller attacker)
    {
        dashSkill.SkillCheck(attacker);
    }

    public void UseSpecialSkill(Controller attacker)
    {
        specialSkill.SkillCheck(attacker);
    }


    public void Init()
    {
        // �����ڿ��� �ʿ��� �ʱ�ȭ �۾��� �����մϴ�.
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
