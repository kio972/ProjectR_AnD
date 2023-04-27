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
    public float attackRange = 2f;
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
    public bool IsAttacking { get { return isAttacking; } set { isAttacking = value; } }
    
    public float rotateTime = 0.1f;

    public UnitType unitType = UnitType.Monster;

    public bool isDead = false;

    public SkillMain basicAttack = null;

    public CCType curCCState = CCType.None;
    private float ccDuration = 0f;
    private float ccElapsed = 0f;
    public float CCDuration { get => ccDuration; }
    public float CCElapsed { get => ccElapsed; set => ccElapsed = value; }
    
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        InitState(this, FSMSpawn.Instance);
    }

    public int GetCCPriority(CCType ccType)
    {
        //CC우선순위 계산 함수 추가 필요
        return 0;
    }

    public void GetCC(CCType ccType, float ccDuration)
    {
        if (isDead)
            return;

        if(GetCCPriority(ccType) >= GetCCPriority(curCCState))
        {
            curCCState = ccType;
            this.ccDuration = ccDuration;
            ccElapsed = 0f;
            
            ChangeState(FSMCC.Instance);
        }
    }

    public void AttackCheck()
    {
        if (isAttacking)
            return;

        if (Input.GetKeyDown(InputManager.Instance.player_BasicAttackKey))
            SkillManager.Instance.UseBasicSkill(this);
        if (Input.GetKeyDown(InputManager.Instance.player_Skill1Key))
            SkillManager.Instance.UseDashSkill(this);
        if (Input.GetKeyDown(InputManager.Instance.player_SpecialAttackKey))
            SkillManager.Instance.UseSpecialSkill(this);
    }

    public void KeyBoardMove()
    {
        if (isAttacking)
            return;

        Vector3 direction = Vector3.zero;
        if (Input.GetKey(InputManager.Instance.player_MoveFront))
            direction += Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0) * Vector3.forward;
        if (Input.GetKey(InputManager.Instance.player_MoveBack))
            direction += Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0) * Vector3.back;
        if (Input.GetKey(InputManager.Instance.player_MoveLeft))
            direction += Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0) * Vector3.left;
        if (Input.GetKey(InputManager.Instance.player_MoveRight))
            direction += Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0) * Vector3.right;

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
        animator.SetInteger("Random", Random.Range(0, 3));
        animator.SetBool("Dead", true);
        isDead = true;
        Destroy(agent);
    }

    public virtual void Attack()
    {
        //공격처리
        StartCoroutine(UtillHelper.RotateTowards(transform, curTarget.transform.position, rotateTime));
        isAttacking = true;
        animator.SetInteger("Random", Random.Range(0, 3));
        animator.SetTrigger("Attack");
        if (basicAttack != null)
            basicAttack.SkillCheck(this, false);
    }

    public void PatrolMove()
    {
        
    }

    public void FollowTarget()
    {
        agent.isStopped = false;
        agent.SetDestination(curTarget.transform.position);
    }

    public float DamageModify(float baseDamage)
    {
        float damage = Mathf.Round(baseDamage * damageRate);
        return damage;
    }

    public void ModifyHp(float value)
    {
        if (isDead)
            return;

        float curHp = hp;
        curHp += value;
        curHp = Mathf.Round(curHp);
        if (curHp > maxHp)
            curHp = maxHp;
        if (curHp < 0)
            curHp = 0;
        hp = curHp;

        if (hp <= 0)
            ChangeState(FSMDead.Instance);
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
        if (finalDamage <= 0)
            return 0;

        ModifyHp(-finalDamage);
        if (animator != null)
            animator.SetTrigger("Damaged");
        return finalDamage;
    }

    public void ForcedSetPosition(Vector3 positon)
    {
        agent.Warp(positon);
    }

    // Update is called once per frame
    void Update()
    {
        FSMUpdate();
    }
}
