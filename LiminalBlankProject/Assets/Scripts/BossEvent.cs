using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEvent : MonoBehaviour
{
    public GameObject player;
    public GameObject bossSpawn;
    public GameObject waveSpawner;



    // Start is called before the first frame update
    void Start()
    {
        waveSpawner.SetActive(false);
        bossSpawn.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            bossSpawn.SetActive(true);
            waveSpawner.SetActive(true);
        }


    }
}
