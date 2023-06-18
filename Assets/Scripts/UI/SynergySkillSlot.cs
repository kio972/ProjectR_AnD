using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SynergySkillSlot : MonoBehaviour
{
    public int skillId = -1;
    [SerializeField]
    private Button treeCallBtn;
    [SerializeField]
    private Image skillIcon;
    string prefabPath;

    private void TreeCall()
    {
        TreeControl tree = Resources.Load<TreeControl>(prefabPath);
        if (tree == null)
            return;

        tree = Instantiate(tree, SynergyManager.Instance.treeZone);

    }

    public void Init(int skillId)
    {
        treeCallBtn.onClick.AddListener(TreeCall);
        this.skillId = skillId;
        int skillIndex = UtillHelper.Find_Data_Index(skillId, DataManager.Instance.Skill_Active_Dic);
        string spriteName = DataManager.Instance.Skill_Active_Dic[skillIndex]["Icon"].ToString();
        skillIcon.sprite = SpriteList.Instance.LoadSprite(spriteName);
        prefabPath = DataManager.Instance.Skill_Active_Dic[skillIndex]["Prefab"].ToString();
    }
}
