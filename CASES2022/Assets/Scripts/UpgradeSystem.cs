using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;
using DG.Tweening;

public class UpgradeSystem : MonoBehaviour
{
    [SerializeField] bool Tutorial;
    GameObject thingToUpgrade;
    public LevelManager levelManager;
    public ParticleSystem upgradeParticles;
    public TMP_Text priceUI;
    public float upgradePrice;
    private TutorialManagement tutorialManagement;

    private void Start()
    {
        if (Tutorial)
        {
            tutorialManagement = FindObjectOfType<TutorialManagement>();
        }
    }
    public void Upgrade()
    {
        if (thingToUpgrade == null)
        {
            return;
        }
        if (Tutorial)
        {
            tutorialManagement.ThingUpgraded(thingToUpgrade.tag);
        }
        switch (thingToUpgrade.tag)
        {
            case ("Weapon"):
                if (levelManager.Buy(upgradePrice))
                {
                    thingToUpgrade.GetComponent<Pistol>().Upgrade();
                }
                break;
            case ("Tower"):
                if (levelManager.Buy(upgradePrice))
                {
                    thingToUpgrade.GetComponent<Tower>().Upgrade();
                }
                break;
        }
        ChangeObjectToUpgrade();
        thingToUpgrade.gameObject.transform.DOPunchScale(Vector3.one * 1.1f, 0.1f);
        upgradeParticles.Play();
    }

    public void ChooseThingToUprade(SelectEnterEventArgs args)
    {
        thingToUpgrade = args.interactableObject.transform.gameObject;
        ChangeObjectToUpgrade();
    }

    void ChangeObjectToUpgrade()
    {
        switch (thingToUpgrade.tag)
        {
            case ("Weapon"):
                upgradePrice = Mathf.Round(thingToUpgrade.GetComponent<Pistol>().upgradePrice);
               
                break;
            case ("Tower"):
                upgradePrice = Mathf.Round(thingToUpgrade.GetComponent<Tower>().upgradePrice);
                break;
        }

        priceUI.text = Mathf.Round(upgradePrice).ToString();
    }
}
