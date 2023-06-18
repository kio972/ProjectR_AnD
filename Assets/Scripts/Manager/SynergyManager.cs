using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SynergyManager : Singleton<SynergyManager>
{
    [SerializeField]
    private Transform synergyZone;

    private List<SkillInfo> curSkills = new List<SkillInfo>();
    private List<int> synergySkillIds = new List<int>();

    public Transform treeZone;
    [SerializeField]
    private List<SynergySkillSlot> skillSlot;

    private void AddSkillTree(int skillId)
    {
        foreach(SynergySkillSlot slot in skillSlot)
        {
            if(slot.skillId == -1)
            {
                slot.Init(skillId);
                return;
            }
        }
    }

    private void SynergySkillCheck(SkillInfo skillInfo)
    {
        foreach(int synergySkill in skillInfo.topSkill)
        {
            if (synergySkillIds.Contains(synergySkill))
                continue;

            //해당하는 시너지의 스킬트리를 추가하도록 변경
            AddSkillTree(synergySkill);
            synergySkillIds.Add(synergySkill);
        }
    }

    public void GetSynergy(SkillInfo skillInfo)
    {
        Synergy newSynergy = Resources.Load<Synergy>("Prefab/UI/SynergyEmpty2");
        newSynergy = Instantiate(newSynergy, synergyZone);
        newSynergy.transform.position = synergyZone.transform.position;

        newSynergy.Init(skillInfo.skillID);
        curSkills.Add(skillInfo);
        SynergySkillCheck(skillInfo);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
