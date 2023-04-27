using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CCType
{
    None,
    Stiff,
    Stun,
}

public class FSMCC : FSMSingleton<FSMCC>, CharState<Controller>
{
    public void Enter(Controller e)
    {
        print(e + " Get CC:" + e.curCCState + " Duration:" + e.CCDuration);
        if (e.basicAttack != null)
            e.basicAttack.StopSkill(e);
        else
            SkillManager.Instance.curSkill.StopSkill(e);
    }

    public void Excute(Controller e)
    {
        e.CCElapsed += Time.deltaTime;
        if(e.CCElapsed >= e.CCDuration)
        {
            e.CCElapsed = 0;
            e.ChangeState(e.PrevState);
        }
    }

    public void Exit(Controller e)
    {

    }
}
