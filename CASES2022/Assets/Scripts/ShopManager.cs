using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] GameObject[] towers;
    [SerializeField] Transform spawnPoint;
    public LevelManager LevelManager;
    public ParticleSystem buyParticle;

    public void BuyTower(int tower)
    {
        if (LevelManager.Buy(towers[tower].GetComponent<Tower>().Datas.price))
        {
            Instantiate(towers[tower], spawnPoint);
            buyParticle.Play();
        }
        return;
        

    }
}
