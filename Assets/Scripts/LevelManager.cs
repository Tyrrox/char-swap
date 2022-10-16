using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// LevelManager controls all forms of total level activities.

public class LevelManager : MonoBehaviour
{
    public static LevelManager levelManager { get; private set; }

    public bool levelComplete = false;

    private Timer levelTimer;

    void Start()
    {
        if (levelManager != null && levelManager != this)
        {
            Destroy(this);
        }
        else
        {
            levelManager = this;
        }
        levelTimer = GetComponent<Timer>();
        levelTimer.StartTimer();
    }

    void Update()
    {
        
    }

    public void StartLevelTimer()
    {
        levelTimer.StartTimer();
    }

    public void StopLevelTimer()
    {
        levelTimer.StopTimer();
    }

    public float GetLevelTime()
    {
        return levelTimer.OutputTime();
    }
}
