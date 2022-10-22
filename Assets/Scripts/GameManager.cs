using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // GameManager initialisation, static, prevent duplicates
    public static GameManager gameManager { get; private set; }

    public static bool GamePaused = false;

    private bool debugMode = false;

    // TODO: Pick characters in main menu
    [SerializeField] private GameObject[] playerCharacterPrefabs;

    private void Awake()
    {
        if (gameManager != null && gameManager != this)
        {
            Destroy(this);
        }
        else
        {
            gameManager = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Update()
    {
        // Take input for swapping to debug mode.
        if (Input.GetKeyDown(KeyCode.Equals))
        {
            debugMode = !debugMode;
        }
    }

    public void PauseGame()
    {
        GamePaused = true;
        Time.timeScale = 0f;

        // Unlock cursor during pause
        Cursor.lockState = CursorLockMode.None;
    }

    public void UnpauseGame()
    {
        GamePaused = false;
        Time.timeScale = 1f;
    }

    public bool IsDebugMode()
    {
        return debugMode;
    }

    public GameObject[] GetPlayerCharacterPrefabs()
    {
        return playerCharacterPrefabs;
    }
}
