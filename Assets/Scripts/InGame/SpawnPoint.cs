using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public int mapID = -1;
    public int spawnIndex = -1;
    public int monsterIndex = -1;

    public void Init()
    {
        if (mapID == -1 || spawnIndex == -1)
            return;

        //mapTable에서 몬스터 id가져와 저장
        monsterIndex = DataManager.Instance.Find_SpawnMonsterID(mapID, spawnIndex);
    }
}
