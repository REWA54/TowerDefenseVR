using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public Transform Destination;
    NavMeshAgent NavMeshAgent;
    public bool isFlying;
    public float speed;
   
    void Start()
    {
        // Get the NavMeshAgent component and set the destination
        NavMeshAgent = GetComponent<NavMeshAgent>();
        NavMeshAgent.speed = speed;
        if (Destination != null)
        {
            SetDestination(Destination);
        }
    }
    public void SetDestination(Transform Target)
    {
        // set the destination of the navmesh agent if the enemy is not flying
        if(isFlying)
        {
            NavMeshAgent.enabled = false;
            return;
        }
            NavMeshAgent.enabled = true;
            NavMeshAgent.SetDestination(Target.position);
    }
    private void Update()
    {
        // if the enemy is flying, move it towards the destination and rotate it to look at the destination
        transform.LookAt(Destination);
        if(isFlying)
        {
            transform.position = Vector3.MoveTowards(transform.position, Destination.position, speed * Time.deltaTime);
        }
    }
}
