using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolSystem : MonoBehaviour
{
    public Transform[] waypoints;
    public float patrolSpeed = 3f;
    public float waypointTolerance = 1f;
    public bool loop = true;

    private int currentWaypoint = 0;
    private NavMeshAgent agent;
    private bool isPatrollingForward = true;

    public Transform player;

    public bool canAttack = false;
    public float attackDistance = 1f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = patrolSpeed;
        SetNextWaypoint();
        player = FindObjectOfType<PlayerMovement>().transform;
    }

   public void Patrol()
    {
        if (agent.remainingDistance < waypointTolerance)
        {
            SetNextWaypoint();
        }
    }

    void SetNextWaypoint()
    {
        if (waypoints.Length == 0)
        {
            return;
        }

        if (currentWaypoint >= waypoints.Length)
        {
            if (loop)
            {
                currentWaypoint = 0;
            }
            else
            {
                return;
            }
        }

        agent.SetDestination(waypoints[currentWaypoint].position);
        currentWaypoint += isPatrollingForward ? 1 : -1;
    }

    public void ReversePatrolDirection()
    {
        isPatrollingForward = !isPatrollingForward;
    }
}