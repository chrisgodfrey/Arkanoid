using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void StartGame()
    {
        Debug.Log("Loading One Player game");
        SceneManager.LoadScene("Level1", LoadSceneMode.Single);
    }

    public void HowToPlay()
    {
        Debug.Log("Displaying How To Play Scene");
        SceneManager.LoadScene("HowToPlay", LoadSceneMode.Single);
    }

    public void Credits()
    {
        Debug.Log("Displaying Credits Scene");
        SceneManager.LoadScene("Credits", LoadSceneMode.Single);
    }

    public void ReturnToMainMenu()
    {
        Debug.Log("Returning to main menu");
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game");
        Application.Quit(); // quit game to OS (ignored in Unity Editor)
    }
}