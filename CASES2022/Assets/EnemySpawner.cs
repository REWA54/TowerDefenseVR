using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] EnemyData[] enemyTypes;
    [SerializeField] GameObject[] enemyPrefabs;
    [SerializeField] Transform spawnPoint;
    LevelManager levelManager;
    [SerializeField] Transform enemyTarget;
    public int EnemyQuantity;
    int SpawnedEnemy = 0;
    int level;
    float spawnCooldown;
    public float spawnRate;

    bool LevelLaunched = false;


    // Start is called before the first frame update
    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        level = levelManager.level;
    }

    public void StartSpawn()
    {
        LevelLaunched = true;
    }

    private void Update()
    {
        if (SpawnedEnemy <= EnemyQuantity && SpawnCooldown(spawnRate) && LevelLaunched)
        {
            SpawnEnemy(0);
        }
    }
    bool SpawnCooldown(float spawnRateUpdate)
    {
        if (spawnCooldown > 0)
        {
            spawnCooldown -= Time.deltaTime;
            return false;
        }
        spawnCooldown = spawnRateUpdate;

        return true;
    }

    void SpawnEnemy(int Type)
    {
        GameObject EnemyInstanciated = (GameObject) Instantiate(enemyPrefabs[Type],spawnPoint);
        EnemyInstanciated.GetComponent<EnemyManager>().LoadData(enemyTypes[Type]);
        EnemyInstanciated.GetComponent<EnemyMovement>().Destination = enemyTarget;
        levelManager.Enemys.Add(EnemyInstanciated);
        SpawnedEnemy++;
    }

}
