using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    public EnemyData Data;
    public float Damage;
    [SerializeField] float Life;
    public bool alive;
    public Transform targetPoint;

    private void Start()
    {
        LoadData(Data);
        alive = true;
    }

    public void LoadData(EnemyData Data)
    {
        Damage = Data.Damage;
        Life = Data.Life;
        GetComponent<NavMeshAgent>().speed = Data.Speed;
    }

    public void TakeDamages(float hitDamages)
    {
        Life -= hitDamages;
        if (Life<=0)
        {
            Die();
        }
    }

    void Die()
    {
        alive = false;
        gameObject.SetActive(false);
    }
}
