using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeath : MonoBehaviour
{
    public GameObject boss;
    public GameObject bossDeath;
    public static bool bossDefeated = false;

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Arrow")
        {
            boss.SetActive(false);
            bossDeath.SetActive(true);
            bossDefeated = true;
        }

    }


}
