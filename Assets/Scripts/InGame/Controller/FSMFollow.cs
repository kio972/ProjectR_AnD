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
            return;
        }
        else if (dist < e.attackRange)
        {
            float angle = UtillHelper.TargetAngle(e.transform, e.curTarget.transform.position);
            print(angle);
            if (!e.canMove && angle < 10f)
            {
                e.ChangeState(FSMAttack.Instance);
                return;
            }
            else if(e.canMove)
            {
                e.ChangeState(FSMAttack.Instance);
                return;
            }
        }

        if (e.canMove)
        {
            e.FollowTarget();
            e.animator.SetBool("Move", true);
        }
        else
            e.LookTarget();
    }

    public void Exit(Controller e)
    {
        e.animator.SetBool("Move", false);
        print(e.name + "Follow End");
    }
}