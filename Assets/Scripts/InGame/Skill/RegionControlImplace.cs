using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegionControlImplace : SkillMain
{
    public float radius = 13f;
    private GameObject guideObject;

    public override IEnumerator ISkillFunc(Controller attacker, bool mouseRotate = false)
    {
        if (guideObject == null)
        {
            guideObject = new GameObject();
            guideObject.name = "RegionControlImplaceGuide";
        }

        for (int i = 0; i < 5; i++)
        {
            Vector3 randomPos = UtillHelper.GetRandomPosition(attacker.transform.position, radius);
            guideObject.transform.position = randomPos;
            EffectManager.Instance.PlayEffect("RegionControlImplace", guideObject.transform);
        }

        after_Delay = 1f;
        yield return StartCoroutine(IAfterDelay(() => { }));
        SkillEnd(attacker);
    }
}
