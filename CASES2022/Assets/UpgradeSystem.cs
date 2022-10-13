using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class UpgradeSystem : MonoBehaviour
{
    GameObject thingToUpgrade;


    public void Upgrade()
    {
        if (thingToUpgrade == null)
        {
            return;
        }
        switch (thingToUpgrade.tag)
        {
            case ("Weapon"):
                thingToUpgrade.GetComponent<Pistol>().Upgrade();
                break;
            case ("Tower"):
                thingToUpgrade.GetComponent<Tower>().Upgrade();
                break;
        }
    }

    public void ChooseThingToUprade(SelectEnterEventArgs args)
    {
        thingToUpgrade = args.interactableObject.transform.gameObject;
    }
}
