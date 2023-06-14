using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceSeparation : SkillMain
{
    public float radius = 18f;
    public float deadRadius = 5f;
    private GameObject guideObject;

    public override IEnumerator ISkillFunc(Controller attacker, bool mouseRotate = false)
    {
        AngelBoss boss = attacker.GetComponent<AngelBoss>();
        if (boss != null)
            boss.auraEffect.Play();
        float targetPrevHp = attacker.curTarget.hp;
        bool getStun = true;
        if(guideObject == null)
        {
            guideObject = new GameObject();
            guideObject.name = "SpaceSepartionGuide";
        }

        for (int i = 0; i < 20; i++)
        {
            Vector3 randomPos = UtillHelper.GetRandomPosition(attacker.transform.position, radius, deadRadius);
            guideObject.transform.position = randomPos;
            EffectManager.Instance.PlayEffect("SpaceSepartion", guideObject.transform);
            yield return new WaitForSeconds(0.3f);
            if (attacker.curTarget.hp < targetPrevHp)
                getStun = false;
        }

        print("use SpaceSepration");
        after_Delay = 1f;
        yield return StartCoroutine(IAfterDelay(() => { }));
        SkillEnd(attacker);
        if (boss != null)
            boss.auraEffect.Stop();
        if (getStun)
            attacker.GetCC(CCType.Stun, 5f);
    }
}
