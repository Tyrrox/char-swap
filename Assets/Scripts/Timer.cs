using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Simple timer that counts up, used for timing levels.
public class Timer : MonoBehaviour
{
    private bool timerRunning = false;
    private float totalTime = 0f;

    // Update is called once per frame
    void Update()
    {
        if (timerRunning)
        {
            totalTime += Time.deltaTime;
        }
    }

    public void StartTimer()
    {
        timerRunning = true;
    }

    public void StopTimer()
    {
        timerRunning = false;
    }

    public float OutputTime()
    {
        return totalTime;
    }
}
