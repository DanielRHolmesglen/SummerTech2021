using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public ParticleSystem entryParticle;

    private void Start()
    {
        entryParticle.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Arrow")
        {
            TeleportNode.enemiesKilled++;
            TeleportNode.playerScore += 13;
            Debug.Log("EnemyDead");
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
