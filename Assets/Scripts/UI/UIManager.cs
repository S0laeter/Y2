using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public SceneTransition sceneTransition;
    public GameObject pauseMenu;
    public GameObject settingsMenu;

    private string levelToOpen;

    private void OnEnable()
    {
        Actions.LevelSelectButtonPressed += SetLevelToOpen;
    }
    private void DisEnable()
    {
        Actions.LevelSelectButtonPressed -= SetLevelToOpen;
    }

    //this clusterfuck is just to select and open a level..
    private void SetLevelToOpen(string levelName)
    {
        levelToOpen = levelName;
    }
    public void Map1()
    {
        Actions.LevelSelectButtonPressed("Map1");
    }
    public void Map2()
    {
        Actions.LevelSelectButtonPressed("Map2");
    }
    public void Map3()
    {
        Actions.LevelSelectButtonPressed("Map3");
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

    //open settings menu
    public void Settings()
    {
        //put smt here..
    }
    
    //quit
    public void QuitGame()
    {
        Time.timeScale = 1f;
        Application.Quit();
    }

}
