using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    private float distance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ChasePlayer();
    }

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(transform.position, player.position);
        Debug.Log(distance);

        if (distance <= 1.5f)
        {
            agent.ResetPath();
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

}
