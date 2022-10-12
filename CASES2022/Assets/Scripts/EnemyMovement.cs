using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public Transform Destination;
    NavMeshAgent NavMeshAgent;
   
    void Start()
    {
        NavMeshAgent = GetComponent<NavMeshAgent>();
        if (Destination != null)
        {
            SetDestination(Destination);
        }
    }

    public void SetDestination(Transform Target)
    {
        NavMeshAgent.SetDestination(Target.position);
    }

    private void Update()
    {
        transform.LookAt(Destination);
    }

}
