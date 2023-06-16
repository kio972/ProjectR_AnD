using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public struct SkillInfo
{
    public int skillID;
    public Sprite skillImage;
    public string skillName;
    public string skillText;
    public bool stackable;
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

    private int skillIndex = -1;

    private void GetSynergy()
    {
        if (haveSkill)
            return;

        SynergyView synergyView = FindObjectOfType<SynergyView>();
        synergyView.GetSynergy(skillInfo);
    }

    private void GetSkillData(int skillId)
    {
        // skillInfo에 정보를 불러오는 함수
        skillInfo = DataManager.Instance.FindSkillInfo(skillId);

        skillIcon.sprite = skillInfo.skillImage;
        skillName.text = skillInfo.skillName;
        skillText.text = skillInfo.skillText;
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
            synergyBtn.onClick.AddListener(GetSynergy);
        if (skillIcon != null && skillInfo.skillImage != null)
            skillIcon.sprite = skillInfo.skillImage;
        if (skillName != null)
            skillName.text = skillInfo.skillName;
        if (skillText != null)
            skillText.text = skillInfo.skillText;
    }

    public void Init(int index)
    {
        GetElements();
        GetSkillData(index);
        SetElements();
        skillIndex = index;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Init();
    }
}
