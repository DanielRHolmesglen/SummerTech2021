using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShieldManager : MonoBehaviour
{
    public GameObject shield01;
    public GameObject shield02;
    public GameObject shield03;
    public GameObject shield04;
    public GameObject redShield01;
    public GameObject redShield02;
    public GameObject redShield03;
    public GameObject redShield04;


    // Start is called before the first frame update
    void Start()
    {
        redShield01.SetActive(false);
        redShield02.SetActive(false);
        redShield03.SetActive(false);
        redShield04.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (TeleportNode.enemiesKilled == 5)
        {
            shield01.SetActive(false);
            redShield01.SetActive(true);
        }
        if (TeleportNode.enemiesKilled == 10)
        {
            shield02.SetActive(false);
            redShield02.SetActive(true);
        }
        if (TeleportNode.enemiesKilled == 15)
        {
            shield03.SetActive(false);
            redShield03.SetActive(true);
        }
        if (TeleportNode.enemiesKilled == 20)
        {
            shield04.SetActive(false);
            redShield04.SetActive(true);
        }
    }
}
