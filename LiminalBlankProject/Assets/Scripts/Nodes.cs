using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nodes : MonoBehaviour
{

    public GameObject player;
    public GameObject stagetimer;
    public GameObject secondNode;
    public GameObject thirdNode;

    void Start()
    {
        stagetimer.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            stagetimer.SetActive(true);
            StageTimer.timerOn = true;
            Invoke("StageOneStart", 4.0f);                       
        }
    }

    void StageOneStart()
    {
        stagetimer.SetActive(false);
        EnemyManager.spawnActive01 = true;
        Destroy(gameObject);
    }

    void StageTwoStart()
    {
        stagetimer.SetActive(false);
        EnemyManager.spawnActive02 = true;
        Destroy(gameObject);
    }

    void StageThreeStart()
    {
        stagetimer.SetActive(false);
        EnemyManager.spawnActive03 = true;
        Destroy(gameObject);
    }

}
