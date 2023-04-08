using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public NavMeshAgent agent;
    public float stopDelayTime = 0.1f;
    private float stopDelayElapsed = 0f;

    public void ForcedSetPosition(Vector3 positon)
    {
        agent.Warp(positon);
    }

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void PointClickMove()
    {
        if (Input.GetKey(InputManager.Instance.player_MoveKey))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Terrain")))
            {
                NavMeshHit navHit;
                if (NavMesh.SamplePosition(hit.point, out navHit, 1.0f, NavMesh.AllAreas))
                {
                    agent.isStopped = false;
                    agent.SetDestination(navHit.position);
                }
            }
        }
        else
        {
            if (agent.isStopped)
            {
                if (stopDelayElapsed > stopDelayTime)
                {
                    agent.isStopped = true;
                    stopDelayElapsed = 0f;
                }
                else
                    stopDelayElapsed += Time.deltaTime;
            }
        }
    }

    private void KeyBoardMove()
    {

    }

    private void Update()
    {
        if (!agent.isOnNavMesh)
            return;

        PointClickMove();
    }
}
