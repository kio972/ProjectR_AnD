using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SynergyView : MonoBehaviour
{
    private List<SynergyIcon> icons = new List<SynergyIcon>();

    public void GetSynergy(SkillInfo skillInfo)
    {
        foreach(SynergyIcon icon in icons)
        {
            if(icon.skillId == skillInfo.skillID)
            {
                icon.stack++;
                icon.UpdateText();
                return;
            }
        }

        SynergyIcon newIcon = Resources.Load<SynergyIcon>("Prefab/UI/SynergyEmpty");
        newIcon = Instantiate(newIcon, transform);
        newIcon.transform.position = transform.position;

        newIcon.Init(skillInfo);
        icons.Add(newIcon);
    }
}
