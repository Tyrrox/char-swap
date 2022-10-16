using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuBehaviour : MonoBehaviour
{
    public void ExitGame()
    {
        Debug.Log("Quitting Game.");
        Application.Quit();
    }
}
