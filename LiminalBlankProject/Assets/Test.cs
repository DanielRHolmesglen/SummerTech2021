using System.Collections;
using System.Collections.Generic;
using Liminal.SDK.VR;
using Liminal.SDK.VR.Input;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    public IVRInputDevice primaryInput;
    public IVRInputDevice secondaryInput;

    public Text temp;
    public Animator Anim;

    private float rightTriggerValue;
    private float leftTriggerValue;

    // Update is called once per frame
    void Update()
    {
        if (primaryInput == null)
        {
            primaryInput = VRDevice.Device.PrimaryInputDevice;
        }
        if (secondaryInput == null)
        {
            secondaryInput = VRDevice.Device.SecondaryInputDevice;
        }

        if (primaryInput != null)
        {
            rightTriggerValue = primaryInput.GetAxis1D(VRAxis.Two);
        }

        if (secondaryInput != null)
        {
            leftTriggerValue = secondaryInput.GetAxis1D(VRAxis.Two);
        }

        temp.text = $"{rightTriggerValue}  {leftTriggerValue}";

        Anim.SetFloat("Blend", rightTriggerValue);
    }
}
