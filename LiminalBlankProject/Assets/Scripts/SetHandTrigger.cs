using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Liminal.SDK.VR;
using Liminal.SDK.VR.Input;

public class SetHandTrigger : MonoBehaviour
{
    //should get from bow script as it's already set there.
    [Header("Vr Primary Hand")]
    [SerializeField] private GameObject rightHand;
    [Header("Vr Secondary Hand")]
    [SerializeField] private GameObject leftHand;

    public IVRInputDevice primaryInput, secondaryInput;
    private Bow bow;
    [SerializeField] private GameObject bowGrabPoint;

    private float grabDistance = 0.15f;
    private void Start()
    {
        bow = GetComponent<Bow>();
    }

    private void Update()
    {
        primaryInput = VRDevice.Device.PrimaryInputDevice;
        secondaryInput = VRDevice.Device.SecondaryInputDevice;
       

        if (bow.bowIsHeld == false)
        {
            float rightDistance = Vector3.Distance(rightHand.transform.position, bowGrabPoint.transform.position);
            float leftDistance = Vector3.Distance(leftHand.transform.position, bowGrabPoint.transform.position);

            if (rightDistance < grabDistance)
            {
                if (primaryInput.GetButton(VRButton.Trigger))
                {
                    bow.SetRightHand();
                }

            } else if (leftDistance < grabDistance)
            {
                if (secondaryInput.GetButton(VRButton.Trigger))
                {
                    bow.SetLeftHand();
                }

            }
        } else return;
        
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, grabDistance);
    }
}
