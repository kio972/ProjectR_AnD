using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    private SpawnPoint[] spawnPoints;
    public Collider spawnTrigger = null;
    public ChapterController chapterController;

    public void SpawnMonsters(float spawnWaitTime = 2f)
    {
        foreach (SpawnPoint spawnPoint in spawnPoints)
        {
            int id = spawnPoint.monsterIndex;
            if (id == -1)
                continue;
            Controller monster = UtillHelper.Find_Prefab<Controller>(id, DataManager.Instance.CharacterDic);
            if (monster == null)
                continue;
            monster = Instantiate(monster, spawnPoint.transform);
            int characterIndex = UtillHelper.Find_Data_Index(id, DataManager.Instance.CharacterDic);
            monster.Init(DataManager.Instance.CharacterDic[characterIndex]);
            monster.spawnTime = spawnWaitTime;
            chapterController.monsterGroup.Add(monster);
        }
    }

    public void Init(float spawnWaitTime = 2f)
    {
        spawnPoints = GetComponentsInChildren<SpawnPoint>();
        foreach (SpawnPoint spawnPoint in spawnPoints)
            spawnPoint.Init();

        if (spawnTrigger == null)
            SpawnMonsters(spawnWaitTime);
    }

    private void Update()
    {
        if(spawnTrigger != null)
        {

        }
    }
}
