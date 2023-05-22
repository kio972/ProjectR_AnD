using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitIniter : MonoBehaviour
{
    public Controller monster;
    public int unitIndex = -1;
    // Update is called once per frame
    void Start()
    {
        if (monster == null)
            return;

        if (unitIndex >= 0 && unitIndex < DataManager.Instance.CharacterDic.Count)
            monster.Init(DataManager.Instance.CharacterDic[unitIndex]);
        else
            monster.Init();
    }
}
