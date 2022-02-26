using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private Transform tip;
    
    private Rigidbody rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }
    void FixedUpdate()
    {
        rb.MoveRotation(Quaternion.LookRotation(rb.velocity, transform.up));

        //if (Physics.Linecast(lastPosition, tip.position))
        //{
        //    Hit();
        //}
        //
        //lastPosition = tip.position;
    }
    //private void Hit()
    //{
    //    Destroy(gameObject);
    //   /Debug.Log("Arrow Destroyed");
    //}
}
