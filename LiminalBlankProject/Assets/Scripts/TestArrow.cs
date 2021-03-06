using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestArrow : MonoBehaviour
{
    public BoxCollider arrowCollider;
    public Transform tip;

    private Rigidbody rb;
    private Vector3 lastPosition = Vector3.zero;
    private MeshRenderer arrowMesh;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        arrowMesh = GetComponent<MeshRenderer>();
    }
    private void Update()
    {
        rb.MoveRotation(Quaternion.LookRotation(rb.velocity, transform.up));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Physics.Linecast(lastPosition, tip.position))
        {
            Hit();
        }

        lastPosition = tip.position;
    }
    private void Hit()
    {
        // arrow hit particle
        arrowMesh.enabled = false;
    }

}
