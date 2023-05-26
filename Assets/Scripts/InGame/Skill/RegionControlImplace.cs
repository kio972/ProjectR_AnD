using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegionControlImplace : SkillMain
{
    public override IEnumerator ISkillFunc(Controller attacker, bool mouseRotate = false)
    {
        print("use RegionControlImplace");
        SkillEnd(attacker);
        yield return null;
    }
}
