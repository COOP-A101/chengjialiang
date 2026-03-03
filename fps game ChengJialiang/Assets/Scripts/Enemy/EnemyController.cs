using UnityEngine.AI;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private enum State
    {
        IDLE, WALKING, CHASING, ATTACKING
    }

    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float walkingSpeed = 2f;
    [SerializeField] private float runningSpeed = 6f;
    [SerializeField] private float attackInterval = 1f; // 每秒攻击一次

    private Vector3 startingPosition;
    private Vector3 roamPosition;
    private Vector3 targetPosition;
    private State state;

    private EnemyAI enemyAI;
    private Animator animator;
    private Transform player;
    private float lastAttackTime; // 记录上次攻击的时间
    public float pursuitRange = 20f;
    public float attackRange = 3f;
    public float attack = 1f;

    private void Awake()
    {
        enemyAI = gameObject.GetComponent<EnemyAI>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Start()
    {
        state = State.IDLE;
        startingPosition = transform.position;
        roamPosition = enemyAI.GetRoamingPosition(startingPosition);
    }

    private void Update()
    {
        HandleAI();
    }

    private void HandleAI()
    {
        SwitchMovements();
        FindTarget();
        IsTargetNear();
        StopChasing();
    }

    private void SwitchMovements()
    {
        switch (state)
        {
            case State.CHASING:
                HandleChase();
                break;
            case State.ATTACKING:
                HandleAttack();
                break;
            default:
                HandleIdle();
                return;
        }
    }

    private void HandleIdle()
    {
        animator.SetFloat("Movement", 0f, 0.1f, Time.deltaTime);
    }

    private void HandleWalk()
    {
        animator.SetFloat("Movement", 0.25f, 0.1f, Time.deltaTime);
        enemyAI.MoveTo(roamPosition);
        agent.speed = walkingSpeed;
        if (Vector3.Distance(transform.position, roamPosition) < 1f)
        {
            roamPosition = enemyAI.GetRoamingPosition(startingPosition);
        }
    }

    private void HandleChase()
    {
        animator.SetFloat("Movement", 0.5f, 0.1f, Time.deltaTime);
        transform.LookAt(player);
        targetPosition = player.position + offset;
        enemyAI.MoveTo(targetPosition);
        agent.speed = runningSpeed;
    }

    private void HandleAttack()
    {
        animator.SetFloat("Movement", 0.75f, 0.1f, Time.deltaTime);

        // 判断是否可以攻击（根据间隔）
        if (Time.time - lastAttackTime >= attackInterval)
        {
            lastAttackTime = Time.time;
            player.GetComponent<PlayerHealth>().DecreaseHealthValue(attack); // 减少玩家血量
        }
    }

    private void FindTarget()
    {
        if (Vector3.Distance(transform.position, player.position) < pursuitRange)
        {
            state = State.CHASING;
        }
    }

    private void IsTargetNear()
    {
        if (Vector3.Distance(transform.position, player.position) <= attackRange)
        {
            state = State.ATTACKING;
            agent.isStopped = true;
        }
        else
        {
            agent.isStopped = false;
        }
    }

    private void StopChasing()
    {
        if (state == State.CHASING)
        {
            if (Vector3.Distance(transform.position, player.position) > pursuitRange)
            {
                animator.SetFloat("Movement", 0f, 0.1f, Time.deltaTime);
                state = State.IDLE;
            }
        }
    }
}