using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//pressing Esc to pause
public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    //public so it can be accessed by other scripts and being able to check from other scripts if the game is paused

    // pause menu introduction
    public GameObject pauseMenuUI;
    private void Update()
    {
        //
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();

            } 
            else
            {
                Pause();

            }
            
                
        }
    }
    // creating the methods of pausing and resuming
    public void Resume()
    {
        //stopping the time of the game when pressed pause
        pauseMenuUI.SetActive(false);// disabling pause menu
        Time.timeScale = 1f; // speed passing at normal rate
        GameIsPaused = false;

    }
     void Pause()
    {
        //enabling and disabling the game object
        pauseMenuUI.SetActive(true); //enabled pause menu
        Time.timeScale = 0f; // speed of time passing
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        //Debug.Log("Loading menu..."); //to display what is happening
        SceneManager.LoadScene("MenuSceneMery");
    }
    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
