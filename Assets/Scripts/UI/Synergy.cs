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

    private void CallSkillTree()
    {

    }

    private void GetSkillData()
    {
        // skillInfo에 정보를 불러오는 함수
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
        GetSkillData();
        SetElements();
    }

    // Start is called before the first frame update
    void Start()
    {
        //Init();
    }
}
