using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Liminal.SDK.VR;
using Liminal.SDK.VR.Input;

public class SetHandTrigger : MonoBehaviour
{
    public GameObject rightHand;  //PrimaryHand
    public GameObject leftHand; // SecondaryHand
    public IVRInputDevice primaryInput, secondaryInput;
    public Bow bow;
    


    private void Start()
    {
        //bowCollider = GetComponent<Collider>();
        //primaryInput = VRDevice.Device.PrimaryInputDevice;
        //secondaryInput = VRDevice.Device.SecondaryInputDevice;
    }
    
    private void Update()
    {
        primaryInput = VRDevice.Device.PrimaryInputDevice;
        secondaryInput = VRDevice.Device.SecondaryInputDevice;
        /*
        if (bow.bowIsHeld == false)
        {
            float rightDistance = Vector3.Distance(rightHand.transform.position, bowGrabPoint.transform.position);
            float leftDistance = Vector3.Distance(leftHand.transform.position, bowGrabPoint.transform.position);

            if (rightDistance < grabDistance && input.primaryTriggerHeld)
            {
                bow.SetHand(rightHand.transform);
            } else if (leftDistance < grabDistance && input.secondaryTriggerHeld)
            {
                bow.SetHand(leftHand.transform);
            } if (rightDistance > grabDistance || leftDistance > grabDistance) return;
        } return;
        */
    }
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == rightHand && primaryInput.GetButton(VRButton.Trigger))
        {
            //bow.SetRightHand();

        }
        if (other.gameObject == leftHand && secondaryInput.GetButton(VRButton.Trigger))
        {
            //bow.SetLeftHand();

        }
        return;
    }
}
