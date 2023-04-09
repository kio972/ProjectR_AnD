using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public NavMeshAgent agent;
    public float stopDelayTime = 0.1f;
    private float stopDelayElapsed = 0f;
    public Animator animator;

    //private Vector3 lastDest = Vector3.zero;

    public bool isKeyboardControl = true;

    public void ForcedSetPosition(Vector3 positon)
    {
        agent.Warp(positon);
    }

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
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
        Vector3 direction = Vector3.zero;
        if (Input.GetKey(InputManager.Instance.player_MoveFront))
            direction += Vector3.forward;
        if (Input.GetKey(InputManager.Instance.player_MoveBack))
            direction += Vector3.back;
        if (Input.GetKeyDown(InputManager.Instance.player_MoveLeft))
            direction += Vector3.left;
        if (Input.GetKeyDown(InputManager.Instance.player_MoveRight))
            direction += Vector3.right;

        direction = direction.normalized;

        if(direction != Vector3.zero)
        {
            Vector3 nextDest = transform.position + direction;
            agent.isStopped = false;
            agent.SetDestination(nextDest);
            stopDelayElapsed = 0;
        }
        else
        {
            if (stopDelayElapsed > stopDelayTime)
            {
                agent.isStopped = true;
            }

            stopDelayElapsed += Time.deltaTime;
        }
    }

    private void Update()
    {
        if (!agent.isOnNavMesh)
            return;

        if (isKeyboardControl)
            KeyBoardMove();
        else
            PointClickMove();

        if(animator != null)
            animator.SetBool("Move", !agent.isStopped);

    }
}
