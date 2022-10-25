using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject[] enemyPrefabs;
    [SerializeField] Transform spawnPoint;
    public Vector3[] destinationsPoints;
    public Animation openDoor;
    LevelManager levelManager;
    int SpawnedEnemy = 0;
    int enemysToSpawn;
    int level;
    float spawnCooldown;
    public float spawnRate;
    bool LevelLaunched = false;

    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        spawnRate -= openDoor.GetClip("military_building").length / 2;
        Debug.Log(openDoor.GetClip("military_building").length);
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
                Mathf.RoundToInt( Random.Range(0,enemyPrefabs.Length))
               
                );
        }
    }

    IEnumerator OpenDoor()
    {
        openDoor.Play("military_building");
        Debug.Log("Animation Door played");
        yield return new WaitForSeconds(openDoor.GetClip("military_building").length / 2);
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

    void SpawnEnemy(int prefab)
    {
        StartCoroutine(OpenDoor());
        GameObject EnemyInstanciated = Instantiate(enemyPrefabs[prefab],spawnPoint);
        EnemyManager em = EnemyInstanciated.GetComponent<EnemyManager>();
        //em.LoadData(enemyTypes[Type]);
        em.IncreaseDifficulty(level);

        EnemyInstanciated.GetComponent<EnemyMovement>().destinationsPoints = destinationsPoints;
        levelManager.Enemys.Add(EnemyInstanciated);
        SpawnedEnemy++;
    }

}
