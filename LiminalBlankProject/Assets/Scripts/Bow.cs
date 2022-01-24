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
    public ButtonInputs input;

    public float grabThreshold = 0.15f;
    public BoxCollider arrowGrabPoint;
    public BoxCollider bowGrabPoint;
    public Transform fullDrawPoint;
    public Transform startDrawPoint;
    //public Transform arrowSocket;

    private MeshRenderer rightHandMesh;
    private MeshRenderer leftHandMesh;
    private GameObject holdingHand;
    private GameObject pullingHand;
    private GameObject currentArrow; //'Arrow' in tutorial?
    private Animator animator;
    public bool bowIsHeld = false;
    private bool isStringHeld = false;
    //private bool rigthHandInRange = false;
    //private bool leftHandInRange = false;

    [SerializeField]private float pullValue = 0.0f;

    
    // Start is called before the first frame update
    void Start()
    {
        // put in a follow command to tell bow to follow rotation and position of holdingHand
        rightHandMesh = rightHand.GetComponentInChildren<MeshRenderer>();
        leftHandMesh = leftHand.GetComponentInChildren<MeshRenderer>();
        arrowGrabPoint.enabled = false;
        animator = GetComponent<Animator>();
        StartCoroutine(CreateDummyArrow(0.0f));
    }

    // Update is called once per frame
    void Update()
    {
        if (!pullingHand || !arrowPrefab) return;
        pullValue = CalculatePull(pullingHand.transform);
        pullValue = Mathf.Clamp(pullValue, 0f, 1f);

        if (!bowIsHeld) return;
        else UpdateBowPosition();

        animator.SetFloat("Blend", pullValue);
    }

    private float CalculatePull(Transform pullHand) // may need to manually assign pullHand
    {
        Vector3 direction = fullDrawPoint.position - startDrawPoint.position;
        float magnitude = direction.magnitude;

        direction.Normalize();
        Vector3 difference = pullHand.position - startDrawPoint.position;

        return Vector3.Dot(difference, direction) / magnitude;
    }

    
    public void Pull(GameObject hand) // assign pullHand
    {
        float distance = Vector3.Distance(hand.transform.position, startDrawPoint.position);

        if (distance > grabThreshold) return;

        pullingHand = hand;
    }

    public void Release()
    {
        if (pullValue > 0.25f) FireArrow();
        // play release soundfx
        pullingHand = null; // ???

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
        GameObject arrowObject = Instantiate(arrowMesh, arrowSocket);
        arrowObject.transform.localPosition = new Vector3(0, 0, 0.425f); // need to confirm location
        arrowObject.transform.localEulerAngles = Vector3.zero;
        currentArrow = arrowObject.GetComponent<GameObject>();
        */
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == rightHand && input.primaryTriggerHeld)
        {
            if (!bowIsHeld)
            {
                rightHand = holdingHand;
                leftHand = pullingHand;
                arrowGrabPoint.enabled = true;
                bowGrabPoint.enabled = false;
            }
            Pull(pullingHand);
        }
        if (other.gameObject == leftHand && input.secondaryTriggerHeld)
        {
            if (!bowIsHeld)
            {
                leftHand = holdingHand;
                rightHand = pullingHand;
                arrowGrabPoint.enabled = true;
                bowGrabPoint.enabled = false;
            }
            Pull(pullingHand);
        }
    }


    private void UpdateBowPosition()
    {
        transform.position = holdingHand.transform.position;
        if (!isStringHeld)
        {
            transform.rotation = holdingHand.transform.rotation;
            // need to set so that it rotates to the direction between the hands.
            // is calculated in CalculatePull as well ?
        }
    }
    public void SetHand(Transform activeHand)
    {
        arrowGrabPoint.enabled = true;
        bowGrabPoint.enabled = false;
    }
}
