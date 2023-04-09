using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionCutControl_Boss : MonoBehaviour
{
    public Animator bossAnimator;
    public WingAnimationController bossWingAnimator;

    public Transform directionVirtualCamera;
    public float waitTime = 2f;
    public float rotateTime = 0.8f;
    private bool isEnd = false;
    public bool IsEnd { get { return isEnd; } }

    public Transform directionCameraRotateTarget;

    public IEnumerator IStartDirection()
    {
        float elapsedTime = 0f;

        //DirecitonºÎºÐ
        bossAnimator.SetTrigger("Shout");
        bossWingAnimator.ChangeBlend("wingSpread", 1, 0.8f);


        Quaternion startRotation = Quaternion.Euler(Vector3.zero);
        if(directionCameraRotateTarget != null)
            startRotation = directionCameraRotateTarget.transform.rotation;
        Quaternion endRotation = Quaternion.Euler(new Vector3(0f, 360f, 0f)) * startRotation;
        while (elapsedTime < waitTime)
        {
            elapsedTime += Time.deltaTime;

            if(directionCameraRotateTarget != null)
            {
                if (elapsedTime < rotateTime)
                {
                    float t = Mathf.Clamp01(elapsedTime / rotateTime);
                    float yRotation = Mathf.Lerp(0f, 360f, t);
                    Quaternion targetRotation = Quaternion.Euler(0f, yRotation, 0f);
                    directionCameraRotateTarget.transform.rotation = startRotation * targetRotation;
                }
                else
                    directionCameraRotateTarget.transform.rotation = endRotation;
            }

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
