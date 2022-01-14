using System.Collections;
using System.Collections.Generic;
using Liminal.SDK.VR;
using Liminal.SDK.VR.Input;
using UnityEngine;

public class Bow : MonoBehaviour
{
    public GameObject ArrowPrefab; // for instatiating the shot arrow
    public GameObject ArrowMesh; // for holding the arrow in the bow

    public float grabThreshold = 0.15f;
    public BoxCollider arrowGrabPoint;
    public BoxCollider bowGrabPoint;
    public Transform fullDrawPoint;
    public Transform startPoint;
    public Transform arrowSocket;

    private Transform pullingHand;
    private GameObject currentArrow; //'Arrow' in tutorial?
    private Animator animator;

    private float pullValue = 0.0f;

    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(CreateDummyArrow(0.0f));
    }

    // Update is called once per frame
    void Update()
    {
        if (!pullingHand || !currentArrow) return;
        pullValue = CalculatePull(pullingHand);
        pullValue = Mathf.Clamp(pullValue, 0, 1);

        animator.SetFloat("Blend", pullValue);
    }

    private float CalculatePull(Transform pullHand)
    {
        Vector3 direction = fullDrawPoint.position - startPoint.position;
        float magnitude = direction.magnitude;

        direction.Normalize();
        Vector3 difference = pullHand.position - startPoint.position;

        return Vector3.Dot(difference, direction) / magnitude;
    }

    private IEnumerator CreateDummyArrow(float waitTime)
    {
        // wait
        yield return new WaitForSeconds(waitTime);

        // create, child
        //orient
        //set
    }

    public void Pull(Transform hand)
    {
        float distance = Vector3.Distance(hand.position, startPoint.position);

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
        ArrowPrefab = null;
    }
}
