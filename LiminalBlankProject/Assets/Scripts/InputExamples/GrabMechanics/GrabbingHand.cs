using System.Collections;
using System.Collections.Generic;
using Liminal.SDK.VR;
using Liminal.SDK.VR.Input;
using UnityEngine;
using Valve.VR.InteractionSystem;
/// <summary>
/// This script detects grabbible items with rigidbodies, picks one, and picks up, carries, and releases it
/// Put this script on your hand object in the Unity scene and adjust it's distToPickup Value
/// all models that can be picked up should have colliders, and no rigidbody. They should be a child of an empty object that has a rigidbody as well as a cript that uses the IGrabbable interface.
/// </summary>
public class GrabbingHand : MonoBehaviour
{
    public float distToPickup = 0.3f; //used to limit how far you can grab
    bool handClosed = false; //used to track if the hand should look for new objects, or move the selected object
    

    Rigidbody holdingTarget; //the rigidbody of the object being held

    // Update is called once per frame
    void FixedUpdate()
    {
        //Get the primaryInput
        var primaryInput = VRDevice.Device.PrimaryInputDevice;

        //check that button is being held, if so set handClosed to true
        if (primaryInput.GetButton(VRButton.One) || Input.GetMouseButton(0))
            handClosed = true;
        else
            handClosed = false;

        if(!handClosed)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, distToPickup); //return a list of grabable objects in range
            if (colliders.Length > 0)
            {
                foreach (var item in colliders)
                {
                    var grabScript = item.transform.parent.GetComponent<IGrabbable>();
                    if (grabScript != null)
                    {
                        grabScript.Grab();//run any feedback you have put on the grabbable object
                        holdingTarget = item.transform.parent.GetComponent<Rigidbody>(); //get the first object in the list and assign it's parent rigidbody to
                        break;
                    }
                }
                
            }
            else
            {
                holdingTarget = null;
            }
        }
        else
        {
            if (holdingTarget)
            {
                //adjust the velocity of target to move to hand
                holdingTarget.velocity = (transform.position - holdingTarget.transform.position) / Time.fixedDeltaTime;

                //Find rotation values and convert to eulers and radians
                holdingTarget.maxAngularVelocity = 20f; 
                Quaternion deltaRot = transform.rotation * Quaternion.Inverse(holdingTarget.transform.rotation);

                Vector3 eulerRot = new Vector3(Mathf.DeltaAngle(0, deltaRot.eulerAngles.x), 
                   Mathf.DeltaAngle(0, deltaRot.eulerAngles.y), Mathf.DeltaAngle(0, deltaRot.eulerAngles.z));

               
                eulerRot *= Mathf.Deg2Rad;

                //adjust the angular velocity of target to rotate to hand
                holdingTarget.angularVelocity = eulerRot / Time.fixedDeltaTime;
            }
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, distToPickup);
    }
}
