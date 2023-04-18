using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Controller : FSM<Controller>
{
    public string name;

    public NavMeshAgent agent;

    public float hp = 150;
    public float maxHp = 150;
    public float baseDamage = 30;
    public float attackRange = 1f;
    public float criticalChange = 0.15f;
    public float speed = 1f;
    public int dashCount = 1;
    public float damageRate = 1f;
    public float damageReduce = 0f;
    public float drainRate = 0f;

    public Animator animator;

        
    public float spawnTime = 2f;
    public float spawnElapsed = 0f;

    public float patrolDist = 15f;
    public float chasingDist = 20f;
    public float maxAggroRange = 20f;
    public LayerMask enemyLayer;

    public Controller curTarget;

    public float attackDelayTime = 2;
    public float attackElapsed = 0f;

    public float stopDelayTime = 0.1f;
    private float stopDelayElapsed = 0f;

    private bool isAttacking = false;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void BasicAttack()
    {
        isAttacking = true;
        //현재 마우스방향으로 캐릭터 회전
        if (animator != null)
            animator.SetTrigger("Attack");
        //공격전방 부챼꼴 10도 사거리 1이내의 적들에게 데미지 처리
    }

    private void DashAttack()
    {
        isAttacking = true;
        //현재 마우스방향으로 캐릭터 회전
        if (animator != null)
            animator.SetTrigger("Attack2");

        /*마우스 방향으로 2M 이동
        돌진 이동중 경로에 적과 충돌시 멈춤
        충돌시 적에게 30의 데미지를 입힘
        쿨타임 존재
        기본 쿨타임 5초 */
    }

    private void SpecialAttack()
    {
        isAttacking = true;
        if (animator != null)
            animator.SetTrigger("Special");
    }

    public void AttackCheck()
    {
        if (!isAttacking)
            return;

        if (Input.GetKeyDown(InputManager.Instance.player_BasicAttackKey))
            BasicAttack();
        if (Input.GetKeyDown(InputManager.Instance.player_Skill1Key))
            DashAttack();
        if (Input.GetKeyDown(InputManager.Instance.player_SpecialAttackKey))
            SpecialAttack();
    }

    public void KeyBoardMove()
    {
        Vector3 direction = Vector3.zero;
        if (Input.GetKey(InputManager.Instance.player_MoveFront))
            direction += Vector3.forward;
        if (Input.GetKey(InputManager.Instance.player_MoveBack))
            direction += Vector3.back;
        if (Input.GetKey(InputManager.Instance.player_MoveLeft))
            direction += Vector3.left;
        if (Input.GetKey(InputManager.Instance.player_MoveRight))
            direction += Vector3.right;

        direction = direction.normalized;

        if (direction != Vector3.zero)
        {
            Vector3 nextDest = transform.position + direction;
            agent.isStopped = false;
            agent.SetDestination(nextDest);
            stopDelayElapsed = 0;
            if (animator != null)
                animator.SetBool("Move", true);
        }
        else
        {
            if (stopDelayElapsed > stopDelayTime)
            {
                agent.isStopped = true;
                if (animator != null)
                    animator.SetBool("Move", false);
            }

            stopDelayElapsed += Time.deltaTime;
        }
    }

    public void Dead()
    {

    }

    public void Attack()
    {
        
    }

    public void PatrolMove()
    {
        
    }

    public void FollowTarget()
    {
        agent.SetDestination(curTarget.transform.position);
    }

    public float DamageModify(float baseDamage)
    {
        float damage = Mathf.Round(baseDamage * damageRate);
        return damage;
    }

    public void ModifyHp(float value)
    {
        float curHp = hp;
        curHp += value;
        curHp = Mathf.Round(curHp);
        if (curHp > maxHp)
            curHp = maxHp;
        if (curHp < 0)
            curHp = 0;
        hp = curHp;
    }

    public void DrainHp(float damage)
    {
        float drainAmount = Mathf.Round(damage * drainRate);
        if(drainAmount > 0)
        {
            ModifyHp(drainAmount);
        }
    }

    public float TakeDamage(float damage)
    {
        float finalDamage = Mathf.Round(damage * (1 - damageReduce));
        ModifyHp(-finalDamage);
        return finalDamage;
    }

    public void ForcedSetPosition(Vector3 positon)
    {
        agent.Warp(positon);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
