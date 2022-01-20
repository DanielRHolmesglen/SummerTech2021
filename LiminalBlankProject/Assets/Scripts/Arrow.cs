using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public BoxCollider arrowCollider;
    // Start is called before the first frame update

    public float moveSpeed = 2000f;
    public Transform tip;

    private Rigidbody rb;
    private bool isStopped = true;
    private Vector3 lastPosition = Vector3.zero;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isStopped) return;

        rb.MoveRotation(Quaternion.LookRotation(rb.velocity, transform.up));

        if (Physics.Linecast(lastPosition, tip.position))
        {
            Hit();
        }

        lastPosition = tip.position;
    }

    private void Hit() // turn into destroy and particle fx
    {
        isStopped = true;

        rb.isKinematic = true;
        rb.useGravity = false;
    }

    public void Fire(float pullValue)
    {
        isStopped = false;
        transform.parent = null;

        rb.isKinematic = false;
        rb.useGravity = true;
        rb.AddForce(transform.forward * (pullValue * moveSpeed));

        Destroy(gameObject, 2.5f);
    }
}
