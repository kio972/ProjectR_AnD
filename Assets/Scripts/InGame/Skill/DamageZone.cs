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

    private void Excute()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius/2);
        foreach(Collider collider in colliders)
        {
            Controller target = collider.GetComponentInParent<Controller>();
            if (target == null || target.unitType != targetType)
                continue;
            target.TakeDamage(damage);
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

        if(effect.isPlaying && isActive)
        {
            Excute();
        }
        else if(!effect.isPlaying && !isActive)
        {
            isActive = true;
        }
    }
}
