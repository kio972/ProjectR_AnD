using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public string name;

    public float hp = 150;
    public float maxHp = 150;
    public float baseDamage = 30;
    public float attackRange = 1f;
    public float criticalChange = 0.15f;
    public float speed = 1f;
    public int dashCount = 1;
    public float damageRate = 1f;
    public float damageReduce = 0f;
    public float drainRate = 0f;

    public float DamageModify(float baseDamage)
    {
        float damage = Mathf.Round(baseDamage * damageRate);
        return damage;
    }

    public void ModifyHp(float value)
    {
        float curHp = hp;
        curHp += value;
        curHp = Mathf.Round(curHp);
        if (curHp > maxHp)
            curHp = maxHp;
        if (curHp < 0)
            curHp = 0;
        hp = curHp;
    }

    public void DrainHp(float damage)
    {
        float drainAmount = Mathf.Round(damage * drainRate);
        if(drainAmount > 0)
        {
            ModifyHp(drainAmount);
        }
    }

    public float TakeDamage(float damage)
    {
        float finalDamage = Mathf.Round(damage * (1 - damageReduce));
        ModifyHp(-finalDamage);
        return finalDamage;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
