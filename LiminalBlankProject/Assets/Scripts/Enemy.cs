using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public ParticleSystem entryParticle;
    public ParticleSystem deathParticles;
    public GameObject deathSFX;

    private float currentScore = 15f;
    private int passoutScore;

    private void Awake()
    {
        entryParticle.Play();
        
    }
    private void Update()
    {
        if (currentScore > 0)
        {
            currentScore -= Time.deltaTime;
            passoutScore = Mathf.RoundToInt(currentScore);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Arrow"))
        {
            Instantiate(deathSFX, transform.position, Quaternion.identity);
            Instantiate(deathParticles, transform.position, Quaternion.identity);
            TeleportNode.enemiesKilled++;
            ScoreManager.playerScore += passoutScore;
            Debug.Log("EnemyDead");
            Destroy(other.gameObject);
            Destroy(gameObject);
        }

        if (other.CompareTag("Player"))
        {
            Instantiate(deathSFX, transform.position, Quaternion.identity);
            Instantiate(deathParticles, transform.position, Quaternion.identity);
            TeleportNode.enemiesKilled++;
            ScoreManager.playerScore -= 8;
            Debug.Log("EnemyDead");
            Destroy(gameObject);
        }
    }


}
