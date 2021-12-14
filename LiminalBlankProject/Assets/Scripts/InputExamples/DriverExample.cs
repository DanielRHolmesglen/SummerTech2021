using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Liminal.SDK.VR;
using Liminal.SDK.VR.Input;

public class DriverExample : MonoBehaviour
{
    Vector3 direction;
    Quaternion rotation;
    public float speed, turningSpeed;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var primaryInput = VRDevice.Device.PrimaryInputDevice;
        var inputs = primaryInput.GetAxis2D(VRAxis.One);
        var trigger = primaryInput.GetAxis1D(VRButton.Trigger);

        if (trigger > 0.01 || Input.GetKey(KeyCode.Space)) 
        {
            direction.Set(inputs.x, 0, inputs.y);
            rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, turningSpeed);
            rb.AddForce(transform.forward * speed, ForceMode.Acceleration);
        }
        

       
    }
}
