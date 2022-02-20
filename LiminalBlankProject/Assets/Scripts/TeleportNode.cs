using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportNode : MonoBehaviour
{
    public GameObject timerTxt;
    public GameObject player;
    public GameObject playerHead;
    // public Transform playerPos;
    public GameObject[] enemyType;
    public GameObject nextNode;
    public GameObject waveMusic;
    // public Transform playerTeleportLocation;
    public List<GameObject> spawnPositions;
    public int numberOfEnemiesToSpawn;
    private int enemiesSpawned;
    public int speedOfEnemies;
    public float enemySpawnTimer;
    public float enemyStartMoveDelay;
    public static int enemiesKilled;
    bool startStage;
    bool readyToSpawn;

    [Header("Skybow settings")]
    public Material skybox;
    public Color skyTintColour;
    public Color ambientWorldColour;
    [Tooltip("set between 0.55 - 1.4")]
    public float skyExposure;

    private void Start()
    {
        waveMusic.SetActive(false);
        timerTxt.SetActive(false);
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
            timerTxt.SetActive(true);           
            StageTimer.timerOn = true;
            Invoke("TimerForFirstSpawn", 1.5f);
            waveMusic.SetActive(true);
            Debug.Log("PLayerHit");
        }

       // if (other.gameObject.tag == "Arrow")
       // {
       //     playerPos.position = playerTeleportLocation.position;
       // }

    }

    public IEnumerator SendHoming(GameObject enemy)
    {
        yield return new WaitForSeconds(enemyStartMoveDelay);

        while (Vector3.Distance(playerHead.transform.position, enemy.transform.position) > 0.3)
        {
            enemy.transform.position += (playerHead.transform.position - enemy.transform.position).normalized * speedOfEnemies * Time.deltaTime;
            enemy.transform.LookAt(playerHead.transform);
            yield return null;
        }
    }

    void EnemySpawn()
    {
        GameObject enemy = Instantiate(enemyType[Random.Range(0, enemyType.Length)], spawnPositions[Random.Range(0, 6)].transform.position, transform.rotation);
        enemy.transform.LookAt(playerHead.transform);
        StartCoroutine(SendHoming(enemy));
       
        readyToSpawn = false;
        enemiesSpawned++;
        Invoke("SpawnEnemy", enemySpawnTimer);
        
    }

    void TimerForFirstSpawn()
    {
        timerTxt.SetActive(false);
        startStage = true;
        readyToSpawn = true;
    }

    void SpawnEnemy()
    {
        readyToSpawn = true;
    }

}
