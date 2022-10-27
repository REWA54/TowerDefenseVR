using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class EnemyManager : MonoBehaviour
{
    public EnemyData Data;
    float lootAmount;
    public float Damage;
    [SerializeField] float Life;
    [SerializeField] GameObject UICanvas;
    [SerializeField] TMP_Text lifeText;
    Transform playerCamera;
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
        playerCamera = FindObjectOfType<Camera>().transform;
        LifeFillAmount.fillAmount = Life;
        UpdateUI();
    }

    public void LoadData(EnemyData Data)
    {
        Damage = Data.Damage;
        Life = Data.Life;
        maxLife = Data.Life;
        lootAmount = Data.lootMoney;
        GetComponent<EnemyMovement>().speed = Data.Speed;
        
    }
    void Update()
    {
        UICanvas.gameObject.transform.LookAt(playerCamera.transform);
    }
    public void TakeDamages(float hitDamages)
    {
        Life -= hitDamages;
        UpdateUI();
        if (Life<=0)
        {
            Die();
        }
        
    }

    void UpdateUI() {
        LifeFillAmount.fillAmount = Life / maxLife;
        lifeText.text = Mathf.Round(Life).ToString();
        lifeText.transform.DOPunchScale(Vector3.one*1.1f, 0.01f);
    }
    public void IncreaseDifficulty(int spawnLevel){
        
        // Increase difficulty by level
        level = spawnLevel;
        Damage *= Mathf.Pow(DifficultyMultiplicator,level);
        Life *= Mathf.Pow(DifficultyMultiplicator,level);
        maxLife = Life;
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
