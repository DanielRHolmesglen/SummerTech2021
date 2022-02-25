using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public GameObject pointUI;
    public GameObject pointLossUI;
    public ParticleSystem entryParticle;
    public ParticleSystem deathParticles;
    public GameObject deathSFX;
    private int enemyPointValue;
    private float pointTimer;

    private void Awake()
    {        
        entryParticle.Play();
    }

    private void Update()
    {
        pointTimer += Time.deltaTime;
        if (pointTimer <= 1f)
        {
            enemyPointValue = 250;
        }
        if (pointTimer > 1f && pointTimer <= 3f)
        {
            enemyPointValue = 200;
        }
        if (pointTimer > 3f && pointTimer <= 4f)
        {
            enemyPointValue = 150;
        }
        if (pointTimer > 4f && pointTimer <= 5f)
        {
            enemyPointValue = 100;
        }
        if (pointTimer > 5f)
        {
            enemyPointValue = 50;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Arrow"))
        {
            GameObject points = Instantiate(pointUI, transform.position, Quaternion.identity) as GameObject;
            points.transform.GetChild(0).GetComponent <Text>().text = enemyPointValue.ToString();
            Instantiate(deathSFX, transform.position, Quaternion.identity);
            Instantiate(deathParticles, transform.position, Quaternion.identity);
            TeleportNode.enemiesKilled++;
            ScoreManager.playerScore += enemyPointValue;
            Debug.Log("EnemyDead");
            Destroy(other.gameObject);
            Destroy(gameObject);
        }

        if (other.CompareTag("Player"))
        {
            Instantiate(pointLossUI, transform.position, Quaternion.identity);
            Instantiate(deathSFX, transform.position, Quaternion.identity);
            Instantiate(deathParticles, transform.position, Quaternion.identity);
            TeleportNode.enemiesKilled++;
            ScoreManager.playerScore -= 100;
            Debug.Log("EnemyDead");
            Destroy(gameObject);
        }
    }
}
