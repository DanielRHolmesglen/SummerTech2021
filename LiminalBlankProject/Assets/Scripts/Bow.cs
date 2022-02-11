using System.Collections;
using System.Collections.Generic;
using Liminal.SDK.VR;
using Liminal.SDK.VR.Input;
using UnityEngine;

public class Bow : MonoBehaviour
{
    [Tooltip("Vr Primary Hand")]
    public GameObject rightHand;
    public Transform rightAnchor;
    
    [Tooltip("Vr Secondary Hand")]
    public GameObject leftHand;
    public Transform leftAnchor;

    public GameObject arrowPrefab; 
    public MeshRenderer arrowMesh;
    public Transform arrowSpawnPoint;
    public SkinnedMeshRenderer bowMesh;
    public Transform bowGrabPoint;
    public Transform fullDrawPoint;
    public Transform arrowGrabPoint;
    public Transform arrowNotch;
    public Transform arrowNotchDefaultPoint;
    public Transform stringPosition;
    public bool bowIsHeld = false;
    public ParticleSystem arrowSpawn;
    public ParticleSystem burstParticle;
    

    [Header("Bow String")]    
    //public Transform topStringPoint;
    //public Transform bottomStringPoint;
    public LineRenderer bowString;
    public Vector3[] stringPoints;

    public IVRInputDevice primaryInput, secondaryInput;


    //private ButtonInputs inputs;
    private float grabThreshold = 0.15f;
    private float moveSpeed = 2000f;
    private GameObject holdingHand;
    private GameObject pullingHand;
    private Transform anchorOffset;
    private MeshRenderer rightHandMesh;
    private MeshRenderer leftHandMesh;
    //private Transform arrowNotchDefaultPoint; set as new transform
    private Animator animator;
    private bool isStringHeld = false;
    

    private float pullValue = 0.0f;

   
    void Start()
    {
        
        rightHandMesh = rightHand.GetComponentInChildren<MeshRenderer>();
        leftHandMesh = leftHand.GetComponentInChildren<MeshRenderer>();
        animator = GetComponentInChildren<Animator>();
        StartCoroutine(CreateDummyArrow(0.0f));
        arrowNotchDefaultPoint.position = arrowNotch.position;
        //bowString = GetComponent<LineRenderer>();

        bowString.positionCount = stringPoints.Length;
        //positions[0] = topStringPoint.transform.position;
        //positions[1] = arrowNotch.transform.position; // need to set in update
        //positions[2] = bottomStringPoint.transform.position;
        //bowString.positionCount = positions.Length;
        
    }

    // Update is called once per frame
    void Update()
    {
        primaryInput = VRDevice.Device.PrimaryInputDevice;
        secondaryInput = VRDevice.Device.SecondaryInputDevice;
        bowString.SetPosition(1, stringPosition.localPosition);

        
        //for (int i = 0; i < stringPoints.Length; i++)
        //{
        //    bowString.SetPosition(i, stringPoints[i].localPosition);
        //}

        if (bowIsHeld)
        {

            UpdateBowPosition();
            if (isStringHeld)
            {
                pullValue = CalculatePull(pullingHand.transform);
                pullValue = Mathf.Clamp(pullValue, 0f, 1f);

                arrowNotch.position = pullingHand.transform.position;
                stringPosition.localPosition = pullingHand.transform.position;


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
            arrowNotch.position = arrowNotchDefaultPoint.position;

            pullValue = 0;
            animator.SetFloat("Blend", 0.0f);

            StartCoroutine(CreateDummyArrow(0.25f));

        } else
        {
            pullValue = 0;
            animator.SetFloat("Blend", 0.0f);
            arrowNotch.position = arrowNotchDefaultPoint.position;
        }
       
    }
    private void FireArrow(float pullValue)
    {
        GameObject arrow = Instantiate(arrowPrefab, arrowSpawnPoint);
        arrow.transform.SetParent(null, true);
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

            arrowSpawnPoint.transform.rotation = Quaternion.LookRotation(aimDirection);

        } else
        {
            transform.rotation = anchorOffset.transform.rotation;
        }
        // need to add 45 to x axis rotaion of holdingHand

    }

    public void SetRightHand()
    {
        //rightHand = holdingHand;
        //leftHand = pullingHand;
        holdingHand = rightHand;
        pullingHand = leftHand;
        anchorOffset = rightAnchor;
        bowIsHeld = true;
        rightHandMesh.enabled = false;
    }
    public void SetLeftHand()
    {
        //leftHand = holdingHand;
        //rightHand = pullingHand;
        holdingHand = leftHand;
        pullingHand = rightHand;
        anchorOffset = leftAnchor;
        bowIsHeld = true;
        leftHandMesh.enabled = false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(arrowGrabPoint.position, grabThreshold);
    }
    
}
