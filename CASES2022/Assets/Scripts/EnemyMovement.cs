using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public Transform Destination;
    public Transform[] destinationsPoints;
    int destinationIndex = 0;
    
    public float speed;
   
    void Start()
    {
        Destination = destinationsPoints[0];
    }
    
    void Drive()
    {      
        if (Vector3.Distance(transform.position,Destination.position)<0.1f)
        {
            destinationIndex++;
            Destination = destinationsPoints[destinationIndex];
        }
        transform.position = Vector3.MoveTowards(transform.position, Destination.position, speed * Time.deltaTime);
    }


       
        private void Update()
    {
        transform.LookAt(Destination);
        Drive();       
    }
}
