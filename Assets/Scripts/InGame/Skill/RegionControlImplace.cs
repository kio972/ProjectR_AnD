using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegionControlImplace : SkillMain
{
    public float radius = 16f;
    public float deadRadius = 5f;
    private GameObject guideObject;

    public override IEnumerator ISkillFunc(Controller attacker, bool mouseRotate = false)
    {
        AngelBoss boss = attacker.GetComponent<AngelBoss>();
        if (boss != null)
            boss.auraEffect.Play();
        //ÄðÅ¸ÀÓ 20ÃÊ
        if (guideObject == null)
        {
            guideObject = new GameObject();
            guideObject.name = "RegionControlImplaceGuide";
        }

        for (int i = 0; i < 6; i++)
        {
            Vector3 randomPos = UtillHelper.GetRandomPosition(attacker.transform.position, radius, deadRadius);
            guideObject.transform.position = randomPos;
            EffectManager.Instance.PlayEffect("RegionImplace", guideObject.transform);
        }

        after_Delay = 1f;
        yield return StartCoroutine(IAfterDelay(() => { }));
        SkillEnd(attacker);
        if (boss != null)
            boss.auraEffect.Stop();
    }
}
