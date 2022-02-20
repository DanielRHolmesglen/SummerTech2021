using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartLevel : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        ScoreManager.playerScore = 0;
        BossDeath.bossDefeated = false;
        Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
    }


}
