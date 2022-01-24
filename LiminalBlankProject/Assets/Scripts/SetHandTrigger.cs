using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetHandTrigger : MonoBehaviour
{
    public GameObject rightHand;  //PrimaryHand
    public GameObject leftHand; // SecondaryHand
    public Transform bowGrabPoint;
    ButtonInputs input;
    Bow bow;

    private float grabDistance = 0.15f;

    private Collider bowCollider;

    private void Start()
    {
        bowCollider = GetComponent<Collider>();
    }

    private void Update()
    {
        if (bow.bowIsHeld == false)
        {
            float rightDistance = Vector3.Distance(rightHand.transform.position, bowGrabPoint.transform.position);
            float leftDistance = Vector3.Distance(leftHand.transform.position, bowGrabPoint.transform.position);

            if (rightDistance < grabDistance && input.primaryTriggerHeld)
            {
                bow.SetHand(rightHand.transform);
            } else if (leftDistance < grabDistance && input.secondaryTriggerHeld)
            {
                bow.SetHand(leftHand.transform);
            } if (rightDistance > grabDistance || leftDistance > grabDistance) return;
        } return;
    }


    private void OnTriggerEnter(Collider other)
    {
        // if rightHand collider enters and  right VR trigger is pulled 
        // set bow hand as righthand
        // else if lefthand collider enters and left VR trigger is pulled
        // set left as bow hand
    }
}
