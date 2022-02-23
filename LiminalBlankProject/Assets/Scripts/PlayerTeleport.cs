using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleport : MonoBehaviour
{
    public Transform playerPos;
    public Transform playerTeleportLocation;
    public GameObject fadeUI;
    public GameObject teleportSFX;
    private bool teleport = false;
    

    private void Start()
    {
        
        fadeUI.SetActive(false);
    }


    private void Update()
    {
        if (teleport == true)
        {
            teleportSFX.SetActive (true);
            fadeUI.SetActive(true);
            Debug.Log("Teleport");
            playerPos.position = Vector3.Lerp(playerPos.position, playerTeleportLocation.position, 0.4f);
            Invoke("StopTeleport", 0.45f);
        }

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Arrow")
        {
            teleport = true;
            Destroy(other.gameObject);
        }
        
    }

    void StopTeleport()
    {
        Invoke("EndFade", 0.45f);
        teleport = false;
        Destroy(gameObject);
    }

    void EndFade()
    {
        //teleportSFX.SetActive(false);
        //fadeUI.SetActive(false);
    }

}
