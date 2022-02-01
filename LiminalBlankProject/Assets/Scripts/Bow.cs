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

    public GameObject testArrow;
    public float moveSpeed = 2000f;
    private Rigidbody testArrowRB;
    public Transform arrowSpawnPoint;

    //public MeshRenderer bowMesh;
    public IVRInputDevice primaryInput, secondaryInput;

    public float grabThreshold = 0.15f;
    //public BoxCollider arrowGrabPoint;
    public BoxCollider bowGrabPoint;
    public Transform fullDrawPoint;
    public Transform startDrawPoint;
    public bool bowIsHeld = false;
    public ParticleSystem burstParticle;
    public Vector3 arrowSocket;


    //private ButtonInputs inputs;
    private GameObject holdingHand;
    private GameObject pullingHand;
    private MeshRenderer rightHandMesh;
    private MeshRenderer leftHandMesh;
    public MeshRenderer bowMesh;
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
        testArrowRB = testArrow.GetComponent<Rigidbody>();
        //arrowGrabPoint.enabled = false;
        animator = GetComponent<Animator>();
        StartCoroutine(CreateDummyArrow(0.0f));
        //added for debugging
        //SetRightHand();
        //bowIsHeld = true;
    }

    // Update is called once per frame
    void Update()
    {
        primaryInput = VRDevice.Device.PrimaryInputDevice;
        secondaryInput = VRDevice.Device.SecondaryInputDevice;
        Debug.Log("Bow Update");

        if (bowIsHeld)
        {
            UpdateBowPosition();
            if (isStringHeld)
            {
                pullValue = CalculatePull(pullingHand.transform);
                pullValue = Mathf.Clamp(pullValue, 0f, 1f);


                animator.SetFloat("Blend", pullValue);

                if (pullingHand == rightHand && primaryInput.GetButton(VRButton.Trigger) == false || pullingHand == leftHand && secondaryInput.GetButton(VRButton.Trigger) == false)
                    //(primaryInput.GetButtonUp(VRButton.Trigger) || secondaryInput.GetButtonUp(VRButton.Trigger))
                    // add in check for if left or right hand is pullingHand
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
        Vector3 direction = fullDrawPoint.position - startDrawPoint.position;
        float magnitude = direction.magnitude;

        // need to make the arrow pull back here

        direction.Normalize();
        Vector3 difference = pullHand.position - startDrawPoint.position;

        return Vector3.Dot(difference, direction) / magnitude;
    }

    
    public void ReadyPull(GameObject hand) // assign pullHand
    {
        float distance = Vector3.Distance(hand.transform.position, startDrawPoint.position);
        
        if(distance < grabThreshold)
        {
            bowMesh.material.SetColor("_Color", Color.green);
            //play grab string particle FX
            if (pullingHand == rightHand && primaryInput.GetButton(VRButton.Trigger) || pullingHand == leftHand && secondaryInput.GetButton(VRButton.Trigger))
            //(primaryInput.GetButtonDown(VRButton.Trigger)|| secondaryInput.GetButtonDown(VRButton.Trigger))
            // add check for left or right hand.
            {
                bowMesh.material.SetColor("_Color", Color.yellow);
                isStringHeld = true;
            }
        } else { return; }
    }

    public void Release()  
    {
        if (pullValue > 0.25f) FireTestArrow(pullValue);
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
   

    private void UpdateBowPosition()
    {
        transform.position = holdingHand.transform.position;
        if (isStringHeld)
        {
            Vector3 aimDirection = holdingHand.transform.position - pullingHand.transform.position;
            Vector3 handAngle = new Vector3(0,0,holdingHand.transform.rotation.z);
            transform.rotation = Quaternion.LookRotation(aimDirection,handAngle);

            //Vector3 aimDirection = new Vector3(pullingHand.transform.position.x, pullingHand.transform.position.y, holdingHand.transform.position.z);
            //transform.rotation = Quaternion.Euler(aimDirection);

        } else transform.rotation = holdingHand.transform.rotation;
    }
    
    public void SetRightHand()
    {
        //rightHand = holdingHand;
        //leftHand = pullingHand;
        holdingHand = rightHand;
        pullingHand = leftHand;
        bowIsHeld = true;
        //arrowGrabPoint.enabled = true;
        bowGrabPoint.enabled = false;
        rightHandMesh.enabled = false;
    }
    public void SetLeftHand()
    {
        //leftHand = holdingHand;
        //rightHand = pullingHand;
        holdingHand = leftHand;
        pullingHand = rightHand;
        bowIsHeld = true;
        //arrowGrabPoint.enabled = true;
        bowGrabPoint.enabled = false;
        leftHandMesh.enabled = false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(startDrawPoint.position, grabThreshold);
    }
    private void FireTestArrow(float pullValue)
    {
        GameObject arrow = Instantiate<GameObject>(testArrow, arrowSpawnPoint.position, arrowSpawnPoint.rotation);
        //shot.GetComponent<Rigidbody>().AddForce(transform.forward * (pullValue * moveSpeed));
        testArrowRB.AddForce(transform.forward * (pullValue * moveSpeed));

        Destroy(arrow, 2.5f);
    }
}
