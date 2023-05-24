using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileObject : MonoBehaviour
{
    public float movingDist = 20f;
    public float duration = 1f;
    public float movingSpeed = 10f; // 1초당 몇m를 이동할지
    public float damage = 150f;

    public UnitType targetType = UnitType.Player;

    public string soundName = "PostEffect2";

    private float elapsedTime = 0f;

    public Transform rootTransform;

    public string collisionEffect = string.Empty;

    void OnCollisionEnter(Collision collision)
    {
        Controller controller = collision.gameObject.GetComponentInParent<Controller>();
        if (controller != null && controller.unitType == targetType)
        {
            controller.TakeDamage(damage);
            EffectManager.Instance.PlayEffect(collisionEffect, controller.transform);
            AudioManager.Instance.Play2DSound(soundName, 1);
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
