using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public GameObject Home;
    public List<GameObject> Enemys;
    public int level;
    [SerializeField] EnemySpawner enemySpawner;
    [SerializeField] TMP_Text moneyUI;
    float money;
    // Start is called before the first frame update
    void Start()
    {
        money = 100f;
        UpdateUI();
        
    }
    public void LevelStart()
    {
        enemySpawner.StartSpawn();
    }

    private void UpdateUI()
    {
        moneyUI.text = money.ToString();
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
