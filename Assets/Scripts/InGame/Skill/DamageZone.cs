using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    public float radius = 1f;
    public float damage;
    public UnitType targetType = UnitType.Player;

    private ParticleSystem effect;
    private bool isActive = true;

    public bool knockBack = false;
    public float knockBackTime = 0.1f;
    public bool continuous = false;
    public float interval = 0.2f;
    public float duration = 0f;
    private float elapsedTime = 0f;
    private float timer = 0f;

    private void Excute()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius/2);
        foreach(Collider collider in colliders)
        {
            Controller target = collider.GetComponentInParent<Controller>();
            if (target == null || target.unitType != targetType)
                continue;
            target.TakeDamage(damage);
            if (knockBack)
            {
                Vector3 direction = (target.transform.position - transform.position).normalized;
                target.GetKnockBack(direction, knockBackTime);
                target.GetCC(CCType.Stiff, knockBackTime);
            }
        }

        isActive = false;
    }

    private void Start()
    {
        effect = GetComponentInChildren<ParticleSystem>();
    }

    private void Update()
    {
        if (effect == null)
            return;

        if (continuous)
        {
            timer += Time.deltaTime;
            if (timer >= interval)
            {
                Excute();
                timer = 0f;
            }

            if (duration != 0)
            {
                elapsedTime += Time.deltaTime;
                if(elapsedTime >= duration)
                {
                    elapsedTime = 0f;
                    gameObject.SetActive(false);
                }
            }
        }
        else
        {
            if (effect.isPlaying && isActive)
                Excute();
            else if (!effect.isPlaying && !isActive)
                isActive = true;
        }
    }
}
