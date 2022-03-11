using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public GameObject enemyPos;
    public Transform playerPos;
    public float enemySpeed;
    public GameObject pointUI;
    public GameObject pointLossUI;
    public float enemyStartMoveDelay;
    //public ParticleSystem entryParticle;
    //public ParticleSystem deathParticles;
    public GameObject deathSFX;
    private int enemyPointValue;
    private float pointTimer;
    private bool isMoving = false;
    //public static bool multiplierActive = false;

    //private float currentTime;
    // private int passoutScore;


    private void Start()
    {
        //playerPos = GameObject.FindWithTag("Player").transform;
        Invoke("MoveTrue", enemyStartMoveDelay);
    }
    private void Awake()
    {
        //entryParticle.Play();

    }
    private void Update()
    {
        pointTimer += Time.deltaTime;
        if (isMoving == true)
        {
            StartMove();
        }
        
    }
    private void GetPoints()
    {
        if (pointTimer <= 1.5f)
        {
            enemyPointValue = 250;
            return;
        }
        if (pointTimer > 1.5f && pointTimer <= 3f)
        {
            enemyPointValue = 200;
            return;
        }
        if (pointTimer > 3f && pointTimer <= 5f)
        {
            enemyPointValue = 150;
            return;
        }
        if (pointTimer > 5f && pointTimer <= 7f)
        {
            enemyPointValue = 100;
            return;
        }
        if (pointTimer > 7f)
        {
            enemyPointValue = 50;
            return;
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Arrow"))
        {
            GetPoints();
            Instantiate(deathSFX, transform.position, Quaternion.identity);
            TeleportNode.enemiesKilled++;
            enemyPointValue = enemyPointValue * PowerUp.multiplerAmount;
            ScoreManager.playerScore += enemyPointValue;
            GameObject points = Instantiate(pointUI, transform.position, Quaternion.identity) as GameObject;
            points.transform.GetChild(0).GetComponent<Text>().text = enemyPointValue.ToString();
            //Debug.Log("EnemyDead");
            Destroy(other.gameObject);
            Destroy(gameObject);
        }

        if (other.CompareTag("Player"))
        {
            Instantiate(pointLossUI, transform.position, Quaternion.identity);
            TeleportNode.enemiesKilled++;
            ScoreManager.playerScore -= 100;
            //Debug.Log("EnemyDead");
            Destroy(gameObject);
        }
    }

    void StartMove()
    {
        enemyPos.transform.position = Vector3.Lerp(enemyPos.transform.position, playerPos.transform.position, enemySpeed * Time.deltaTime);
        
    }

    void MoveTrue()
    {
        isMoving = true;
    }
}
