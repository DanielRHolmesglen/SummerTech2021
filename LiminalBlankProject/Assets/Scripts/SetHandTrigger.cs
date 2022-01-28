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
    public MeshRenderer bowMesh;

    private BoxCollider bowCollider;
    private ButtonInputs triggerInputs;

    private float grabDistance = 0.15f;

    private void Awake()
    {
        //Debug.Log("SetHandTrigger Awake");
    }

    private void Start()
    {
        //Debug.Log("SetHandTrigger Start");
        bowMesh.material.SetColor("_Color", Color.cyan);
        bowCollider = GetComponentInChildren<BoxCollider>();
        //primaryInput = VRDevice.Device.PrimaryInputDevice;
        //secondaryInput = VRDevice.Device.SecondaryInputDevice;
    }
    
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
                bowMesh.material.SetColor("_Color", Color.blue);
                if (primaryInput.GetButton(VRButton.Trigger))
                {
                    bow.SetRightHand();
                }

            } else if (leftDistance < grabDistance)
            {
                bowMesh.material.SetColor("_Color", Color.red);
                if (secondaryInput.GetButton(VRButton.Trigger))
                {
                    bow.SetLeftHand();
                }

            }
            if (rightDistance > grabDistance || leftDistance > grabDistance)
            {
                bowMesh.material.SetColor("_Color", Color.cyan);
                return;
            }
        } else return;
        
    }
    /*

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == rightHand)
        {
            bowMesh.material.SetColor("_Color", Color.blue);
            //add fx here for grab
            if (primaryInput.GetButton(VRButton.Trigger))
            {
                bow.SetRightHand();
            }
        }
        if (other.gameObject == leftHand)
        {
            bowMesh.material.SetColor("_Color", Color.red);
            //add fx here for grab
            if (secondaryInput.GetButton(VRButton.Trigger))
            {
                bow.SetLeftHand();
            }
        }
        return;
    }*/
}
