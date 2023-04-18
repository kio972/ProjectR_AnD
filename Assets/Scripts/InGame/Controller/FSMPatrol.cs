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
        // enemyLayer�� controller�� ã��, ���� ����� ����� Ž���ϴ� �ڵ�
        // ���� ���� ��������� e.patrolDist �̳� �Ÿ��� �ִٸ� e.curTarget�� ������ FSMFollow.Instance�� ���º���
        LayerMask enemyLayer = e.enemyLayer;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(e.transform.position, e.patrolDist, enemyLayer);
        float closestDist = Mathf.Infinity;
        Controller closestController = null;
        foreach (Collider2D collider in colliders)
        {
            Controller controller = collider.GetComponent<Controller>();
            if (controller != null)
            {
                float dist = Vector2.Distance(e.transform.position, controller.transform.position);
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
