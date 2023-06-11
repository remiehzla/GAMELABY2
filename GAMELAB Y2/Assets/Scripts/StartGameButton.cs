using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameButton : MonoBehaviour
{
    //build number of scene to start when start button is pressed
    public int gameStartScene;
    // storing on the script


    //whats being triggered on click and loading the scene by value or name
    public void StartGame()
    
        // var can be a global variable in a bigger project with many startup scenes
    {

        SceneManager.LoadScene(gameStartScene);
    }
}
