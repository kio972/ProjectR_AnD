using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapterController : MonoBehaviour
{
    public Transform startPosition;
    [SerializeField]
    private Transform endPosition;

    private SpawnController[] spawnGroups;

    public List<Controller> monsterGroup = new List<Controller>();

    public bool isChapterStart = false;
    public bool isChapterEnd = false;

    private bool isMonsterCleared = false;

    public Transform cutSceneCam;
    public float waitTime = 5f;
    private float elapsedTime = 0f;


    public void Init()
    {
        //if (startPosition == null)
        //    ;
        if (endPosition != null)
            endPosition.gameObject.SetActive(false);
        spawnGroups = GetComponentsInChildren<SpawnController>();
        foreach (SpawnController spawnGroup in spawnGroups)
        {
            spawnGroup.chapterController = this;
            spawnGroup.Init(waitTime);
        }
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
                cutSceneCam.gameObject.SetActive(false);

            foreach (Controller monster in monsterGroup)
            {
                if (!monster.isDead)
                    return;
            }

            isMonsterCleared = true;
            endPosition.gameObject.SetActive(true);
        }
    }
}
