using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour
{
    private ChapterController[] chapters;

    private int curChapter = 0;

    [SerializeField]
    private Controller player;

    public void Init()
    {
        chapters = GetComponentsInChildren<ChapterController>();
        //foreach (ChapterController chapter in chapters)
        //    chapter.Init();
    }

    public void Start()
    {
        Init();
    }

    private void Update()
    {
        if (curChapter > chapters.Length - 1)
            return;

        if(!chapters[curChapter].isChapterStart)
        {
            chapters[curChapter].isChapterStart = true;
            chapters[curChapter].Init();
        }
        else if(chapters[curChapter].isChapterEnd)
        {
            curChapter++;
            if (curChapter > chapters.Length - 1)
            {
                //다음 챕터가 없다면, 다음 맵으로 이동
            }
            else
            {
                //다음 챕터가 있다면, 다음챕터의 시작포인트로 이동
                player.agent.enabled = false;
                player.transform.position = chapters[curChapter].startPosition.position;
                player.transform.rotation = chapters[curChapter].startPosition.rotation;
                player.agent.enabled = true;
                player.Init();
                player.animator.SetTrigger("Init");
            }
        }
    }
}
