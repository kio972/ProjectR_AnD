using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitIniter : MonoBehaviour
{
    public Controller monster;
    public int unitId = -1;
    // Update is called once per frame
    void Start()
    {
        if (monster == null)
            return;

        int unitIndex = UtillHelper.Find_Data_Index(unitId, DataManager.Instance.CharacterDic);
        if (unitIndex != -1)
            monster.Init(DataManager.Instance.CharacterDic[unitIndex]);
        else
            monster.Init();
    }
}
