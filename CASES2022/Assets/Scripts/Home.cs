using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class Home : MonoBehaviour
{
    public float Life;
    public float CurrentLife;
    LevelManager levelManager;
    public GameObject homeGameObject;
    public GameObject UIHome;
    public Image LifeFillAmount;


    private void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        CurrentLife = Life;
        UpdateUI();
        
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyManager enemyManager = other.GetComponent<EnemyManager>();
            TakeDamages( enemyManager.Damage);
            enemyManager.Die(false);
        }
    }
    private void Update()
    {
        UIHome.transform.LookAt(Camera.main.transform);
    }
    void TakeDamages(float Damages)
    {
        CurrentLife -= Damages;
        if (CurrentLife <= 0)
        {
            Die();
        }
        homeGameObject.transform.DOPunchScale((Vector3.one - Vector3.up) * 0.1f, 0.1f);
        UpdateUI();
    }
    void UpdateUI()
    {
        UIHome.GetComponentInChildren<TextMeshProUGUI>().text = CurrentLife.ToString();
        LifeFillAmount.fillAmount = CurrentLife / Life;

    }
    void Die()
    {
        levelManager.LevelEnd();
        Destroy(gameObject);
    }
}
