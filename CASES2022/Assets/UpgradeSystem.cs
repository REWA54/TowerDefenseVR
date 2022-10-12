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
        Debug.Log("I've something to upgrade");
        switch (thingToUpgrade.tag)
        {
            case ("Weapon"):
                Debug.Log("I've a weapon to upgrade");
                thingToUpgrade.GetComponent<Pistol>().Upgrade();
                break;
            case ("Tower"):
                thingToUpgrade.GetComponent<Tower>().Upgrade();
                break;
        }
    }

    public void ChooseThingToUprade(SelectEnterEventArgs args)
    {
        thingToUpgrade = args.interactable.gameObject;
    }
}
