using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Transition from main menu to gameplay scene
    /// </summary>
    public void StartGame()
    {
        //SceneManager.LoadScene("MainGame");
    }

    /// <summary>
    /// Quit the game from the main menu
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }

    /// <summary>
    /// Transition from gameplay scene to game over scene when player dies.
    /// Transition from gameplay scene to game over scene when player manually gives up from pause.
    /// </summary>
    public void GameOver()
    {
        SceneManager.LoadScene("GameOverScene");
    }

    /// <summary>
    /// Transition from game over scene back to gameplay scene
    /// </summary>
    public void RestartGame()
    {
        //Make sure to reset any objects in DontDestroyOnLoad
        //SceneManager.LoadScene("MainGame");
    }

    /// <summary>
    /// Transition from game over scene back to main menu
    /// </summary>
    public void QuitToMain()
    {
        //Make sure to destroy any objects in DontDestroyOnLoad
        SceneManager.LoadScene("StartScene");
    }
}
