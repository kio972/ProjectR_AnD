using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public struct SkillInfo
{
    public int skillID;
    public Image skillImage;
    public string skillName;
    public string skillText;
}

public class Synergy : MonoBehaviour
{
    private Button synergyBtn;
    private SkillInfo skillInfo;
    private Image skillIcon;
    private TextMeshProUGUI skillName;
    private TextMeshProUGUI skillText;
    public SkillInfo SkillInfo { get { return skillInfo; } }
    public bool haveSkill = false;

    private void CallSkillTree()
    {

    }

    private void GetSkillData(int skillID)
    {
        // skillInfo에 정보를 불러오는 함수
        int skillIndex = UtillHelper.Find_Data_Index(skillID, DataManager.Instance.Skill_Passive_Dic);
        //skillIcon.sprite = ;
        skillName.text = DataManager.Instance.Skill_Passive_Dic[skillIndex]["SkillName"].ToString();
        skillText.text = DataManager.Instance.Skill_Passive_Dic[skillIndex]["Description"].ToString();
    }

    private void GetElements()
    {
        if (synergyBtn == null)
            synergyBtn = GetComponent<Button>();
        if (skillIcon == null)
            skillIcon = UtillHelper.Find<Image>(transform, "SkillIcon");
        if (skillName == null)
            skillName = UtillHelper.Find<TextMeshProUGUI>(transform, "SkillDescription/SkillName");
        if (skillText == null)
            skillText = UtillHelper.Find<TextMeshProUGUI>(transform, "SkillDescription/SkillText");
    }

    private void SetElements()
    {
        if (synergyBtn != null)
            synergyBtn.onClick.AddListener(CallSkillTree);
        if (skillIcon != null)
            skillIcon = skillInfo.skillImage;
        if (skillName != null)
            skillName.text = skillInfo.skillName;
        if (skillText != null)
            skillText.text = skillInfo.skillText;
    }

    public void Init()
    {
        GetElements();
        GetSkillData(0);
        SetElements();
    }

    // Start is called before the first frame update
    void Start()
    {
        //Init();
    }
}
