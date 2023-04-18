using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMAttack : FSMSingleton<FSMAttack>, CharState<Controller>
{
    public void Enter(Controller e)
    {
        
    }

    public void Excute(Controller e)
    {
        e.Attack();
        if (e.attackElapsed > e.attackDelayTime)
            e.ChangeState(FSMFollow.Instance);
    }

    public void Exit(Controller e)
    {

    }
}
