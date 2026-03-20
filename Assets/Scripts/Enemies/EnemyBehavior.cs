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
    public LayerMask playerLayer;

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
        if (isStunned)
        {
            if (Time.time >= stunEndTime)
            {
                isStunned = false;
                animator.SetBool("isStunned", false);
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
        Vector3 attackPoint = transform.position + transform.forward * 1.5f;

        Collider[] hits = Physics.OverlapSphere(attackPoint, attackRadius, playerLayer);

        foreach (Collider hit in hits)
        {
            HealthBarScript hp = hit.GetComponent<HealthBarScript>();

            if (hp != null)
            {
                hp.TakeDamage(damage);
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
        animator.SetBool("isStunned", true);
        stunEndTime = Time.time + stunDuration;

        agent.ResetPath();

        Debug.Log("Enemy Stunned");
    }

    #region Gizmos
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + transform.forward * 1.5f, attackRadius);
    }
    #endregion Gizmos
}
