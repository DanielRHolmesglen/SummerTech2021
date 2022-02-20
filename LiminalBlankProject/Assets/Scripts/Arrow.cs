using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public CapsuleCollider arrowCollider;
    public Transform tip;
    //public Transform arrowSpawnPoint;

    private Rigidbody rb;
    private Vector3 lastPosition = Vector3.zero;
    private MeshRenderer arrowMesh;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        arrowMesh = GetComponent<MeshRenderer>();
        //transform.rotation = arrowSpawnPoint.rotation;

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
        Destroy(gameObject);
        Debug.Log("Arrow Destroyed");
    }
}
