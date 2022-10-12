using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class TeleportationActivator : MonoBehaviour
{
    public XRRayInteractor rayInteractor;
    public InputActionProperty rayActivate;

    public Collider leftHandCol;

    // Update is called once per frame
    void Update()
    {
        if (rayActivate.action.ReadValue<float>() > 0.1f )
        {
            rayInteractor.enabled = true;
        }
        else
        {
            rayInteractor.enabled = false;
        }
        
    }

   
}
