using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class AttachPointBothHand : MonoBehaviour
{
    public Transform AttachPointRightHand;
    public Transform AttachPointLeftHand;
    XRGrabInteractable grabInteractable;
    bool isGrabbed;

    // Start is called before the first frame update
    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isGrabbed)
        {
            isGrabbed = true;

            if (other.tag == "RightHand")
            {
                ChooseGoodAttachPoint(AttachPointRightHand);
            }
            else if (other.tag == "LeftHand")
            {
                ChooseGoodAttachPoint(AttachPointLeftHand);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        isGrabbed = false;
    }

    public void ChooseGoodAttachPoint(Transform AttachementPoint)
    {
        grabInteractable.attachTransform = AttachementPoint;
    }
}
