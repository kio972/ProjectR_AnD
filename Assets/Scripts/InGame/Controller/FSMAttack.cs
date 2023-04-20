using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMAttack : FSMSingleton<FSMAttack>, CharState<Controller>
{
    public void Enter(Controller e)
    {
        e.agent.isStopped = true;
        e.attackElapsed = 0;
        print(e.name + "Attack");
    }

    public void Excute(Controller e)
    {
        e.Attack();
        if (e.attackElapsed > e.attackDelayTime)
            e.ChangeState(FSMFollow.Instance);
    }

    public void Exit(Controller e)
    {
        e.agent.isStopped = false;
        e.IsAttacking = false;
        print(e.name + "Attack End");
    }
}
