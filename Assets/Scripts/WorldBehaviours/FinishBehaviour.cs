using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject finishMenu;
    [SerializeField] private GameObject finishTimeText;

    // When the player finishes the level
    private void OnTriggerEnter2D(Collider2D collision)
    {
        LevelManager.levelManager.levelComplete = true;
        finishMenu.SetActive(true);
        GameManager.gameManager.PauseGame();
    }
}
