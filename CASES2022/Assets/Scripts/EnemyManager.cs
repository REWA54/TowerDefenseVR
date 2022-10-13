using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    public EnemyData Data;
    float lootAmount;
    public float Damage;
    [SerializeField] float Life;
    public GameObject deathEffect;
    public bool alive;
    public Transform targetPoint;
    LevelManager levelManager;
    private void Start()
    {
        LoadData(Data);
        alive = true;
        levelManager = FindObjectOfType<LevelManager>();
    }

    public void LoadData(EnemyData Data)
    {
        Damage = Data.Damage;
        Life = Data.Life;
        lootAmount = Data.lootMoney;
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
        levelManager.Loot(lootAmount);
        alive = false;
        GameObject DeathEffect = (GameObject) Instantiate(deathEffect,transform);
        Destroy(DeathEffect,0.1f);
        Destroy(gameObject);
    }
}
