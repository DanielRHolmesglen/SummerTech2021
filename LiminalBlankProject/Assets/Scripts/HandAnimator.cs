using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Liminal.SDK.VR;
using Liminal.SDK.VR.Input;

public class HandAnimator : MonoBehaviour
{
    //[SerializeField] GameObject hand;
    [SerializeField] bool ifRightHand = true;
    public IVRInputDevice primaryInput, secondaryInput;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        primaryInput = VRDevice.Device.PrimaryInputDevice;
        secondaryInput = VRDevice.Device.SecondaryInputDevice;

        //if (Input.GetKeyDown("mouse 1")) // debug
        //{
        //    anim.SetFloat("Blend", 1);
        //}
        //if (Input.GetKeyDown("mouse 0"))
        //{
        //    anim.SetFloat("Blend", 0);
        //}

        if (ifRightHand && primaryInput.GetButton(VRButton.Trigger) == true || !ifRightHand && secondaryInput.GetButton(VRButton.Trigger) == true)
        {
            anim.SetFloat("Blend", 0);
        }
        else if (ifRightHand && primaryInput.GetButton(VRButton.Trigger) == false || !ifRightHand && secondaryInput.GetButton(VRButton.Trigger) == false)
        {
            anim.SetFloat("Blend", 1);
        }
    }
}
