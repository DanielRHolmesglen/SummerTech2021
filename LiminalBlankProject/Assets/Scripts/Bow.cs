﻿using System.Collections;
using System.Collections.Generic;
using Liminal.SDK.VR;
using Liminal.SDK.VR.Input;
using UnityEngine;

public class Bow : MonoBehaviour
{
    public GameObject arrowPrefab; // for instatiating the shot arrow
    public MeshRenderer arrowMesh; // for holding an arrow in the bow
    public GameObject rightHand;  //PrimaryHand
    public GameObject leftHand; // SecondaryHand
    public float moveSpeed = 10f;
    public Transform arrowSpawnPoint;
    public MeshRenderer bowMesh;
    public float grabThreshold = 0.15f;
    public Transform bowGrabPoint;
    public Transform fullDrawPoint;
    public Transform arrowGrabPoint;
    public Transform arrowNotch;
    public bool bowIsHeld = false;
    public ParticleSystem arrowSpawn;
    public ParticleSystem burstParticle;


    public IVRInputDevice primaryInput, secondaryInput;


    //private ButtonInputs inputs;
    private GameObject holdingHand;
    private GameObject pullingHand;
    private MeshRenderer rightHandMesh;
    private MeshRenderer leftHandMesh;
    private Transform arrowNotchDefaultPoint; // to set the point the arrow mesh returns to
    //private GameObject currentArrow; //'Arrow' in tutorial?
    private Animator animator;
    private bool isStringHeld = false;

    private float pullValue = 0.0f;

   
    void Start()
    {
        Debug.Log("Bow Start");

        rightHandMesh = rightHand.GetComponentInChildren<MeshRenderer>();
        leftHandMesh = leftHand.GetComponentInChildren<MeshRenderer>();
        animator = GetComponent<Animator>();
        StartCoroutine(CreateDummyArrow(0.0f));
        arrowNotch.position = arrowNotchDefaultPoint.position;
    }

    // Update is called once per frame
    void Update()
    {
        primaryInput = VRDevice.Device.PrimaryInputDevice;
        secondaryInput = VRDevice.Device.SecondaryInputDevice;
        //Debug.Log("Bow Update");

        if (bowIsHeld)
        {

            UpdateBowPosition();
            if (isStringHeld)
            {
                pullValue = CalculatePull(pullingHand.transform);
                pullValue = Mathf.Clamp(pullValue, 0f, 1f);

                arrowNotch.position = pullingHand.transform.position;
                animator.SetFloat("Blend", pullValue);

                if (pullingHand == rightHand && primaryInput.GetButton(VRButton.Trigger) == false || pullingHand == leftHand && secondaryInput.GetButton(VRButton.Trigger) == false)
                    
                {
                    bowMesh.material.SetColor("_Color", Color.cyan);
                    isStringHeld = false;
                    Release();
                }
            } else ReadyPull(pullingHand);
        } else return;
    }

    private float CalculatePull(Transform pullHand) 
    {
        Vector3 direction = fullDrawPoint.position - arrowGrabPoint.position;
        float magnitude = direction.magnitude;

        
        direction.Normalize();
        Vector3 difference = pullHand.position - arrowGrabPoint.position;

        return Vector3.Dot(difference, direction) / magnitude;
    }

    
    public void ReadyPull(GameObject hand) // assign pullHand
    {
        float distance = Vector3.Distance(hand.transform.position, arrowGrabPoint.position);
        
        if(distance < grabThreshold)
        {
            bowMesh.material.SetColor("_Color", Color.green);
            //play grab string particle FX
            if (pullingHand == rightHand && primaryInput.GetButton(VRButton.Trigger) || pullingHand == leftHand && secondaryInput.GetButton(VRButton.Trigger))
            
            {
                bowMesh.material.SetColor("_Color", Color.yellow);
                isStringHeld = true;
            }
        } else { return; }
    }

    public void Release()  
    {
        if (pullValue > 0.5f)
        {
            FireArrow(pullValue);
            // play release soundfx
            arrowMesh.enabled = false;
            arrowNotchDefaultPoint.position = arrowNotch.position;

            pullValue = 0;
            animator.SetFloat("Blend", 0);

            StartCoroutine(CreateDummyArrow(0.1f));

        } else
        {
            pullValue = 0;
            animator.SetFloat("Blend", 0);
            arrowNotchDefaultPoint.position = arrowNotch.position;
        }
       
    }
    private void FireArrow(float pullValue)
    {
        GameObject arrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, arrowSpawnPoint.rotation);
        arrow.GetComponent<Rigidbody>().AddForce(transform.forward * (pullValue * moveSpeed));
        Destroy(arrow, 2.5f);
    }

    private IEnumerator CreateDummyArrow(float waitTime)
    {
        arrowSpawn.Play();
        // play arrow spawn particle fx
        yield return new WaitForSeconds(waitTime);
        arrowMesh.enabled = true;

    }
   

    private void UpdateBowPosition()
    {
        transform.position = holdingHand.transform.position;
        if (isStringHeld)
        {
            Vector3 aimDirection = holdingHand.transform.position - pullingHand.transform.position;
            transform.rotation = Quaternion.LookRotation(aimDirection, holdingHand.transform.up);

        } else transform.rotation = holdingHand.transform.rotation;
        // need to offset x rotation by 40-45 degrees so when you hold the controller straight the bow is straight
        //Quaternion.Euler(holdingHand.transform.rotation.x - 40,holdingHand.transform.rotation.y,holdingHand.transform.rotation.z);
    }

    public void SetRightHand()
    {
        //rightHand = holdingHand;
        //leftHand = pullingHand;
        holdingHand = rightHand;
        pullingHand = leftHand;
        bowIsHeld = true;
        rightHandMesh.enabled = false;
    }
    public void SetLeftHand()
    {
        //leftHand = holdingHand;
        //rightHand = pullingHand;
        holdingHand = leftHand;
        pullingHand = rightHand;
        bowIsHeld = true;
        leftHandMesh.enabled = false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(arrowGrabPoint.position, grabThreshold);
    }
    
}
