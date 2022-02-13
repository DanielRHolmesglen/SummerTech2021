using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBreak : MonoBehaviour
{
    public GameObject shieldBreakSFX;

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Arrow")
        {
            Destroy(other.gameObject);
            Instantiate(shieldBreakSFX, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

    }
}
