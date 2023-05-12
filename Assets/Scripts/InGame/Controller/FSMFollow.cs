using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMFollow : FSMSingleton<FSMFollow>, CharState<Controller>
{
    public void Enter(Controller e)
    {
        if(e.curTarget != null)
            print(e.name + "Following" + e.curTarget.name);
        
    }

    public void Excute(Controller e)
    {
        if (e.curTarget == null)
        {
            e.ChangeState(FSMPatrol.Instance);
            return;
        }

        float dist = (e.transform.position - e.curTarget.transform.position).magnitude;
        if (dist > e.maxAggroRange)
        {
            e.curTarget = null;
            e.ChangeState(FSMPatrol.Instance);
        }
        else if (dist < e.attackRange)
        {
            e.ChangeState(FSMAttack.Instance);
        }
        else
        {
            e.FollowTarget();
            e.animator.SetBool("Move", true);
        }
    }

    public void Exit(Controller e)
    {
        e.animator.SetBool("Move", false);
        print(e.name + "Follow End");
    }
}