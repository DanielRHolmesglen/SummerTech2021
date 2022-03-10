using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportNode : MonoBehaviour
{
    //public GameObject timerTxt;
    public GameObject player;
    public GameObject playerHead;
    // public Transform playerPos;
    //public GameObject[] powerUpType;
    //public List<GameObject> powerUpSpawnPositions;
    public GameObject[] enemyType;
    public GameObject nextNode;
    public GameObject waveMusic;
    // public Transform playerTeleportLocation;
    public List<GameObject> spawnPositions;
    private int spawnPositionsSize;
    //private int powerUpSpawnPositionsSize;
    public int numberOfEnemiesToSpawn;
    private int enemiesSpawned;
    //public int speedOfEnemies;
    public float enemySpawnTimer;
   // public float enemyStartMoveDelay;
    public static int enemiesKilled;
    //private int rndSpawnTime;
    bool startStage;
    bool readyToSpawn;

    [Header("Skybox settings")]
    public Material skybox;
    public Color skyTintColour;
    public Color ambientWorldColour;
    [Tooltip("set between 0.55 - 1.4")]
    public float skyExposure;

    private void Start()
    {
        //rndSpawnTime = Random.Range(5,25);
        spawnPositionsSize = spawnPositions.Count;
        //powerUpSpawnPositionsSize = powerUpSpawnPositions.Count;
        waveMusic.SetActive(false);
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
            skybox.SetColor("_Tint", skyTintColour);
            skybox.SetFloat("_Exposure", skyExposure);
            RenderSettings.ambientLight = ambientWorldColour;
            //timerTxt.SetActive(true);           
            StageTimer.timerOn = true;
            //Invoke("PowerUpSpawn", rndSpawnTime);
            Invoke("TimerForFirstSpawn", 1.5f);
            waveMusic.SetActive(true);
            Debug.Log("PLayerHit");
        }

       // if (other.gameObject.tag == "Arrow")
       // {
       //     playerPos.position = playerTeleportLocation.position;
       // }

    }

    //public IEnumerator SendHoming(GameObject enemy)
    //{
    //    yield return new WaitForSeconds(enemyStartMoveDelay);
    //
    //
    //   //I think this is causing a lag spike?
    //   //may need to put it in an if statement instead
    //   //when the enemy is destroyed the while loop is still looking for the enemy.
    //   //and after the first one is killed it throws an error, possibly causing a lag spike, after that it ignores the error.
    //   while (enemy != null && Vector3.Distance(playerHead.transform.position, enemy.transform.position) > 0.3)
    //   {
    //       enemy.transform.position += (playerHead.transform.position - enemy.transform.position).normalized * speedOfEnemies * Time.deltaTime;
    //       enemy.transform.LookAt(playerHead.transform);
    //       yield return null;
    //   }
    //}

    void EnemySpawn()
    {
        GameObject enemy = Instantiate(enemyType[Random.Range(0, enemyType.Length)], spawnPositions[Random.Range(0, spawnPositionsSize)].transform.position, transform.rotation);
        enemy.GetComponent<Enemy>().playerPos = playerHead.transform;
        enemy.transform.LookAt(playerHead.transform);
        //  StartCoroutine(SendHoming(enemy));
       
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

    //void PowerUpSpawn()
    //{
    //    GameObject powerUpItem = Instantiate(powerUpType[Random.Range(0, powerUpType.Length)], powerUpSpawnPositions[Random.Range(0, powerUpSpawnPositionsSize)].transform.position, transform.rotation);       
    //}

}
