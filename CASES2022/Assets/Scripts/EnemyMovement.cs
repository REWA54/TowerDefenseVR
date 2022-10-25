using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public Vector3 Destination;
    public Vector3[] destinationsPoints;
    int destinationIndex = 0;
    
    public float speed;
   
    void Start()
    {
        Destination = destinationsPoints[0];
    }
    
    void Translate()
    {      
        if (Vector3.Distance(transform.position,Destination)<0.1f)
        {
            destinationIndex++;
            Destination = destinationsPoints[destinationIndex];
        }
        // transform.position = Vector3.MoveTowards(transform.position, Destination, speed * Time.deltaTime);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }


       
        private void Update()
    {
        transform.LookAt(Destination);
        Translate();       
    }
}
