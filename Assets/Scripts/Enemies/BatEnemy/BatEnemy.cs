using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

public class BatEnemy : MonoBehaviour
{
    //public WaypointHolder waypointHolder;
    public Transform player;
    public float speed = 20f;
    private float rotationSpeed = 7.5f;
    private float circleDuration = 5f;
    private float waypointDistanceThreshold = 2f;

    private Transform currentWaypointTarget;
    public Transform[] waypoints;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //if(waypointHolder != null)
        //{
         //   waypointHolder.RefreshWaypoints();
           // waypoints = waypointHolder.Waypoints;
        //}

        if(waypoints == null || waypoints.Length == 0)
        {
            StartCoroutine(StateMachine());
        }

        StartCoroutine(StateMachine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator StateMachine()
    {
        while (true)
        {
            yield return StartCoroutine(Patrol(circleDuration));
        }
    }

    private void FaceTarget(Vector3 targetPos)
    {
        Vector3 dir = targetPos - transform.position;
        if (dir.sqrMagnitude < 0.0001f) return;

        dir.Normalize();
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * rotationSpeed);
    }

    private void PickRandomPoint()
    {
        if(waypoints != null && waypoints.Length > 0)
        {
            currentWaypointTarget = waypoints[Random.Range(0, waypoints.Length)];

            Debug.Log("picked point");
        }
        MoveTowardsTarget(currentWaypointTarget.position);
    }

    private bool ReachWayPoint()
    {
        if (!currentWaypointTarget)
        {
            return false;
        }

        return Vector3.Distance(transform.position, currentWaypointTarget.position) < waypointDistanceThreshold;
    }

    private void MoveTowardsTarget(Vector3 targetPos)
    {
        Vector3 dir = targetPos - transform.position;
        //if (dir.sqrMagnitude < 0.0001f) return;

        dir.Normalize();
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * rotationSpeed);

        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

        Debug.Log("moving");
    }

    private IEnumerator Patrol(float duration)
    {
        float timer = 0f;
        PickRandomPoint();
        while (timer < duration)
        {
            timer += Time.deltaTime;

            if(currentWaypointTarget)
            {
                MoveTowardsTarget(currentWaypointTarget.position);
            }

            if(ReachWayPoint())
            {
                PickRandomPoint();
            }

            yield return null;
        }
    }
}
