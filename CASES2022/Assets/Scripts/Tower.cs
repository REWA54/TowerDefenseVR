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
    float bulletSpeed;
    float range;
    float damagesMultiplicator;
    float shootCooldown;
    float shootingRate;
    GameObject TargetGO;

    bool isPlaced = false;
    private void Awake()
    {
        LoadData(Datas);
    }
    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        value = price;
        shootCooldown = 0f;
    }
    void LoadData(TowerData Data)
    {
        range = Data.range;
        shootingRate = Data.shootingRate;
        damagesMultiplicator = Data.damagesMultiplicator;
        bulletSpeed = Data.bulletVelocity;
        price = Data.price;
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
        
        if (isPlaced)
        {
            FindEnemy();
            if (TargetGO == null)
            {
                return;
            }
            MoveTower(TargetGO.transform.position);
            switch (TowerType)
            {
                case TypeSelection.Canon:
                    CanonShoot();
                    break;
                case TypeSelection.Rail:
                    break;
                case TypeSelection.Missile:
                    MissileLaunch();
                    break;
            }
        }
       
    }
    public void Upgrade()
    {
        shootingRate /= upgradeMultiplicator;
        bulletSpeed *= upgradeMultiplicator;
        range *= upgradeMultiplicator;
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
    void FindEnemy() {

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
