using System.Collections;
using System.Collections.Generic;
using Liminal.SDK.VR;
using Liminal.SDK.VR.Input;
using UnityEngine;

public class Bow : MonoBehaviour
{
    [Header("Vr Primary Hand")]
    [SerializeField] private GameObject rightHand;
    [Tooltip("The anchors have to be rotated 45,0,0")]
    [SerializeField] private Transform rightAnchor;
    
    [Header("Vr Secondary Hand")]
    [SerializeField] private GameObject leftHand;
    [Tooltip("The anchors have to be rotated 45,0,0")]
    [SerializeField] private Transform leftAnchor;


    [SerializeField] private GameObject arrowPrefab; 
    [SerializeField] private MeshRenderer arrowMesh;
    [SerializeField] private Transform arrowSpawnPoint;
    [SerializeField] private Transform bowGrabPoint;
    [SerializeField] private Transform fullDrawPoint;
    [SerializeField] private Transform arrowGrabPoint;
    [SerializeField] private Transform arrowNotch;
    [SerializeField] private Transform arrowNotchDefaultPoint;
    [SerializeField] private ParticleSystem arrowSpawn;
    [SerializeField] private ParticleSystem burstParticle;
    [SerializeField] private AudioSource releaseAudioSource;
    [SerializeField] private AudioClip[] releaseSoundsArray;

    [HideInInspector]
    public bool bowIsHeld = false;


    [Header("Bow String")]
    [SerializeField] private LineRenderer bowString;
    [SerializeField] private Transform[] stringPoints; // it's set here... but its not.
    [SerializeField] private Transform stringPosition;
    [SerializeField] private Transform topStringPos; //line renderers are jank.
    [SerializeField] private Transform bottomStringPos; // it needs to be set to world pos or wont work... and updated every frame...

    public IVRInputDevice primaryInput, secondaryInput;
    
    private float grabThreshold = 0.15f;
    private float moveSpeed = 2000f;
    private GameObject holdingHand;
    private GameObject pullingHand;
    private Transform anchorOffset;
    private MeshRenderer rightHandMesh;
    private MeshRenderer leftHandMesh;
    private Animator animator;
    private bool isStringHeld = false;

    public PowerUp powerUpCS;

    private float pullValue = 0.0f;

   
    void Start()
    {
        
        rightHandMesh = rightHand.GetComponentInChildren<MeshRenderer>();
        leftHandMesh = leftHand.GetComponentInChildren<MeshRenderer>();
        animator = GetComponentInChildren<Animator>();
        StartCoroutine(CreateDummyArrow(0.0f));
        arrowNotchDefaultPoint.position = arrowNotch.position;
        stringPosition.position = arrowNotchDefaultPoint.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        primaryInput = VRDevice.Device.PrimaryInputDevice;
        secondaryInput = VRDevice.Device.SecondaryInputDevice;

        // there must be a better way to do this
        bowString.SetPosition(0, topStringPos.position);
        bowString.SetPosition(1, stringPosition.position);
        bowString.SetPosition(2, bottomStringPos.position);

        if (holdingHand == rightHand && primaryInput.GetButton(VRButton.Trigger)== false || holdingHand == leftHand && secondaryInput.GetButton(VRButton.Trigger) == false)
        {
            BowDropped();
        }

        //if (Input.GetKeyDown("mouse 1")) // debug
        //{
        //    ShootDebugArrow();
        //}
        //if (Input.GetKeyDown("mouse 0"))
        //{
        //    powerUpCS.GetShotInfo();
        //}

        if (bowIsHeld)
        {
            UpdateBowPosition();
            if (isStringHeld)
            {
                pullValue = CalculatePull(pullingHand.transform);
                pullValue = Mathf.Clamp(pullValue, 0f, 1f);

                arrowNotch.position = pullingHand.transform.position;
                stringPosition.position = pullingHand.transform.position;


                animator.SetFloat("Blend", pullValue);

                if (pullingHand == rightHand && primaryInput.GetButton(VRButton.Trigger) == false || pullingHand == leftHand && secondaryInput.GetButton(VRButton.Trigger) == false)
                    
                {
                    //enable hand mesh
                    // disable string pull hand
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

    
    public void ReadyPull(GameObject hand)
    {
        float distance = Vector3.Distance(hand.transform.position, arrowGrabPoint.position);
        
        if(distance < grabThreshold)
        {
            // grab string effect
            if (pullingHand == rightHand && primaryInput.GetButton(VRButton.Trigger) || pullingHand == leftHand && secondaryInput.GetButton(VRButton.Trigger))
            
            {
                //disable hand mesh
                //Enable string pull hand
                isStringHeld = true;
            }
        } else { return; }
    }

    public void Release()  
    {
        if (pullValue > 0.5f)
        {
            FireArrow(pullValue);
            AudioClip releaseSFX = releaseSoundsArray[Random.Range(0, releaseSoundsArray.Length)];
            releaseAudioSource.PlayOneShot(releaseSFX);
            arrowMesh.enabled = false;
            arrowNotch.position = arrowNotchDefaultPoint.position;
            stringPosition.position = arrowNotchDefaultPoint.position;
            powerUpCS.GetShotInfo();
            pullValue = 0;
            animator.SetFloat("Blend", 0.0f);

            StartCoroutine(CreateDummyArrow(0.25f));

        } else
        {
            pullValue = 0;
            animator.SetFloat("Blend", 0.0f);
            arrowNotch.position = arrowNotchDefaultPoint.position;
            stringPosition.position = arrowNotchDefaultPoint.position;
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
        
    }

    private void BowDropped()
    {
        holdingHand = null;
        pullingHand = null;
        anchorOffset = null;
        bowIsHeld = false;
        rightHandMesh.enabled = true;
        leftHandMesh.enabled = true;
    }

    public void SetRightHand()
    {
        holdingHand = rightHand;
        pullingHand = leftHand;
        anchorOffset = rightAnchor;
        bowIsHeld = true;
        rightHandMesh.enabled = false;
    }
    public void SetLeftHand()
    {
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
    private void ShootDebugArrow() 
    {
        pullValue = 1;
        FireArrow(pullValue);
        powerUpCS.GetShotInfo();
        pullValue = 0;
    }
}
