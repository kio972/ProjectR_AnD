using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBasic : SkillMain
{

    public override void SkillCheck(Controller attacker, Controller victim)
    {
        float damage = attacker.baseDamage;
        damage = attacker.DamageModify(damage);
        damage = victim.TakeDamage(damage);
        attacker.DrainHp(damage);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
