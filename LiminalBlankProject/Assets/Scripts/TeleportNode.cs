using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportNode : MonoBehaviour
{
    public static int playerScore;
    public GameObject timerTxt;
    public GameObject player;
    public GameObject playerHead;
    // public Transform playerPos;
    public GameObject[] enemyType;
    public GameObject nextNode;
    //public GameObject waveMusic;
   // public Transform playerTeleportLocation;
    public List<GameObject> spawnPositions;
    public int numberOfEnemiesToSpawn;
    private int enemiesSpawned;
    public int speedOfEnemies;
    public float enemySpawnTimer;
    public static int enemiesKilled;
    bool startStage;
    bool readyToSpawn;

    private void Start()
    {
        //waveMusic.SetActive(false);
        //timerTxt.SetActive(false);
        nextNode.SetActive(false);
        startStage = false;
        readyToSpawn = false;
    }

    private void Update()
    {
        if (startStage == true & readyToSpawn == true)
        {
            EnemySpawn();
        }

        if (enemiesSpawned == numberOfEnemiesToSpawn)
        {
            startStage = false;
            readyToSpawn = false;
        }

        if(enemiesKilled == numberOfEnemiesToSpawn)
        {
            enemiesKilled = 0;
            nextNode.SetActive(true);
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            //timerTxt.SetActive(true);           
            StageTimer.timerOn = true;
            Invoke("TimerForFirstSpawn", 4.0f);
            //waveMusic.SetActive(true);
            Debug.Log("PLayerHit");
        }

       // if (other.gameObject.tag == "Arrow")
       // {
       //     playerPos.position = playerTeleportLocation.position;
       // }

    }

    public IEnumerator SendHoming(GameObject enemy)
    {
        while (Vector3.Distance(playerHead.transform.position, enemy.transform.position) > 0.3)
        {
            enemy.transform.position += (playerHead.transform.position - enemy.transform.position).normalized * speedOfEnemies * Time.deltaTime;
            enemy.transform.LookAt(playerHead.transform);
            yield return null;
        }
    }

    void EnemySpawn()
    {
        GameObject enemy = Instantiate(enemyType[Random.Range(0, enemyType.Length)], spawnPositions[Random.Range(0, 6)].transform.position, transform.rotation) as GameObject;
        enemy.transform.LookAt(playerHead.transform);
        StartCoroutine(SendHoming(enemy));
       
        readyToSpawn = false;
        enemiesSpawned++;
        Invoke("SpawnEnemy", enemySpawnTimer);
        
    }

    void TimerForFirstSpawn()
    {
        //timerTxt.SetActive(false);
        startStage = true;
        readyToSpawn = true;
    }

    void SpawnEnemy()
    {
        readyToSpawn = true;
    }

}
