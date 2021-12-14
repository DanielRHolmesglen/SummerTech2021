using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Liminal.SDK.VR;
using Liminal.SDK.VR.Input;
public class ButtonInputs : MonoBehaviour
{
    [SerializeField] ParticleSystem particles;
    [SerializeField] TrailRenderer trails;
 
    // Update is called once per frame
    void Update()
    {
        //get the primary input
        var primaryInput = VRDevice.Device.PrimaryInputDevice;

        if (primaryInput.GetButtonDown(VRButton.Trigger) || Input.GetMouseButtonDown(0))
        {
            particles.Play();
            trails.emitting = true;
        }
        if (primaryInput.GetButtonUp(VRButton.Trigger) || Input.GetMouseButtonUp(0))
        {
            particles.Play();
            trails.emitting = false;
        }
        if (primaryInput.GetButton(VRButton.Trigger) || Input.GetMouseButton(0))
        {

        }
    }
}



/// <summary>
/// Example script using the trigger inputs on the VR controller.
/// This script toggles a particle system and trail renderer on and off as an example of reacting to input events.
/// For emulation on the pc a mouse input has been included.
/// </summary>