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
    public Animator rightAnim;
    public Animator leftAnim;
    [SerializeField] private GameObject bowGrabPoint;

    private float grabDistance = 0.15f;
    private void Start()
    {
        bow = GetComponent<Bow>();
        //rightAnim = rightHand.GetComponentInChildren<Animator>();
        //leftAnim = leftHand.GetComponentInChildren<Animator>();

    }

    private void Update()
    {
        primaryInput = VRDevice.Device.PrimaryInputDevice;
        secondaryInput = VRDevice.Device.SecondaryInputDevice;
        
        //if(Input.GetKeyDown("mouse 0")) leftAnim.SetFloat("BlendRight", 1);
        //if (Input.GetKeyUp("mouse 0")) leftAnim.SetFloat("BlendRight", 0);
        if (primaryInput.GetButton(VRButton.Trigger)) rightAnim.SetFloat("BlendRight", 1); else rightAnim.SetFloat("BlendRight", 0);
        if (secondaryInput.GetButton(VRButton.Trigger)) leftAnim.SetFloat("BlendLeft", 1); else leftAnim.SetFloat("BlendLeft", 0);

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
