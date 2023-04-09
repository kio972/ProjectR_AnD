using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : Controller
{
    private PlayerController player;
    public float chaseDist = 10f;
    public float stopDist = 2f;
    public NavMeshAgent agent;
    public Animator animator;

    private bool isAttacking = false;
    private bool attackTriggered = false;

    public bool isActive = false;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Attack()
    {
        if (!isAttacking)
            return;

        if(!attackTriggered)
        {
            int randomIndex = Random.Range(0, 4);
            attackTriggered = true;
            animator.SetInteger("RandomAttack", randomIndex);
            animator.SetTrigger("Attack");
        }
        else
        {
            if(animator.GetCurrentAnimatorStateInfo(0).IsTag("IDLE"))
            {
                isAttacking = false;
                attackTriggered = false;
            }
        }
    }

    private void Chase()
    {
        if (isAttacking)
            return;

        float dist = (transform.position - player.transform.position).magnitude;
        if(dist < chaseDist && dist >= stopDist)
        {
            agent.isStopped = false;
            agent.SetDestination(player.transform.position);
            animator.SetBool("Move", true);
        }
        else if(dist < stopDist)
        {
            agent.isStopped = true;
            isAttacking = true;
            animator.SetBool("Move", false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null || !isActive)
            return;

        Chase();
        Attack();
    }
}
