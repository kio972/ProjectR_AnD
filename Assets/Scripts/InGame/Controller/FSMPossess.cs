using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FSMPossess : FSMSingleton<FSMPossess>, CharState<Controller>
{
    
    //private Vector3 lastDest = Vector3.zero;

    public bool isKeyboardControl = false;

    public void Enter(Controller e)
    {

    }

    public void Excute(Controller e)
    {
        if (e.agent.isOnNavMesh)
            return;

        e.KeyBoardMove();
        e.AttackCheck();
    }

    public void Exit(Controller e)
    {

    }


    //private void PointClickMove(Controller e)
    //{
    //    if (Input.GetKey(InputManager.Instance.player_MoveKey))
    //    {
    //        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //        RaycastHit hit;

    //        if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << LayerMask.NameToLayer("Terrain")))
    //        {
    //            NavMeshHit navHit;
    //            if (NavMesh.SamplePosition(hit.point, out navHit, 1.0f, NavMesh.AllAreas))
    //            {
    //                e.agent.isStopped = false;
    //                e.agent.SetDestination(navHit.position);
    //                if (e.animator != null)
    //                    e.animator.SetBool("Move", true);
    //            }
    //        }
    //    }
    //    else
    //    {
    //        if (e.agent.isStopped)
    //        {
    //            if (stopDelayElapsed > stopDelayTime)
    //            {
    //                e.agent.isStopped = true;
    //                stopDelayElapsed = 0f;
    //                if (e.animator != null)
    //                    e.animator.SetBool("Move", false);
    //            }
    //            else
    //                stopDelayElapsed += Time.deltaTime;
    //        }
    //    }
    //}

}
