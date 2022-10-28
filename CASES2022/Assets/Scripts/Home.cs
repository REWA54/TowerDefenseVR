using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Home : MonoBehaviour
{
    public float Life;
    LevelManager levelManager;
    public GameObject homeGameObject;

    private void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

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
        homeGameObject.transform.DOPunchScale(Vector3.one * 0.2f, 0.1f);
    }

    void Die()
    {
        levelManager.LevelEnd();
        Destroy(gameObject);
    }
}
