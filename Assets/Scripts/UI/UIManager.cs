using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public SceneTransition sceneTransition;
    public GameObject pauseMenu;

    private string levelToOpen;

    private void OnEnable()
    {
        //actions
        Actions.LevelSelectButtonPressed += SetLevelToOpen;
    }
    private void DisEnable()
    {
        //actions
        Actions.LevelSelectButtonPressed -= SetLevelToOpen;
    }

    //receive the button event and open a level with its name
    private void SetLevelToOpen(GameObject buttonSelected)
    {
        levelToOpen = buttonSelected.name;
    }
    public void StartMap()
    {
        sceneTransition.LoadScene(levelToOpen);
        Debug.Log("opening " + levelToOpen);
    }

    //go to level select screen, from main menu
    public void LevelSelect()
    {
        sceneTransition.LoadScene("LevelSelect");
    }

    //pause and resume game
    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }
    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    //back to main menu
    public void MainMenu()
    {
        Time.timeScale = 1f;
        sceneTransition.LoadScene("MainMenu");
    }
    
    //quit
    public void QuitGame()
    {
        Time.timeScale = 1f;
        Application.Quit();
    }

}
