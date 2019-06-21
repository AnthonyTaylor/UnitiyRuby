using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private bool isPaused;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
        }

        if (isPaused)
        {   
            ActivateMenu();
        }
        else
        {
            DeactivateMenu();
        }
    }

    /// <summary>
    /// Pauses the game
    /// </summary>
    void ActivateMenu()
    {
        Time.timeScale = 0;
        AudioListener.pause = true;
        pauseMenuUI.SetActive(true);
    }


    /// <summary>
    /// Unpauses the game
    /// </summary>
    public void DeactivateMenu()
    {
        Time.timeScale = 1;
        AudioListener.pause = false;
        pauseMenuUI.SetActive(false);
        isPaused = false; 
    }

    /// <summary>
    /// Exits the entire program
    /// </summary>
    public void closeGame()
    {
        Application.Quit();
    }
}
