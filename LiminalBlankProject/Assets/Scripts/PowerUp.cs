using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public GameObject powerUpSFX;
    //public int multiplerAmounts;
    //public float multiplerTimeLengths;
    static public int multiplerAmount;
    //static public float multiplerTimeLength;

    public static int arrowHitTargetCount = 1;

    public void GetShotInfo()
    {
        arrowHitTargetCount++;
        //if (arrowHitTargetCount > 1) Enemy.multiplierActive = true;
        if (arrowHitTargetCount > 3 && arrowHitTargetCount <= 5) multiplerAmount = 2; //Enemy.multiplierActive = true;
        if (arrowHitTargetCount > 5 && arrowHitTargetCount <= 10) multiplerAmount = 5; //Enemy.multiplierActive = true;
        if (arrowHitTargetCount > 10) multiplerAmount = 10; //Enemy.multiplierActive = true;
    }

    private void Start()
    {
        //multiplerTimeLength = multiplerTimeLengths;
        //multiplerAmount = multiplerAmounts;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Arrow"))
        {
            //Instantiate(powerUpSFX, transform.position, Quaternion.identity);
            multiplerAmount = 1;
            //Enemy.multiplierActive = false;
            //Destroy(gameObject);
        }

    }


}
