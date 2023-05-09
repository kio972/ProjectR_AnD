using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMAttack : FSMSingleton<FSMAttack>, CharState<Controller>
{
    public void Enter(Controller e)
    {
        e.agent.isStopped = true;
        e.agent.stoppingDistance = e.attackRange;
    }

    public void Excute(Controller e)
    {
        if (e.attackElapsed == 0f)
        {
            e.Attack();
        }
        e.attackElapsed += Time.deltaTime;
        if (e.attackElapsed > e.attackDelayTime)
        {
            e.attackElapsed = 0f;
            e.ChangeState(FSMFollow.Instance);
        }
    }

    public void Exit(Controller e)
    {
        if (e.curTarget != null && e.curTarget.isDead)
            e.curTarget = null;
        e.agent.isStopped = false;
        e.IsAttacking = false;
        print(e.name + "Attack End");
    }
}
