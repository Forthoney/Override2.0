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
        SceneManager.LoadScene("UI Test");
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
        // TODO reset deltatime scale back to 1 in case user self destructed from pause screen.
        // TODO end score value through DontDestroyOnLoad
        SceneManager.LoadScene("GameOverScene");
    }

    /// <summary>
    /// Transition from game over scene back to gameplay scene
    /// </summary>
    public void RestartGame()
    {
        // TODO Make sure to reset/destroy any objects in DontDestroyOnLoad
        SceneManager.LoadScene("UI Test");
    }

    /// <summary>
    /// Transition from game over scene back to main menu
    /// </summary>
    public void QuitToMain()
    {
        // TODO Make sure to destroy any objects in DontDestroyOnLoad
        SceneManager.LoadScene("StartScene");
    }

    /// <summary>
    /// For Inputting pause through mouse button during pause.
    /// </summary>
    public void PauseButton()
    {
        InputController.Instance.Pausing ^= true;
    }
}
