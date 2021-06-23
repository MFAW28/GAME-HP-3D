using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TimerController : MonoBehaviour
{
    public bool timerActive = false;
    public float currentTime;
    public int startTime;

    public TimeSpan time;

    void Start()
    {
        currentTime = startTime * 60;
    }

    void Update()
    {
        if(timerActive == true)
        {
            currentTime = currentTime - Time.deltaTime;
            if(currentTime <= 0)
            {
                StopTimer();
            }
        }
        time = TimeSpan.FromSeconds(currentTime);
        //place in QuizTesManagement
    }

    public void StartTimer()
    {
        timerActive = true;
    }

    public void StopTimer()
    {
        timerActive = false;
    }
}
