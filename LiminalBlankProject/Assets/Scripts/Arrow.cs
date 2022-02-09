using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public CapsuleCollider arrowCollider;
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
        
    }

    void FixedUpdate()
    {
        rb.MoveRotation(Quaternion.LookRotation(rb.velocity, transform.up));

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
