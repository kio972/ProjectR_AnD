using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceSeparation : SkillMain
{
    public override IEnumerator ISkillFunc(Controller attacker, bool mouseRotate = false)
    {
        print("use SpaceSepration");
        SkillEnd(attacker);
        yield return null;
    }
}
