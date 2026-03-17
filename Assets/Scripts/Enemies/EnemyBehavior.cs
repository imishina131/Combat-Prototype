using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    // reference to enemy class
    protected Enemy enemy;

    // protected keeps it private but lets children access it
    protected NavMeshAgent agent;
    protected Transform player;

    public float attackRange = 2f;
    public float attackCooldown = 3f;

    // stun variables
    protected bool isStunned = false;
    protected float stunEndTime;
    public float stunDuration = 2f;

    protected float lastAttackTime;

    protected Animator animator;

    protected virtual void Awake()
    {
        enemy = GetComponent<Enemy>();

        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        animator = GetComponent<Animator>();
    }

    protected virtual void Update()
    {
        if (isStunned)
        {
            if (Time.time >= stunEndTime)
            {
                isStunned = false;
            }
            return;
        }

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= attackRange)
        {
            agent.ResetPath();
            TryAttack();
        }
        else
        {
            agent.SetDestination(player.position);
        }
    }

    #region EventSubscription
    private void OnEnable()
    {
        Enemy.OnEnemyHit += HandleEnemyHit;
    }

    private void OnDisable()
    {
        Enemy.OnEnemyHit -= HandleEnemyHit;
    }
    #endregion EventSubscription

    protected virtual void TryAttack()
    {
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            Attack();
            lastAttackTime = Time.time;
        }
    }

    public virtual void Attack()
    {
        Debug.Log("Enemy Attack");
    }

    void HandleEnemyHit(Enemy hitEnemy)
    {
        if (hitEnemy != enemy) return;

        if (Random.value < 0.1f)
        {
            Stun();
        }
    }

    void Stun()
    {
        isStunned = true;
        stunEndTime = Time.time + stunDuration;

        agent.ResetPath();

        Debug.Log("Enemy Stunned");
    }
}
