// Combat Prototype
// Irina Mishina
// 2026-03-24
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

public class BatEnemy : MonoBehaviour
{
    //public WaypointHolder waypointHolder;


    public Transform player;
    public float speed = 10f;
    private float rotationSpeed = 7.5f;
    private float circleDuration = 5f;
    private float attackDuration = 10f;

    private float waypointDistanceThreshold = 2f;

    private Transform currentWaypointTarget;
    public GameObject[] waypoints;

    public GameObject projectile;
    public Transform startLocation;
    private bool isAttacking = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        waypoints = GameObject.FindGameObjectsWithTag("waypoint");//finds all waypoints

        StartCoroutine(StateMachine());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator StateMachine()//calls the two states: attack and patrol
    {
        while (true)
        {
            yield return StartCoroutine(Patrol(circleDuration));
            yield return StartCoroutine(FireProjectile(attackDuration));
        }
    }


    private void PickRandomPoint()//picks random point based on the array of waypoints
    {
        if (waypoints != null && waypoints.Length > 0)
        {
            currentWaypointTarget = waypoints[Random.Range(0, waypoints.Length)].transform;

        }
        MoveTowardsTarget(currentWaypointTarget.position);
    }

    private bool ReachWayPoint()//checks if the enemy reached the target point
    {
        if (!currentWaypointTarget)
        {
            return false;
        }

        return Vector3.Distance(transform.position, currentWaypointTarget.position) < waypointDistanceThreshold;
    }

    private void MoveTowardsTarget(Vector3 targetPos)//moves toward designated target
    {
        Vector3 dir = targetPos - transform.position;

        dir.Normalize();
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * rotationSpeed);

        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

    }

    private IEnumerator Patrol(float duration)// controls patroling options if it finished
    {
        if(!isAttacking)
        {
            float timer = 0f;
            PickRandomPoint();
            while (timer < duration)
            {
                timer += Time.deltaTime;

                if (currentWaypointTarget)
                {
                    MoveTowardsTarget(currentWaypointTarget.position);
                }

                if (ReachWayPoint())
                {
                    PickRandomPoint();
                }

                yield return null;
            }
        }
        
    }

    private IEnumerator FireProjectile(float duration)//fires a projectile (instantiates)
    {
        isAttacking = true;
        Instantiate(projectile, startLocation.position, Quaternion.identity);
        yield return new WaitForSeconds(2.0f);
        isAttacking = false;
    }
}
