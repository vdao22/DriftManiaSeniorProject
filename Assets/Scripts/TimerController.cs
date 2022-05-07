using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    public Text timerText;
    private float startTime;
    TimeSpan timePlaying;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time; // Time that the application starts
        //lap = 0;
    }

    // Update is called once per frame
    void Update()
    {

        float elapsedTime = Time.time - startTime; // Store the time since the timer started

        timePlaying = TimeSpan.FromSeconds(elapsedTime);
        string timePlayingStr = "Time: " + timePlaying.ToString("mm':'ss'.'ff");
        timerText.text = timePlayingStr;

    }
}
