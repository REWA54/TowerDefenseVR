using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public GameObject Home;
    public List<GameObject> Enemys;
    public int level = 0;
    [SerializeField] EnemySpawner enemySpawner;
    [SerializeField] TMP_Text moneyUI;
    public TMP_Text levelIndicator;
    float money;
    int enemysThisWave = 0;
    int enemysKilledThisWave =0;
    void Start()
    {
        money = 100f;
        UpdateUI();        
    }
    public void LevelEnd()
    {
        Debug.Log("Level ended"); ;
    }
    public void LevelStart()
    {
        if (!CheckWaveEnd())
        {
            return;
        }
        Enemys.Clear();
        enemysThisWave = 0;
        enemysKilledThisWave = 0;
        enemysThisWave = enemySpawner.StartSpawn(level);
        UpdateUI();
    }

    private void UpdateUI()
    {
        moneyUI.text = money.ToString();
        levelIndicator.text = "LEVEL " + level.ToString();
    }

    public void Loot(float lootAmount)
    {
        money += lootAmount;
        
        enemysKilledThisWave++;
        if (CheckWaveEnd())
        {
            level++;
        }
        UpdateUI();
    }

    bool CheckWaveEnd()
    {
        if (enemysThisWave > enemysKilledThisWave)
        {
            return false;
        }
        return true;
    }
    public bool Buy(float price)
    {
        if (money >= price)
        {
            money -= price;
            UpdateUI();
            return true;
        }
        UpdateUI();
        return false;
    }
}
