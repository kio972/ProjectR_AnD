using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionCutControl_Boss : MonoBehaviour
{
    public Animator bossAnimator;
    public WingAnimationController bossWingAnimator;

    public Transform directionVirtualCamera;
    public float waitTime = 2f;
    private bool isEnd = false;
    public bool IsEnd { get { return isEnd; } }

    public IEnumerator IStartDirection()
    {
        float elapsedTime = 0f;

        //DirecitonºÎºÐ
        bossAnimator.SetTrigger("Shout");
        bossWingAnimator.ChangeBlend("wingSpread", 1, 0.8f);


        while (elapsedTime < waitTime)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        directionVirtualCamera.gameObject.SetActive(false);
        isEnd = true;

        Invoke("ActiveBoss", 0.5f);
        yield return null;
    }

    private void ActiveBoss()
    {
        EnemyController enemyController = FindAnyObjectByType<EnemyController>();
        if (enemyController != null)
            enemyController.isActive = true;
    }

    private void StartDirection()
    {
        StartCoroutine(IStartDirection());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            StartDirection();
    }
}
