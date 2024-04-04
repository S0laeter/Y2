using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public SceneTransition sceneTransition;
    public GameObject pauseMenu;
    public GameObject settingsMenu;

    public void StartGame()
    {
        sceneTransition.LoadScene("");
        Debug.Log("starting game");
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        Debug.Log("pausing game");
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        Debug.Log("resuming game");
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        sceneTransition.LoadScene("MainMenu");
        Debug.Log("loading main menu");
    }

    public void Settings()
    {
        Debug.Log("loading options screen");
    }
    
    public void QuitGame()
    {
        Time.timeScale = 1f;
        Debug.Log("quit");
        Application.Quit();
    }

}
