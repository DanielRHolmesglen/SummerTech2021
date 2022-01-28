using System.Collections;
using System.Collections.Generic;
using Liminal.SDK.VR;
using Liminal.SDK.VR.Input;
using UnityEngine;

public class Bow : MonoBehaviour
{
    public Arrow arrowPrefab; // for instatiating the shot arrow
    public MeshRenderer arrowMesh; // for holding an arrow in the bow
    public GameObject rightHand;  //PrimaryHand
    public GameObject leftHand; // SecondaryHand
    //public MeshRenderer bowMesh;
    //public IVRInputDevice primaryInput, secondaryInput;

    public float grabThreshold = 0.15f;
    //public BoxCollider arrowGrabPoint;
    public BoxCollider bowGrabPoint;
    public Transform fullDrawPoint;
    public Transform startDrawPoint;
    public bool bowIsHeld = false;
    public ParticleSystem burstParticle;
    //public Vector3 arrowSocket;


    private ButtonInputs inputs;
    private GameObject holdingHand;
    private GameObject pullingHand;
    private MeshRenderer rightHandMesh;
    private MeshRenderer leftHandMesh;
    private GameObject currentArrow; //'Arrow' in tutorial?
    private Animator animator;
    private bool isStringHeld = false;
    //private bool rigthHandInRange = false;
    //private bool leftHandInRange = false;
    //private bool primaryTriggerHeld = false;
    //private bool secondaryTriggerHeld = false;

    [SerializeField]private float pullValue = 0.0f;

    private void Awake()
    {
        Debug.Log("Bow Awake");
    }
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Bow Start");
        rightHandMesh = rightHand.GetComponentInChildren<MeshRenderer>();
        leftHandMesh = leftHand.GetComponentInChildren<MeshRenderer>();
        //arrowGrabPoint.enabled = false;
        animator = GetComponent<Animator>();
        StartCoroutine(CreateDummyArrow(0.0f));

    }

    // Update is called once per frame
    void Update()
    {
        //primaryInput = VRDevice.Device.PrimaryInputDevice;
        //secondaryInput = VRDevice.Device.SecondaryInputDevice;
        Debug.Log("Bow Update");

        if (bowIsHeld)
        {
            UpdateBowPosition();
            if (isStringHeld)
            {
                pullValue = CalculatePull(pullingHand.transform);
                pullValue = Mathf.Clamp(pullValue, 0f, 1f);

                animator.SetFloat("Blend", pullValue);

                if (inputs.primaryTriggerHeld == false || inputs.secondaryTriggerHeld == false)
                    //(primaryInput.GetButtonUp(VRButton.Trigger) || secondaryInput.GetButtonUp(VRButton.Trigger))
                    // add in check for if left or right hand is pullingHand
                {
                    isStringHeld = false;
                    Release();
                }
            } else ReadyPull(pullingHand);
        } else return;
    }

    private float CalculatePull(Transform pullHand) 
    {
        Vector3 direction = fullDrawPoint.position - startDrawPoint.position;
        float magnitude = direction.magnitude;

        direction.Normalize();
        Vector3 difference = pullHand.position - startDrawPoint.position;

        return Vector3.Dot(difference, direction) / magnitude;
    }

    
    public void ReadyPull(GameObject hand) // assign pullHand
    {
        float distance = Vector3.Distance(hand.transform.position, startDrawPoint.position);
        
        if(distance < grabThreshold)
        {
            //play grab string particle FX
            if (inputs.primaryTriggerHeld || inputs.secondaryTriggerHeld)
                //(primaryInput.GetButtonDown(VRButton.Trigger)|| secondaryInput.GetButtonDown(VRButton.Trigger))
                // add check for left or right hand.
            {
                isStringHeld = true;
            }
        } else { return; }
    }

    public void Release()  
    {
        if (pullValue > 0.25f) FireArrow();
        // play release soundfx
        //pullingHand = null; // ???

        pullValue = 0;
        animator.SetFloat("Blend", 0);

        if (!currentArrow)
            StartCoroutine(CreateDummyArrow(0.25f));
    }
    private void FireArrow()
    {
        arrowPrefab.Fire(pullValue);
        arrowMesh.enabled = false;
    }

    private IEnumerator CreateDummyArrow(float waitTime)
    {

        // play arrow spawn particle fx
        yield return new WaitForSeconds(waitTime);
        arrowMesh.enabled = true;

        
        /*
        GameObject arrowObject = Instantiate(arrowPrefab);
        arrowObject.transform.localPosition = new Vector3(0, 0, 0.425f); // need to confirm location
        arrowObject.transform.localEulerAngles = Vector3.zero;
        currentArrow = arrowObject.GetComponent<GameObject>();
        */
    }
    // trigger update now in SetHandTrigger class
    /*
    private void OnTriggerStay (Collider other) //OnTriggerEnter wasn't working either
    {
        if (other.gameObject) burstParticle.Play();

        if(other.gameObject == rightHand && inputs.primaryTriggerHeld) //primaryInput.GetButton(VRButton.Trigger)
        {
            rightHand = holdingHand;
            leftHand = pullingHand;
            bowIsHeld = true;
            //arrowGrabPoint.enabled = true;
            bowGrabPoint.enabled = false;
            rightHandMesh.enabled = false;

        }
        if (other.gameObject == leftHand && inputs.secondaryTriggerHeld) //secondaryInput.GetButton(VRButton.Trigger)
        {
            leftHand = holdingHand;
            rightHand = pullingHand;
            bowIsHeld = true;
            //arrowGrabPoint.enabled = true;
            bowGrabPoint.enabled = false;
            leftHandMesh.enabled = false;
        }
    }
    */

    private void UpdateBowPosition()
    {
        transform.position = holdingHand.transform.position;
        if (isStringHeld)
        {
            transform.rotation = holdingHand.transform.rotation;
            // need to set so that it rotates to the direction between the hands.
            // is calculated in CalculatePull as well ?
        } else transform.rotation = holdingHand.transform.rotation;
    }
    
    public void SetRightHand()
    {
        rightHand = holdingHand;
        leftHand = pullingHand;
        bowIsHeld = true;
        //arrowGrabPoint.enabled = true;
        bowGrabPoint.enabled = false;
        rightHandMesh.enabled = false;
    }
    public void SetLeftHand()
    {
        leftHand = holdingHand;
        rightHand = pullingHand;
        bowIsHeld = true;
        //arrowGrabPoint.enabled = true;
        bowGrabPoint.enabled = false;
        leftHandMesh.enabled = false;
    }
    
}
