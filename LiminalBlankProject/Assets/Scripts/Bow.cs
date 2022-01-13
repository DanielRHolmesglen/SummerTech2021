using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    public GameObject ArrowPrefab; // for instatiating the shot arrow
    public GameObject ArrowMesh; // for holding the arrow in the bow

    public float grabThreshold = 0.15f;
    public Transform arrowGrabPoint;
    public Transform fullDrawPoint;
    public Transform arrowSocket;

    private Transform pullingHand;
    private GameObject currentArrow; //'Arrow' in tutorial?
    private Animator animator;

    private float pullValue = 0.0f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CreateArrow(0.0f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator CreateArrow(float waitTime)
    {
        // wait
        yield return new WaitForSeconds(waitTime);

        // create, child
        //orient
        //set
    }

    public void Pull(Transform hand)
    {

    }

    public void Release()
    {

    }
}
