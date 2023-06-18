using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapterController : MonoBehaviour
{
    public Transform startPosition;
    [SerializeField]
    private Transform endPosition;
    [SerializeField]
    private Transform synergyStonePoint;
    private SynergyStone synergyStone;

    private SpawnController[] spawnGroups;

    public List<Controller> monsterGroup = new List<Controller>();

    public bool isChapterStart = false;
    public bool isChapterEnd = false;

    private bool isMonsterCleared = false;

    public Transform cutSceneCam;
    public float waitTime = 5f;
    private float elapsedTime = 0f;

    private bool SpwanNext(float waitTime = 2f)
    {
        for(int i = 0; i < spawnGroups.Length; i++)
        {
            if(!spawnGroups[i].isSpawned && spawnGroups[i].spawnTrigger == null)
            {
                spawnGroups[i].SpawnMonsters(waitTime);
                return true;
            }
        }

        return false;
    }

    public void Init()
    {
        if (endPosition != null)
            endPosition.gameObject.SetActive(false);
        spawnGroups = GetComponentsInChildren<SpawnController>();
        foreach (SpawnController spawnGroup in spawnGroups)
        {
            spawnGroup.chapterController = this;
            spawnGroup.Init();
        }
        SpwanNext(waitTime);

        SynergyStone stone = Resources.Load<SynergyStone>("Prefab/Objects/SynergyStone");
        stone = Instantiate(stone, synergyStonePoint.transform);
        //stone.transform.position = synergyStonePoint.position;
        synergyStone = stone;

        GameManager.Instance.PlayCutScene(true);
        cutSceneCam.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (isMonsterCleared)
            return;

        if(isChapterStart)
        {
            if (elapsedTime < waitTime)
                elapsedTime += Time.deltaTime;
            else if(cutSceneCam != null)
            {
                GameManager.Instance.PlayCutScene(false);
                cutSceneCam.gameObject.SetActive(false);
            }

            foreach (Controller monster in monsterGroup)
            {
                if (!monster.isDead)
                    return;
            }

            if (SpwanNext())
                return;

            isMonsterCleared = true;
            endPosition.gameObject.SetActive(true);
            synergyStone.SetActive(true);
        }
    }
}
