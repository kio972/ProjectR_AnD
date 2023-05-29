using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegionControl : SkillMain
{
    public override IEnumerator ISkillFunc(Controller attacker, bool mouseRotate = false)
    {
        AngelBoss boss = attacker.GetComponent<AngelBoss>();
        if (boss != null)
            boss.auraEffect.Play();

        Transform targetPos = attacker.curTarget.transform;
        float radius = 3f;
        EffectManager.Instance.PlayEffect("RegionControl", targetPos, Vector3.zero, radius);

        after_Delay = 1f;
        yield return StartCoroutine(IAfterDelay(() => { }));
        SkillEnd(attacker);
        if (boss != null)
            boss.auraEffect.Stop();
    }
}
