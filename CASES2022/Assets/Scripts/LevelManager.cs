using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject Home;
    public List<GameObject> Enemys;
    public int level;
    [SerializeField] EnemySpawner enemySpawner;
    float money;
    // Start is called before the first frame update
    void Start()
    {
        money = 100f;
    }
    public void LevelStart()
    {
        enemySpawner.StartSpawn();
    }

    public bool Buy(float price)
    {
        if (money >= price)
        {
            money -= price;
            return true;
        }
        return false;
    }
}
