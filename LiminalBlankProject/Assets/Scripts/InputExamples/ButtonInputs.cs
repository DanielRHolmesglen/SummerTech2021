using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Liminal.SDK.VR;
using Liminal.SDK.VR.Input;
public class ButtonInputs : MonoBehaviour
{
    //public GameObject primaryInput;
    //public GameObject secondaryInput;

    public bool primaryTriggerHeld = false;
    public bool secondaryTriggerHeld = false;

    public IVRInputDevice primaryInput, secondaryInput;

    public MeshRenderer leftHandMesh;
    public MeshRenderer rightHandMesh;

    // Update is called once per frame
    void Update()
    {        
        primaryInput = VRDevice.Device.PrimaryInputDevice;
        secondaryInput = VRDevice.Device.SecondaryInputDevice;
        // may be more performant to cache in start???
        // don't know how
        if (primaryTriggerHeld) rightHandMesh.material.SetColor("_Color", Color.green);
        else rightHandMesh.material.SetColor("_Color", Color.grey);
        if (secondaryTriggerHeld) leftHandMesh.material.SetColor("_Color", Color.green);
        else leftHandMesh.material.SetColor("_Color", Color.grey);
        

        if (primaryInput.GetButtonDown(VRButton.Trigger))
        {
            primaryTriggerHeld = true;
        }
        if (primaryInput.GetButtonUp(VRButton.Trigger))
        {
            primaryTriggerHeld = false;
        }
        if (secondaryInput.GetButtonDown(VRButton.Trigger))
        {
            secondaryTriggerHeld = true;
        }
        if (secondaryInput.GetButtonUp(VRButton.Trigger))
        {
            secondaryTriggerHeld = false;
        }

    }
}



/// <summary>
/// Example script using the trigger inputs on the VR controller.
/// This script toggles a particle system and trail renderer on and off as an example of reacting to input events.
/// For emulation on the pc a mouse input has been included.
/// </summary>