using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class EnemyManager : MonoBehaviour
{
    [InspectorButton("OnButtonClicked")]
    public string KILL;
    
    
    public EnemyData Data;
    float lootAmount;
    public float Damage;
    [SerializeField] float Life;
    [SerializeField] GameObject UICanvas;
    [SerializeField] TMP_Text lifeText;
    Transform playerCamera;
    //public GameObject Loot;
    public Image LifeFillAmount;
    public GameObject deathEffect;
    public bool alive;
    public Transform targetPoint;
    LevelManager levelManager;
    int level = 1;
    float maxLife;
    public float DifficultyMultiplicator = 1.2f;

    private void OnButtonClicked()
    {
        Die(true);
    }
    private void Awake()
    {
        LoadData(Data);
        alive = true;
        levelManager = FindObjectOfType<LevelManager>();
        playerCamera = FindObjectOfType<Camera>().transform;
        LifeFillAmount.fillAmount = Life;
        
    }
    private void Start()
    {
        UpdateUI(true);
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
    public void TakeDamages(float hitDamages , bool animate)
    {
        Life -= hitDamages;
        UpdateUI(animate);
        if (Life<=0)
        {
            Die(true);
        }
        
    }

    void UpdateUI(bool punchScale) {
        LifeFillAmount.fillAmount = Life / maxLife;
        lifeText.text = Mathf.Round(Life).ToString();
        if (punchScale)
        {
            lifeText.transform.DOPunchScale(Vector3.one, 0.1f);
           
        }
        
    }
    public void IncreaseDifficulty(int spawnLevel){
        
        // Increase difficulty by level
        level = spawnLevel;
        Damage *= Mathf.Pow(DifficultyMultiplicator,level);
        Life *= Mathf.Pow(DifficultyMultiplicator,level);
        maxLife = Life;
    }

    public void Die(bool isDestroyedByPlayer)
    {
        if (isDestroyedByPlayer)
        {
            levelManager.Loot(lootAmount * Mathf.Pow(DifficultyMultiplicator, level));
            //for (int i = 0; i < lootAmount; i++)
            //{
            //    GameObject LootGO = Instantiate(Loot, transform.position + (Random.Range(0.1f,0.1f)*Vector3.one), Quaternion.identity);
            //    LootGO.transform.DOJump(playerCamera.position - new Vector3(0, playerCamera.position.y, 0), 1f, 1, 1f, false);
            //    Destroy(LootGO, 1f);
            //}           
        }
        else
        {
            levelManager.Loot(0);
        }
        alive = false;
        GameObject DeathEffect = Instantiate(deathEffect, targetPoint.transform.position, Quaternion.identity);
        //Destroy(DeathEffect,1f);
        //gameObject.SetActive(false);
        Destroy(gameObject);
    }

  
}
