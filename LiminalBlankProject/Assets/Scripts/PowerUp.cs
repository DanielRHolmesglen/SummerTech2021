using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public GameObject powerUpSFX;
    public int multiplerAmounts;
    //public float multiplerTimeLengths;
    static public int multiplerAmount = 1;
    //static public float multiplerTimeLength;

    public int arrowHitTargetCount = 0;
    public int liveArrowHitCount;

    //private void Update()
    //{
    //    liveArrowHitCount = arrowHitTargetCount;
    //    multiplerAmounts = multiplerAmount;
    //}

    

    public void GetShotInfo()
    {
        Debug.Log("Arrow Shot");
        arrowHitTargetCount++;
        //if (arrowHitTargetCount > 1) Enemy.multiplierActive = true;
        if (arrowHitTargetCount > 2 && arrowHitTargetCount <= 5) multiplerAmount = 2; //Enemy.multiplierActive = true;
        if (arrowHitTargetCount > 5 && arrowHitTargetCount <= 10) multiplerAmount = 5;// Enemy.multiplierActive = true;
        if (arrowHitTargetCount > 10 && arrowHitTargetCount <= 20) multiplerAmount = 10;// Enemy.multiplierActive = true;
        if (arrowHitTargetCount > 20 && arrowHitTargetCount <= 50) multiplerAmount = 100;
        if (arrowHitTargetCount > 50) multiplerAmount = 1000;
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
            Debug.Log("Arrow Missed");
            arrowHitTargetCount = 0;
            Enemy.multiplierActive = false;
            //Destroy(gameObject);
        }

    }


}
