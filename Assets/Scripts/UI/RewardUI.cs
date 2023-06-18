using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardUI : MonoBehaviour
{
    [SerializeField]
    private DescToggle rewardDesc;
    [SerializeField]
    private RectTransform interactText;

    public float yOffset = 50f;
    [SerializeField]
    private List<Synergy> synergySlot = new List<Synergy>();

    private void SetReward()
    {
        List<int> indexList = new List<int>();
        foreach(Synergy synergy in synergySlot)
        {
            int randomIndex = Random.Range(0, DataManager.Instance.Skill_Passive_Dic.Count);
            while(true)
            {
                if (!indexList.Contains(randomIndex))
                {
                    indexList.Add(randomIndex);
                    break;
                }
                randomIndex = Random.Range(0, DataManager.Instance.Skill_Passive_Dic.Count);
            }

            int skillId = System.Convert.ToInt32(DataManager.Instance.Skill_Passive_Dic[randomIndex]["ID"]);
            synergy.Init(skillId);
            
        }
    }

    public void CloseReward()
    {
        rewardDesc.CallDrop(false);
        Time.timeScale = 1f;
    }

    public void CallReward()
    {
        rewardDesc.CallDrop(true);
        Time.timeScale = 0f;
        SetReward();
    }

    public void UpdateInteractText(bool value, Vector3 targetPos)
    {
        interactText.gameObject.SetActive(value);
        if (value)
            UtillHelper.UpdateUIPosition(targetPos, interactText, new Vector2(0, yOffset));


    }
}
