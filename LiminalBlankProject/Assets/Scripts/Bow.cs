using System.Collections;
using System.Collections.Generic;
using Liminal.SDK.VR;
using Liminal.SDK.VR.Input;
using UnityEngine;

public class Bow : MonoBehaviour, IGrabbable
{
    public Arrow ArrowPrefab = null; // for instatiating the shot arrow
    public GameObject ArrowMesh; // for holding an arrow in the bow

    public float grabThreshold = 0.15f;
    public BoxCollider arrowGrabPoint;
    public BoxCollider bowGrabPoint;
    public Transform fullDrawPoint;
    public Transform startDrawPoint;
    public Transform arrowSocket;

    private Transform holdingHand;
    private Transform pullingHand;
    private GameObject currentArrow; //'Arrow' in tutorial?
    private Animator animator;

    [SerializeField]private float pullValue = 0.0f;

    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(CreateDummyArrow(0.0f));
    }

    // Update is called once per frame
    void Update()
    {
        if (!pullingHand || !ArrowPrefab) return;
        pullValue = CalculatePull(pullingHand);
        pullValue = Mathf.Clamp(pullValue, 0, 1);

        animator.SetFloat("Blend", pullValue);
    }

    private float CalculatePull(Transform pullHand)
    {
        Vector3 direction = fullDrawPoint.position - startDrawPoint.position;
        float magnitude = direction.magnitude;

        direction.Normalize();
        Vector3 difference = pullHand.position - startDrawPoint.position;

        return Vector3.Dot(difference, direction) / magnitude;
    }

    private IEnumerator CreateDummyArrow(float waitTime)
    {
        
        // play arrow spawn particle fx
        yield return new WaitForSeconds(waitTime);

        GameObject arrowObject = Instantiate(ArrowMesh, arrowSocket);
        
        arrowObject.transform.localPosition = new Vector3(0, 0, 0.425f); // need to confirm location
        arrowObject.transform.localEulerAngles = Vector3.zero;
        
        currentArrow = arrowObject.GetComponent<GameObject>();
    }

    public void Pull(Transform hand)
    {
        float distance = Vector3.Distance(hand.position, startDrawPoint.position);

        if (distance > grabThreshold) return;

        pullingHand = hand;
    }

    public void Release()
    {
        if (pullValue > 0.25f) FireArrow();

        pullingHand = null;

        pullValue = 0;
        animator.SetFloat("Blend", 0);

        if (!currentArrow)
            StartCoroutine(CreateDummyArrow(0.25f));
    }
    private void FireArrow()
    {
        ArrowPrefab.Fire(pullValue);
        ArrowPrefab = null;
    }
    public void Grab()
    {
        //bowGrabPoint = GetComponent<BoxCollider>();
    }
}
