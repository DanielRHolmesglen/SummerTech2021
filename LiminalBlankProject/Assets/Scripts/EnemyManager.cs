using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    public GameObject enemyPrefab;
    public List<GameObject> spawnPositions;
    public GameObject player;
    public int numberOfEnemies;
    public float speed = 1f;
    private int amountOfEnemiesSpawned = 0;
    private bool enemySpawned = false;
    public static bool spawnActive01 = false;
    public static bool spawnActive02 = false;
    public static bool spawnActive03 = false;


    void Start()
    {
        spawnActive01 = false;
        spawnActive02 = false;
        spawnActive03 = false;
        enemySpawned = false;
    }

    void FixedUpdate()
    {
        if(spawnActive01 == true)
        {          
            enemySpawned = true;
            EnemyWaitSpawnTimer();
            EnemySpawn01();
        }

        //if (spawnActive02 == true)
      //  {
       //     enemySpawned = true;
      //      EnemyWaitSpawnTimer();
      //      EnemySpawn02();
      //  }

       // if (spawnActive03 == true)
       // {
       //     EnemySpawn03();
       // }
    }

    public IEnumerator SendHoming(GameObject enemy)
    {
        while (Vector3.Distance(player.transform.position, enemy.transform.position) > 0.3)
        {
            enemy.transform.position += (player.transform.position - enemy.transform.position).normalized * speed * Time.deltaTime;
            enemy.transform.LookAt(player.transform);
            yield return null;
        }
    }

    void EnemySpawn01()
    {        
        GameObject enemy = Instantiate(enemyPrefab, spawnPositions[Random.Range(0, 6)].transform.position, enemyPrefab.transform.rotation);
        enemy.transform.LookAt(player.transform);
        StartCoroutine(SendHoming(enemy));      
    }
    void EnemySpawn02()
    {
        GameObject enemy = Instantiate(enemyPrefab, spawnPositions[Random.Range(7, 16)].transform.position, enemyPrefab.transform.rotation);
        enemy.transform.LookAt(player.transform);
        StartCoroutine(SendHoming(enemy));
    }
    void EnemySpawn03()
    {
        GameObject enemy = Instantiate(enemyPrefab, spawnPositions[Random.Range(17, 30)].transform.position, enemyPrefab.transform.rotation);
        enemy.transform.LookAt(player.transform);
        StartCoroutine(SendHoming(enemy));
    }

    void EndStageOne()
    {
        spawnActive01 = false;
    }

    void EnemyWaitSpawnTimer()
    {
        if(enemySpawned == true)
        {
            amountOfEnemiesSpawned += 1;
            spawnActive01 = false;
            Invoke("EnableSpawn01", 1.0f);
        }
    }

    void EnableSpawn01()
    {
        if( amountOfEnemiesSpawned > numberOfEnemies +1)
        {
            EndStageOne();
        }

        if (amountOfEnemiesSpawned < numberOfEnemies)
        {            
            spawnActive01 = true;
        }
    }

}
