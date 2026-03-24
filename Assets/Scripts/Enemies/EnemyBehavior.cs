using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    HealthBarScript playerHealth;

    // reference to enemy class
    protected Enemy enemy;

    // protected keeps it private but lets children access it
    protected NavMeshAgent agent;
    protected Transform player;

    public float attackRange = 2f;
    public float attackCooldown = 3f;
    public int damage = 10;  //  set damage in enemy prefab or in enemy script start

    // attack sphere variables
    public float attackRadius = 0.5f;
    public LayerMask Player;

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
        playerHealth = player.GetComponent<HealthBarScript>();

        animator = GetComponent<Animator>();
    }

    protected virtual void Update()
    {
        // if enemy is stunned, loop anim and stop from attacking
        if (isStunned)
        {
            if (Time.time >= stunEndTime)
            {
                isStunned = false;
                if (animator != null)
                {
                    animator.SetBool("isStunned", false);
                }    
            }
            return;
        }

        float distance = Vector3.Distance(transform.position, player.position);

        // if an enemy is dead they will stop following the player
        if (enemy != null && enemy.isDead)
        {
            if (agent != null)
            {
                agent.ResetPath();
                agent.isStopped = true;
            }
            return;
        }

        // if player is within attack range, stop moving and try to attack
        if (distance <= attackRange)
        {
            if (agent != null)
            {
                agent.ResetPath();
            }
            TryAttack();
        }
        else
        {
            if (agent != null)
            {
                agent.SetDestination(player.position);
            }
        }
    }

    // subscribing to enemy events
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
        // if enough time has passed attack
        if (Time.deltaTime >= lastAttackTime + attackCooldown)
        {
            Attack();
            lastAttackTime = Time.time;
        }
    }

    public virtual void Attack()
    {
        // creates a hitbox infront of the enemy and anything in the player layer gets grabbed and calls the take damage method
        Vector3 attackPoint = transform.position + transform.forward * 1.5f;

        Collider[] hits = Physics.OverlapSphere(attackPoint, attackRadius, Player);

        foreach (Collider hit in hits)
        {
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage, gameObject);
            }
        }
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
        if (animator != null)
        {
            animator.SetBool("isStunned", true);
        } 
        stunEndTime = Time.time + stunDuration;

        if (agent != null)
        {
            agent.ResetPath();
        }    
    }

    // used to visually see hitbox when enemy is selected
    #region Gizmos
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + transform.forward * 1.5f, attackRadius);
    }
    #endregion Gizmos
}
