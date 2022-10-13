using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PlacePointSystem : MonoBehaviour
{
    bool asATower;

    public bool AsATower()
    {
        return asATower;
    }
    public void TowerPlacing(SelectEnterEventArgs args)
    {
        if (args.interactableObject.transform.gameObject.CompareTag("Tower"))
        {
            args.interactableObject.transform.gameObject.GetComponent<Tower>().Placement(true);
        }
    }
    public void TowerRemove(SelectExitEventArgs args)
    {
        if (args.interactableObject.transform.gameObject.CompareTag("Tower"))
        {
            args.interactableObject.transform.gameObject.GetComponent<Tower>().Placement(false);
        }
    }
}
