using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tower : MonoBehaviour
{
    public enum TypeSelection { Canon, Rail, Missile }
    public TypeSelection TowerType;
    LevelManager levelManager;

    [Header("Prefabs")]
    public GameObject BulletPrefab;
    public GameObject MissilePrefab;
    [SerializeField] TMP_Text[] levelDisplays;

    [Header("Transforms")]
    [SerializeField] Transform SpawnPointBullet;
    [SerializeField] Transform TowerRotationPoint;
    [SerializeField] Transform Canon;

    [Header("Tower Stats")]
    public TowerData Datas;
    public int level;
    public float price;
    public float value;
    public float upgradeMultiplicator;
    public float upgradePrice;
    float bulletSpeed;
    float range;
    float damagesMultiplicator;
    float shootCooldown;
    float shootingRate;
    LineRenderer railLineRenderer;
    Light railHitLight;
    ParticleSystem railHitParticles;
    GameObject TargetGO;
    GameObject enemyAimed;

    bool isPlaced = false;
    private void Awake()
    {
        LoadData(Datas);
    }
    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        value = price;
        upgradePrice = 0.2f * value;
        shootCooldown = 0f;
        InvokeRepeating("FindEnemy", 0f, 0.5f);
    }
    void LoadData(TowerData Data)
    {
        range = Data.range;
        damagesMultiplicator = Data.damagesMultiplicator;
        price = Data.price;
        if (TowerType == TypeSelection.Rail){
            Debug.Log("I Load rail DATA");
            railLineRenderer = gameObject.GetComponent<RailTowerPrefabs>().lineRenderer;
            railHitLight = gameObject.GetComponent<RailTowerPrefabs>().light;
            railHitParticles = gameObject.GetComponent<RailTowerPrefabs>().particleSystem;
            return;
        }        
        shootingRate = Data.shootingRate;        
        bulletSpeed = Data.bulletVelocity;        
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
    public void Placement(bool state)
    {
        isPlaced = state;
        if (state)
        {
            transform.localScale *= 5;
        }
        else
        {
            transform.localScale /= 5;
        }
        
    }
    void Update()
    {
            //FindEnemy();
            if (TargetGO == null)
            {
                if (TowerType == TypeSelection.Rail)
                {
                    railLineRenderer.enabled = false;
                    railHitParticles.Stop();
                    railHitLight.enabled = false;
                }               
                return;
            }
            MoveTower(TargetGO.transform.position);
            switch (TowerType)
            {
                case TypeSelection.Canon:
                    CanonShoot();
                    break;
                case TypeSelection.Rail:
                    LaserRail();
                    break;
                case TypeSelection.Missile:
                    MissileLaunch();
                    break;
            }
    }
    public void Upgrade()
    {
        shootingRate /= upgradeMultiplicator;
        bulletSpeed *= upgradeMultiplicator;
        range *= upgradeMultiplicator;
        value += upgradePrice;
        upgradePrice = 0.2f * value;
        level++;
        UpdateUI();
    }
    void UpdateUI()
    {
        foreach (var item in levelDisplays)
        {
            item.text = level.ToString();
        }
    }
    void CanonShoot()
    {
        if (shootCooldown > 0)
        {
            shootCooldown -= Time.deltaTime;
            return;
        }
        if (TargetGO == null)
        {
            return;
        }
        shootCooldown = shootingRate;
        Shoot();
    }
    void MissileLaunch()
    {
        if (shootCooldown > 0)
        {
            shootCooldown -= Time.deltaTime;
            return;
        }
        if (TargetGO == null)
        {
            return;
        }
        shootCooldown = shootingRate;
        Launch();
    }
    void Launch()
    {
        GameObject Missile = Instantiate(MissilePrefab);
        Missile.GetComponent<Bullet>().Fire(SpawnPointBullet, SpawnPointBullet.forward, bulletSpeed, damagesMultiplicator,TargetGO.transform.position);
    }
    void LaserRail()
    {
       EnemyManager enemyManager = enemyAimed.GetComponent<EnemyManager>();
       enemyManager.TakeDamages(damagesMultiplicator * Time.deltaTime);

       if(railLineRenderer.enabled == false){
            railLineRenderer.enabled = true;
			railHitParticles.Play();
			railHitLight.enabled = true;
       }

        railLineRenderer.SetPosition(0,SpawnPointBullet.position);
        railLineRenderer.SetPosition(1,TargetGO.transform.position);

        Vector3 direction = SpawnPointBullet.position - TargetGO.transform.position;
       railHitParticles.transform.position = TargetGO.transform.position;
       railHitParticles.transform.rotation = Quaternion.LookRotation(direction);

    }
    void FindEnemy() 
    {
         if (!isPlaced)
        {
            return;
        }

        TargetGO = null;
        float mindistance = float.PositiveInfinity;

        foreach (GameObject enemy in levelManager.Enemys)
        {
            if (enemy != null && enemy.GetComponent<EnemyManager>().alive)
            {
                Vector3 posEnemy = enemy.transform.position;

                float distanceFromTower = Vector3.Distance(posEnemy, transform.position);
                if (distanceFromTower < mindistance && distanceFromTower < range)
                {
                    mindistance = distanceFromTower;
                    enemyAimed = enemy;
                    TargetGO = enemy.GetComponent<EnemyManager>().targetPoint.gameObject;
                }
            }           
        }
    }
    void MoveTower(Vector3 EnemyTarget)
    {
        Vector3 LookPosition = new Vector3(EnemyTarget.x, TowerRotationPoint.position.y, EnemyTarget.z);
        TowerRotationPoint.LookAt(LookPosition);
        Canon.LookAt(EnemyTarget);
    }
    void Shoot()
    {
        GameObject Bullet = Instantiate(BulletPrefab);
        Bullet.GetComponent<Bullet>().Fire(SpawnPointBullet, SpawnPointBullet.forward, bulletSpeed, damagesMultiplicator, TargetGO.transform.position);
       
    }
}
