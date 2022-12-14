using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelManager : MonoBehaviour
{
    [InspectorButton("OnButtonClicked")]
    public string START;
    
    public GameObject Home;
    public SaveSystem saveSystem;
    public Map[] maps;
    public List<GameObject> Enemys;
    public int level = 0;
    [SerializeField] EnemySpawner enemySpawner;
    [SerializeField] TMP_Text[] moneyUI;
    public TMP_Text levelIndicator;
    float money;
    int enemysThisWave = 0;
    int enemysKilledThisWave =0;
    GameObject map;
    void Start()
    {
        money = 100f;
        UpdateUI();
        LoadMap(0);
       // LevelStart();
    }
    public void LevelEnd()
    {
        RestartScene();
    }
    private void OnButtonClicked()
    {
        LevelStart();
    }

    public void LevelStart()
    {
        
        if (!CheckWaveEnd() || map == null)
        {
            return;
        }
        saveSystem.SaveIfNeeded(level);
        level++;
        Enemys.Clear();
        enemysThisWave = 0;
        enemysKilledThisWave = 0;
        enemysThisWave = enemySpawner.StartSpawn(level);
        UpdateUI();
    }
    public void LoadMap(int index)
    {
       
        if (!CheckWaveEnd())
        {
            return;
        }
        if (map != null)
        {
            Destroy(map);
        }
        map = Instantiate(maps[index].GO, Vector3.zero, Quaternion.identity);
        enemySpawner.destinationsPoints = maps[index].destinationsPoints;
    }
    private void UpdateUI()
    {
        foreach (TMP_Text text in moneyUI)
        {
            text.text = Mathf.RoundToInt(money).ToString();
        }
       // moneyUI.text = money.ToString();
        levelIndicator.text = "LEVEL " + level.ToString();
    }

    public void Loot(float lootAmount)
    {
        money += lootAmount;
        
        enemysKilledThisWave++;
        UpdateUI();
    }

    bool CheckWaveEnd()
    {
        if (enemysThisWave > enemysKilledThisWave)
        {
            return false;
        }
        return true;
        // check if all enemys are dead
    }
    
    public void Refund(float cost)
    {
        money += cost;
        UpdateUI();
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

        //Add a check to see if the player has enough money to buy the tower
        //If they do, subtract the price from their money and return true
        //If they don't, return false

    }

    public void RestartScene()
    {
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
