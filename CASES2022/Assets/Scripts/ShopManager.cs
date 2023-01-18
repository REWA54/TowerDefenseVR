using DG.Tweening;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] GameObject[] towers;
    [SerializeField] Transform spawnPoint;
    public LevelManager LevelManager;
    public ParticleSystem buyParticle;
    [SerializeField] bool Tutorial;

    private TutorialManagement tutorialManagement;

    private void Start()
    {
        if (Tutorial)
        {
            tutorialManagement = FindObjectOfType<TutorialManagement>();
        }
    }

    public void BuyTower(int tower)
    {
        if (Tutorial)
        {
            tutorialManagement.TutorialStep("Tower Bought " + tower.ToString());
            Instantiate(towers[tower], spawnPoint).transform.DOPunchScale(Vector3.one * 1.1f, 0.1f);
            buyParticle.Play();
            return;
        }
        if (LevelManager.Buy(towers[tower].GetComponent<Tower>().Datas.price))
        {
            Instantiate(towers[tower], spawnPoint).transform.DOPunchScale(Vector3.one * 1.1f, 0.1f);
            buyParticle.Play();
        }
        return;


    }
}
