using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileObject : MonoBehaviour
{
    public float movingDist = 20f;
    public float duration = 1f;
    public float movingSpeed = 10f; // 1초당 몇m를 이동할지

    private float elapsedTime = 0f;

    public Transform rootTransform;

    void OnCollisionEnter(Collision collision)
    {
        Controller controller = collision.gameObject.GetComponentInParent<Controller>();
        if (controller != null)
        {
            // 타겟에 Controller 클래스가 있음
            // 추가적인 동작 수행
            print("hi");
            controller.TakeDamage(150);
            AudioManager.Instance.Play2DSound("PostEffect2", 1);
        }
    }

    private void Update()
    {
        float distanceToMove = movingSpeed * Time.deltaTime;
        transform.position += transform.forward * distanceToMove;
        elapsedTime += Time.deltaTime;
        if(elapsedTime > duration)
        {
            elapsedTime = 0f;
            if (rootTransform == null)
                rootTransform = transform;
            rootTransform.gameObject.SetActive(false);
        }
    }
}
