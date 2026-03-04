using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    private float distance;
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        ChasePlayer();
    }


    // Update is called once per frame
    public void Update()
    {
        distance = Vector3.Distance(transform.position, player.position);

        if (distance <= 1.5f)
        {
            agent.ResetPath();
            Attack();
        }
        else
        {
            agent.SetDestination(player.position);
        }
    }

    void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    public virtual void Attack()
    {
        Debug.Log("basic");
    }

}
