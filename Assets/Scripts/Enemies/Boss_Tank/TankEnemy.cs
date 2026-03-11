using UnityEngine;

public class TankEnemy : MonoBehaviour
{
    protected NavMeshAgent agent;
    protected Transform player;
    int damage = 10;

    float speed = 1.2f;
    float attackRange = 3f;
    float attackCooldown = 3f;

    float health = 100f;



    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= attackRange)
        {
            agent.ResetPath();
            Attack();
        }
        else
        {
            ChasePlayer();
        }
    }

    void Attack()
    {
        //"slap" the player
    }

    void ThrowProjectile()
    {
        //instantiate projectile thrown at last player position
    }

    void ChasePlayer()
    {

    }
}
