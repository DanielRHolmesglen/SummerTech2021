using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportNode : MonoBehaviour
{
    public static int playerScore;
    public GameObject timerTxt;
    public GameObject player;
    public GameObject enemyType;
    public GameObject nextNode;
    public List<GameObject> spawnPositions;
    public int numberOfEnemiesToSpawn;
    public int enemiesSpawned;
    public int speedOfEnemies;
    public float enemySpawnTimer;
    public static int enemiesKilled;
    bool startStage;
    bool readyToSpawn;

    private void Start()
    {
        timerTxt.SetActive(false);
        nextNode.SetActive(false);
        startStage = false;
        readyToSpawn = false;
    }

    private void FixedUpdate()
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
            nextNode.SetActive(true);
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            timerTxt.SetActive(true);
            startStage = true;
            readyToSpawn = true;

            Debug.Log("PLayerHit");
        }
    }

    public IEnumerator SendHoming(GameObject enemy)
    {
        while (Vector3.Distance(player.transform.position, enemy.transform.position) > 0.3)
        {
            enemy.transform.position += (player.transform.position - enemy.transform.position).normalized * speedOfEnemies * Time.deltaTime;
            enemy.transform.LookAt(player.transform);
            yield return null;
        }
    }

    void EnemySpawn()
    {
        GameObject enemy = Instantiate(enemyType, spawnPositions[Random.Range(0, 6)].transform.position, enemyType.transform.rotation);
        enemy.transform.LookAt(player.transform);
        StartCoroutine(SendHoming(enemy));
       
        readyToSpawn = false;
        enemiesSpawned++;
        Invoke("SpawnEnemy", enemySpawnTimer);
        
    }

    void SpawnEnemy()
    {
        readyToSpawn = true;
    }

}
