using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public GameObject Home;
    public SaveSystem saveSystem;
    public Map[] maps;
    public List<GameObject> Enemys;
    public int level = 0;
    [SerializeField] EnemySpawner enemySpawner;
    [SerializeField] TMP_Text[] moneyUI;
    [SerializeField] GameObject[] levelStartButtons;
    public TMP_Text levelIndicator;
    float money;
    int enemysThisWave = 0;
    int enemysKilledThisWave = 0;
    GameObject map;

    [Space]
    [Header("Finish Canvas")]
    [SerializeField] GameObject EndCanvas;
    [SerializeField] TMP_Text EndScoreUI;
    [SerializeField] TMP_Text HighestScoreUI;
    void Start()
    {
        money = 100f;
        UpdateUI();
        EndCanvas.SetActive(false);
        LoadMap(0);
        // LevelStart();
        foreach (var item in levelStartButtons)
        {
            item.SetActive(true);
        }
    }
    public void LevelEnd()
    {
        Destroy(map);
        EndCanvas.SetActive(true);
        EndScoreUI.text = level.ToString();
        if (saveSystem.highScore > level)
        {
            HighestScoreUI.text = saveSystem.highScore.ToString();
            return;
        }
        HighestScoreUI.text = level.ToString();
    }
    [ContextMenu("Start Level")]
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
        foreach (var item in levelStartButtons)
        {
            item.SetActive(false);
        }
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
        if (CheckWaveEnd())
        {
            foreach (var item in levelStartButtons)
            {
                item.SetActive(true);
            }
        }
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
