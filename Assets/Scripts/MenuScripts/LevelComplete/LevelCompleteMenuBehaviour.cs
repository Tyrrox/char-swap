using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelCompleteMenuBehaviour : MonoBehaviour
{
    // Remember that whenever this menu is on screen, the game is paused.
    // Every option must unpause the game.

    public void RetryLevel()
    {
        GameManager.gameManager.UnpauseGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void LoadMainMenu()
    {
        GameManager.gameManager.UnpauseGame();
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadLevelSelect()
    {
        Debug.Log("Loading Level Select...");
    }
}
