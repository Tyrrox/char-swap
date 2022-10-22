using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuBehaviour : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !LevelManager.levelManager.levelComplete)
        {
            if (GameManager.GamePaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    void PauseGame()
    {
        pauseMenu.SetActive(true);
        GameManager.gameManager.PauseGame();
        LevelManager.levelManager.StopLevelTimer();
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        GameManager.gameManager.UnpauseGame();
        LevelManager.levelManager.StartLevelTimer();
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void LoadMainMenu()
    {
        GameManager.gameManager.UnpauseGame();
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadLevelSelect()
    {
        Debug.Log("Loading Character Select...");
    }
}
