using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Liminal.SDK.VR;
using Liminal.SDK.VR.Input;

public class HandAnimator : MonoBehaviour
{
    [SerializeField] GameObject hand;
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

        if (hand && primaryInput.GetButton(VRButton.Trigger) == true || hand && secondaryInput.GetButton(VRButton.Trigger) == true)
        {
            anim.SetFloat("Blend",1);
        }
        if (hand && primaryInput.GetButton(VRButton.Trigger) == false || hand && secondaryInput.GetButton(VRButton.Trigger) == false)
        {
            anim.SetFloat("Blend", 0);
        }

    }
}
