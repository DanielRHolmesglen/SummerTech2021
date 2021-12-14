using System.Collections;
using System.Collections.Generic;
using Liminal.SDK.VR;
using Liminal.SDK.VR.Input;
using TMPro;
using UnityEngine;

public class AxisExample : MonoBehaviour
{
    Vector3 direction;
    public float speed;
    public TextMeshProUGUI textDisplay;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var primaryInput = VRDevice.Device.PrimaryInputDevice;
        var inputs = primaryInput.GetAxis2D(VRAxis.One);

        direction.Set(inputs.x, 0, inputs.y);

        transform.Translate(direction * speed * Time.deltaTime);
        textDisplay.text = "horizontal axis: " + inputs.x + "/n Vertical axis: " + inputs.y;
    }
}
