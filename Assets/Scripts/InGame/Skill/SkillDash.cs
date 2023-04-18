using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDash : SkillMain
{
    public override IEnumerator ISkillFunc(Controller attacker)
    {

        yield return null;
    }


    public override void SkillCheck(Controller attacker)
    {
        //float damage = attacker.baseDamage;
        //damage = attacker.DamageModify(damage);
        //damage = victim.TakeDamage(damage);
        //attacker.DrainHp(damage);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
