using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeath : MonoBehaviour
{
    public GameObject boss;
    public GameObject bossDeath;


    // Start is called before the first frame update
    void Start()
    {
        bossDeath.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Arrow")
        {
            boss.SetActive(false);
            bossDeath.SetActive(true);
        }

    }


}
