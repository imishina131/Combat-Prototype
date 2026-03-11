using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    // protected keeps it private but lets children access it
    protected NavMeshAgent agent;
    protected Transform player;

    public float attackRange = 2f;
    public float attackCooldown = 1.5f;

    protected float lastAttackTime;

    protected virtual void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    protected virtual void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= attackRange)
        {
            agent.ResetPath();
            TryAttack();
        }
        else if(gameObject.CompareTag)
        {
            //ChasePlayer();
        }
    }

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
}
