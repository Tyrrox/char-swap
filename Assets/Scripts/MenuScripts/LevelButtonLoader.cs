using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelButtonLoader : MonoBehaviour
{
    [SerializeField] string levelToLoad;

    public void LoadLevel()
    {
        if (levelToLoad == "")
        {
            Debug.Log("Attempted to load level but no level specified.");
        }
        else
        {
            SceneManager.LoadScene(levelToLoad);
        }
    }
}
