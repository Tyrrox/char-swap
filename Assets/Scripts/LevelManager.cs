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
        // Start the level timer for end of level time
        levelTimer = GetComponent<Timer>();
        levelTimer.StartTimer();

        // Lock the cursor during gameplay
        Cursor.lockState = CursorLockMode.Locked;
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
