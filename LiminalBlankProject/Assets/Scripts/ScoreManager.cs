using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static int playerScore;
    public GameObject powerUpEndSFX;
    private bool endMulti = false;
    public Text score;
    public Text gameTime;
    public Text endScore;
    public Text endGameTime;
    public float timeValue;
    public int currentScore;

    void Start()
    {
        endScore.text = score.text;
        endGameTime.text = gameTime.text;
    }

 
    void Update()
    {
        
        if (BossDeath.bossDefeated == false)
        {
            timeValue += Time.deltaTime;
            DisplayTime(timeValue);


            currentScore = playerScore;
            score.text = currentScore.ToString();
        }

        endScore.text = score.text;
        endGameTime.text = gameTime.text;

        if (playerScore <= 0)
        {
            playerScore = 0;
        }
        if (Enemy.multiplierActive == true)
        {
            powerUpEndSFX.SetActive(false);
            Invoke("StopMultiplier", PowerUp.multiplerTimeLength);
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        gameTime.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void StopMultiplier()
    {
        powerUpEndSFX.SetActive(true);
        Enemy.multiplierActive = false;
    }

}
