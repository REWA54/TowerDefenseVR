using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Home : MonoBehaviour
{
    public float Life;

    private void OnTriggerEnter(Collider other)
    {
       
        if (other.CompareTag("Enemy"))
        {
           
            TakeDamages( other.GetComponent<EnemyManager>().Damage);
            Destroy(other.gameObject);
        }
    }

    void TakeDamages(float Damages)
    {
        Life -= Damages;
        if (Life <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
