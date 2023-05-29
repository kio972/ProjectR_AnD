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
    public bool continuous = false;
    public float interval = 0.2f;
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
                target.GetKnockBack(Vector3.zero);
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
