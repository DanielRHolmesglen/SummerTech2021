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
    public GameObject bowGrabPoint;
    public SkinnedMeshRenderer bowMesh;

    private float grabDistance = 0.15f;

    private void Update()
    {
        primaryInput = VRDevice.Device.PrimaryInputDevice;
        secondaryInput = VRDevice.Device.SecondaryInputDevice;
        //Debug.Log("SetHandTrigger Update");

        if (bow.bowIsHeld == false)
        {
            float rightDistance = Vector3.Distance(rightHand.transform.position, bowGrabPoint.transform.position);
            float leftDistance = Vector3.Distance(leftHand.transform.position, bowGrabPoint.transform.position);

            if (rightDistance < grabDistance)
            {
                //bowMesh.material.SetColor("_Color", Color.blue);

                //add grab effect, either particle of colour
                if (primaryInput.GetButton(VRButton.Trigger))
                {
                    bow.SetRightHand();
                }

            } else if (leftDistance < grabDistance)
            {
                //bowMesh.material.SetColor("_Color", Color.red);

                //add grab effect, either particle of colour
                if (secondaryInput.GetButton(VRButton.Trigger))
                {
                    bow.SetLeftHand();
                }

            }
            else if (rightDistance > grabDistance && leftDistance > grabDistance)
            {
                //bowMesh.material.SetColor("_Color", Color.cyan);
                return;
            }
        } else return;
        
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, grabDistance);
    }
}
