using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] EnemyData[] enemyTypes;
    [SerializeField] GameObject[] enemyPrefabs;
    [SerializeField] Transform spawnPoint;
    [SerializeField] Transform[] destinationsPoints;
    LevelManager levelManager;
    [SerializeField] Transform enemyTarget;
    int SpawnedEnemy = 0;
    int enemysToSpawn;
    int level;
    float spawnCooldown;
    public float spawnRate;
    bool LevelLaunched = false;

    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    public int StartSpawn(int actualLevel)
    {        
        level = actualLevel;
        enemysToSpawn = Mathf.RoundToInt(Random.Range(level, level*1.2f));
        SpawnedEnemy = 0;
        LevelLaunched = true;
        return enemysToSpawn;
    }

    private void Update()
    {
        if (SpawnedEnemy <= enemysToSpawn  && SpawnCooldown(spawnRate) && LevelLaunched)
        {
            SpawnEnemy(
                Mathf.RoundToInt( Random.Range(0,enemyPrefabs.Length)),
                Mathf.RoundToInt( Random.Range(0,enemyTypes.Length))
                );
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

    void SpawnEnemy(int prefab, int Type)
    {
        GameObject EnemyInstanciated = (GameObject) Instantiate(enemyPrefabs[prefab],spawnPoint);

        EnemyManager em = EnemyInstanciated.GetComponent<EnemyManager>();
        em.LoadData(enemyTypes[Type]);
        em.IncreaseDifficulty(level);

        EnemyInstanciated.GetComponent<EnemyMovement>().destinationsPoints = destinationsPoints;
        levelManager.Enemys.Add(EnemyInstanciated);
        SpawnedEnemy++;
    }

}
