﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageTimer : MonoBehaviour
{
    public static bool timerOn = false;
    public float startingTime = 3f;
    public float currentTime = 0f;
    public Text countdownDisplay;

    private void Start()
    {
        currentTime = 0f;
    }

    private void Update()
    {
        if (timerOn == true)
        {
            StartTimer();
        }
    }

    void StartTimer()
    {
        currentTime -= 1 * Time.deltaTime;
        countdownDisplay.text = currentTime.ToString("0");

        if (currentTime <= 0)
        {
            timerOn = false;
            currentTime = startingTime;
            gameObject.SetActive(false);
        }

        //if (currentTime <= 0.5)
       // {
       //     countdownDisplay.text = "GO!";
       // }

    }

}
