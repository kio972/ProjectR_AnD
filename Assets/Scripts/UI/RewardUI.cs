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
