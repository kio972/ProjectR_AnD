using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour
{
    private ChapterController[] chapters;

    private int curChapter = 0;

    [SerializeField]
    private Controller player;

    public string nextSceneName = "";

    private float waitTime = 0f;
    private float elapsedTime = 0f;

    public void Init()
    {
        chapters = GetComponentsInChildren<ChapterController>();
        //foreach (ChapterController chapter in chapters)
        //{

        //}
    }

    public void Start()
    {
        Init();
    }

    private void GoNextZone()
    {
        player.agent.enabled = false;
        player.transform.position = chapters[curChapter].startPosition.position;
        player.transform.rotation = chapters[curChapter].startPosition.rotation;
        player.agent.enabled = true;
        player.Init();
        player.animator.SetTrigger("Init");
    }

    private void Update()
    {
        if (curChapter > chapters.Length - 1)
            return;

        if (waitTime > 0)
        {
            elapsedTime += Time.deltaTime;
            if (waitTime < elapsedTime)
                return;
            elapsedTime = 0;
            waitTime = 0;
            UIFade uiFade = FindObjectOfType<UIFade>();
            if (uiFade != null)
                uiFade.FadeIn(0.2f);
        }

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
                //���� é�Ͱ� ���ٸ�, ���� ������ �̵�
                SceneController.Instance.MoveScene(nextSceneName);
            }
            else
            {
                //���� é�Ͱ� �ִٸ�, ����é���� ��������Ʈ�� �̵�
                UIFade uiFade = FindObjectOfType<UIFade>();
                if (uiFade != null)
                {
                    uiFade.FadeOut(0.2f);
                    Invoke("GoNextZone", 0.2f);
                    waitTime = 0.2f;
                }
                else
                    GoNextZone();
            }
        }
    }
}
