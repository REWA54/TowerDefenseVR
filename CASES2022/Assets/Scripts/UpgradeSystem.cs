using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class UpgradeSystem : MonoBehaviour
{
    GameObject thingToUpgrade;
    public LevelManager levelManager;
    public ParticleSystem upgradeParticles;
    public TMP_Text priceUI;
    public float upgradePrice;
    public void Upgrade()
    {
        if (thingToUpgrade == null)
        {
            return;
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
