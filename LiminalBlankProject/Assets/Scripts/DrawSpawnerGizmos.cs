using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawSpawnerGizmos : MonoBehaviour
{
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(transform.position,0.5f);
    }
}
