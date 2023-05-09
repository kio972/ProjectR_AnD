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
        e.agent.isStopped = true;
        if (e.basicAttack != null)
            e.basicAttack.StopSkill(e);
        else if(e.unitType == UnitType.Player)
        {
            SkillManager.Instance.curSkill?.StopSkill(e);
            SkillManager.Instance.curSkill = null;
        }
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
        e.agent.isStopped = false;
    }
}
