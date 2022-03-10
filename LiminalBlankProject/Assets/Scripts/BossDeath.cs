using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossDeath : MonoBehaviour
{
    public GameObject boss;
    public GameObject bossDeath;
    public GameObject pointUI;
    public static bool bossDefeated = false;
    private int bossPoints = 2500;


    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Arrow")
        {
            boss.SetActive(false);
            bossDeath.SetActive(true);
            bossDefeated = true;
            bossPoints = bossPoints * PowerUp.multiplerAmount;
            GameObject points = Instantiate(pointUI, transform.position, Quaternion.identity);
            //points.transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
            points.transform.GetChild(0).GetComponent<Text>().text = bossPoints.ToString();
        }

    }


}
