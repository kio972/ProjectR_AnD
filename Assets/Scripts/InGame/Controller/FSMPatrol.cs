using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMPatrol : FSMSingleton<FSMPatrol>, CharState<Controller>
{
    public void Enter(Controller e)
    {
        print(e.gameObject.name + " Start Patrol");
    }

    public void Excute(Controller e)
    {
        e.PatrolMove();
        // enemyLayer의 controller를 찾고, 가장 가까운 대상을 탐색하는 코드
        // 만약 가장 가까운대상이 e.patrolDist 이내 거리에 있다면 e.curTarget에 저장후 FSMFollow.Instance로 상태변경
        LayerMask enemyLayer = e.enemyLayer;
        Collider[] colliders = Physics.OverlapSphere(e.transform.position, e.patrolDist, enemyLayer);
        float closestDist = Mathf.Infinity;
        Controller closestController = null;
        foreach (Collider collider in colliders)
        {
            Controller controller = collider.GetComponentInParent<Controller>();
            if (controller != null)
            {
                float dist = Vector3.Distance(e.transform.position, controller.transform.position);
                if (dist < closestDist)
                {
                    closestDist = dist;
                    closestController = controller;
                }
            }
        }

        if (closestController != null)
        {
            e.curTarget = closestController;
            e.ChangeState(FSMFollow.Instance);
        }
    }

    public void Exit(Controller e)
    {
        print(e.gameObject.name + " End Patrol");
    }
}
