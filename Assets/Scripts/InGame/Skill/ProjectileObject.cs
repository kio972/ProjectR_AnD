using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileObject : MonoBehaviour
{
    public float movingDist = 20f;
    public float duration = 1f;
    public float movingSpeed = 10f; // 1�ʴ� ��m�� �̵�����

    private float elapsedTime = 0f;

    public Transform rootTransform;

    void OnCollisionEnter(Collision collision)
    {
        Controller controller = collision.gameObject.GetComponentInParent<Controller>();
        if (controller != null)
        {
            // Ÿ�ٿ� Controller Ŭ������ ����
            // �߰����� ���� ����
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
