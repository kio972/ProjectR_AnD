using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SynergyIcon : MonoBehaviour
{
    public int skillId;
    public Image iconImg;
    public TextMeshProUGUI text;
    public int stack = 1;

    public void UpdateText()
    {
        if (stack > 1)
        {
            text.gameObject.SetActive(true);
            text.text = stack.ToString();
        }
    }

    public void Init(SkillInfo skillInfo)
    {
        skillId = skillInfo.skillID;
        iconImg.sprite = skillInfo.skillImage;
    }
}
