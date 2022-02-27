using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
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
            Enemy.multiplierActive = true;
            Destroy(gameObject);
        }

    }


}
