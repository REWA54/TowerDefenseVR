using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Bullet : MonoBehaviour
{
    //[SerializeField] float speed;
    [SerializeField] float Damages;
    [SerializeField] GameObject particlesHit;
    public BulletData Data;

    private void Awake()
    {
        LoadData(Data);
    }
    
    void LoadData(BulletData Data)
    {
        Damages = Data.Damage;
    }

    public void Fire(Transform spawnPoint, Vector3 direction,float speed, float damages, Vector3 TargetPoint)
    {
        transform.position = spawnPoint.position;
        transform.LookAt(TargetPoint);

        Damages *= damages;
        
        GetComponent<Rigidbody>().velocity = direction * speed;
        Destroy(gameObject, 2);
    }
    private void OnTriggerEnter(Collider other)
    {
        GameObject impactParticles = (GameObject) Instantiate(particlesHit);
        impactParticles.transform.position = transform.position;
        impactParticles.transform.LookAt(GetComponent<Rigidbody>().velocity);
        Destroy(impactParticles, 0.5f);
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyManager>().TakeDamages(Damages);
            Destroy(gameObject);
        }
       
    }

}
