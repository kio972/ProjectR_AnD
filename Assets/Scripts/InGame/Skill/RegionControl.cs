using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegionControl : SkillMain
{
    public override IEnumerator ISkillFunc(Controller attacker, bool mouseRotate = false)
    {
        print("use RegionControl");
        SkillEnd(attacker);
        yield return null;
    }
}
