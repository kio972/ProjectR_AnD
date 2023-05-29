using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceSeparation : SkillMain
{
    public float radius = 15f;
    private GameObject guideObject;

    public override IEnumerator ISkillFunc(Controller attacker, bool mouseRotate = false)
    {
        if(guideObject == null)
        {
            guideObject = new GameObject();
            guideObject.name = "SpaceSepartionGuide";
        }

        for (int i = 0; i < 15; i++)
        {
            Vector3 randomPos = UtillHelper.GetRandomPosition(attacker.transform.position, radius);
            guideObject.transform.position = randomPos;
            EffectManager.Instance.PlayEffect("SpaceSepartion", guideObject.transform);
            yield return new WaitForSeconds(0.5f);
        }

        print("use SpaceSepration");
        after_Delay = 1f;
        yield return StartCoroutine(IAfterDelay(() => { }));
        SkillEnd(attacker);
    }
}
