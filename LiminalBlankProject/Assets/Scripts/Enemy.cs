using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public ParticleSystem entryParticle;
    public ParticleSystem deathParticles;
    public GameObject deathSFX;

    private void Awake()
    {
        entryParticle.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Arrow"))
        {
            Instantiate(deathSFX, transform.position, Quaternion.identity);
            Instantiate(deathParticles, transform.position, Quaternion.identity);
            TeleportNode.enemiesKilled++;
            TeleportNode.playerScore += 13;
            Debug.Log("EnemyDead");
            Destroy(other.gameObject); // not getting called on arrow?
            Destroy(gameObject);
        }
    }
}
