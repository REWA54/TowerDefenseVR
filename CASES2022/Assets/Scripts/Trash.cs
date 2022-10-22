using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Trash : MonoBehaviour
{
    public LevelManager levelManager;

    public void Recycle(HoverEnterEventArgs args)
    {
        GameObject go = args.interactableObject.transform.gameObject;
        if (go.CompareTag("Tower"))
        {
            levelManager.Refund(go.GetComponent<Tower>().value);
            Destroy(go);
        }
    }
}
