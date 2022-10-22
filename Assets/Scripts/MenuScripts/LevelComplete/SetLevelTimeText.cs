using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetLevelTimeText : MonoBehaviour
{
    private TextMeshProUGUI levelTimeText;

    private void Start()
    {
        levelTimeText = GetComponent<TextMeshProUGUI>();
        SetLevelTime();
    }

    private void SetLevelTime()
    {
        float timeFloat = LevelManager.levelManager.GetLevelTime();

        int minutes = Mathf.FloorToInt(timeFloat / 60);
        int seconds = Mathf.FloorToInt(timeFloat % 60);
        int decimals = Mathf.FloorToInt((timeFloat - (int)timeFloat) * 100);

        levelTimeText.text = string.Format("{0:00}:{1:00}.{2:00}", minutes, seconds, decimals);
    }
}
