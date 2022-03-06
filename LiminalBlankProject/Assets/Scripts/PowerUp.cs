using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    //public GameObject powerUpSFX;
    public int multiplerAmounts;
    [SerializeField] AudioClip powerUpSound;
    [SerializeField] AudioClip missSound;
    AudioSource sFXSource;
    //public float multiplerTimeLengths;
    static public int multiplerAmount = 1;
    //static public float multiplerTimeLength;

    public int arrowHitTargetCount = 0;
    public int liveArrowHitCount;
    int lastMultiplierAmount;

    private void Start()
    {
        sFXSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (lastMultiplierAmount < multiplerAmount)
        {
            sFXSource.PlayOneShot(powerUpSound);
            lastMultiplierAmount = multiplerAmount;
        }
    }

    public void GetShotInfo()
    {
        Debug.Log("Arrow Shot");
        arrowHitTargetCount++;

        if (arrowHitTargetCount > 2 && arrowHitTargetCount <= 5) 
        {
            multiplerAmount = 2;
        }
        if (arrowHitTargetCount > 5 && arrowHitTargetCount <= 10) 
        {
            multiplerAmount = 5;
        }
        if (arrowHitTargetCount > 10 && arrowHitTargetCount <= 20)
        {
            multiplerAmount = 10;
        }
        if (arrowHitTargetCount > 20 && arrowHitTargetCount <= 50) 
        {
            multiplerAmount = 100;
        }
        if (arrowHitTargetCount > 50) multiplerAmount = 1000;

    }

   

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Arrow"))
        {
            multiplerAmount = 1;
            lastMultiplierAmount = 1;
            Debug.Log("Arrow Missed");
            arrowHitTargetCount = 0;
            //Enemy.multiplierActive = false;
            sFXSource.PlayOneShot(missSound);
        }

    }


}
