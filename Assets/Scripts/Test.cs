using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Test : MonoBehaviour
{
    public Controller monster;
    // Update is called once per frame
    void Start()
    {
        if (monster == null)
            return;

        monster.Init(DataManager.Instance.CharacterDic[1]);
    }
}
