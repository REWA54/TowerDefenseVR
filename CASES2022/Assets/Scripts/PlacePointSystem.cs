using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PlacePointSystem : MonoBehaviour
{
    bool asATower;
    public ParticleSystem placeParticles;
    public bool Tutorial;
    public bool AsATower()
    {
        return asATower;
    }
    public void TowerPlacing(SelectEnterEventArgs args)
    {
        Debug.Log("Tower placed");
        if (args.interactableObject.transform.gameObject.CompareTag("Tower"))
        {
            args.interactableObject.transform.gameObject.GetComponent<Tower>().Placement(true);
            placeParticles.Play();
        }
        if (Tutorial)
        {
            TutorialManagement tutorialManagement = FindObjectOfType<TutorialManagement>();
            tutorialManagement.TutorialStep("Tower placed " +args.interactableObject.transform.gameObject.GetComponent<Tower>().TowerType);
            if (args.interactableObject.transform.gameObject.GetComponent<Tower>().isUpgraded)
            {
                tutorialManagement.TutorialStep("Tower upgraded placed ");
            }
        }
    }
    public void TowerRemove(SelectExitEventArgs args)
    {
        Debug.Log("Tower removed");
        if (args.interactableObject.transform.gameObject.CompareTag("Tower"))
        {
            args.interactableObject.transform.gameObject.GetComponent<Tower>().Placement(false);
        }
    }
}
