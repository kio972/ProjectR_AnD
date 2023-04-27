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
            //��ų ������Ʈ ����
            bool needRemove = skill();
            //��ų ��Ÿ�� �Ϸ�� ���Ÿ���Ʈ�� �߰�
            if (needRemove)
                needRemoveSkills.Add(skill);
        }
        //������Ʈ ���� ����
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
