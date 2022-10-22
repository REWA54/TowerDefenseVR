using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    public EnemyData Data;
    float lootAmount;
    public float Damage;
    [SerializeField] float Life;
    public Image LifeFillAmount;
    public GameObject deathEffect;
    public bool alive;
    public Transform targetPoint;
    LevelManager levelManager;
    int level = 1;
    float maxLife;
    public float DifficultyMultiplicator = 1.2f;
    
    private void Awake()
    {
        LoadData(Data);
        alive = true;
        levelManager = FindObjectOfType<LevelManager>();
        LifeFillAmount.fillAmount = Life;
    }

    public void LoadData(EnemyData Data)
    {
        Damage = Data.Damage;
        Life = Data.Life;
        maxLife = Data.Life;
        lootAmount = Data.lootMoney;
        GetComponent<EnemyMovement>().speed = Data.Speed;
    }

    public void TakeDamages(float hitDamages)
    {
        Life -= hitDamages;
        LifeFillAmount.fillAmount = Life/ maxLife;
        if (Life<=0)
        {
            Die();
        }
        
    }
    public void IncreaseDifficulty(int spawnLevel){
        // Increase difficulty by level
        level = spawnLevel;
        Damage *= Mathf.Pow(DifficultyMultiplicator,level);
        Life *= Mathf.Pow(DifficultyMultiplicator,level);
    }

    void Die()
    {
        levelManager.Loot(lootAmount*Mathf.Pow(DifficultyMultiplicator,level));
        alive = false;
        GameObject DeathEffect = (GameObject) Instantiate(deathEffect,transform);
        Destroy(DeathEffect,0.1f);
        Destroy(gameObject);
    }
}
