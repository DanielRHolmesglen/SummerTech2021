using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public GameObject powerUpSFX;
    public int multiplerAmounts;
    public float multiplerTimeLengths;
    static public int multiplerAmount;
    static public float multiplerTimeLength;

    private void Start()
    {
        multiplerTimeLength = multiplerTimeLengths;
        multiplerAmount = multiplerAmounts;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Arrow"))
        {
            Instantiate(powerUpSFX, transform.position, Quaternion.identity);
            Enemy.multiplierActive = true;
            Destroy(gameObject);
        }

    }


}
