using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Liminal.SDK.VR;
using Liminal.SDK.VR.Input;
//using UnityEngine.UI;

public class HandAnimator : MonoBehaviour
{
    [SerializeField] bool isRightHand = true;
    public IVRInputDevice primaryInput, secondaryInput;
    Animator anim;

    //public Text temp;
    private float rightTriggerValue;
    private float leftTriggerValue;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(primaryInput == null)
        {
            primaryInput = VRDevice.Device.PrimaryInputDevice;
        }
        if(secondaryInput == null)
        {
            secondaryInput = VRDevice.Device.SecondaryInputDevice;
        }

        // Tried to get the value of how much the trigger was being pulled. Couldn't get it working.

        if (primaryInput != null)
        {
            rightTriggerValue = primaryInput.GetAxis1D(VRAxis.Two);
        }
        if (secondaryInput != null)
        {
            leftTriggerValue = secondaryInput.GetAxis1D(VRAxis.Two);
        }

        //temp.text = $"{rightTriggerValue}  {leftTriggerValue}";

        if (isRightHand) anim.SetFloat("Blend", rightTriggerValue);
        if (!isRightHand) anim.SetFloat("Blend", leftTriggerValue);

        //if (isRightHand && primaryInput.GetButton(VRButton.Trigger) == true || !isRightHand && secondaryInput.GetButton(VRButton.Trigger) == true)
        //{
        //    anim.SetFloat("Blend", 1);
        //}
        //else if (isRightHand && primaryInput.GetButton(VRButton.Trigger) == false || !isRightHand && secondaryInput.GetButton(VRButton.Trigger) == false)
        //{
        //    anim.SetFloat("Blend", 0);
        //}
    }
}
