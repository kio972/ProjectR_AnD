using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegionControl : SkillMain
{
    public override IEnumerator ISkillFunc(Controller attacker, bool mouseRotate = false)
    {
        SkillEnd(attacker);
        yield return null;
    }
}
