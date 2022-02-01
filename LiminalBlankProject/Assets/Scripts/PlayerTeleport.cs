using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleport : MonoBehaviour
{
    public Transform playerPos;
    public Transform playerTeleportLocation;

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Arrow")
        {
            playerPos.position = playerTeleportLocation.position;
            Destroy(gameObject);
        }

    }



}
