using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject optionsMenu;

    public bool gameIsPaused = false;
    public bool inOptions = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused && !inOptions)
            {
                Resume();
            }
            else if (!gameIsPaused && !inOptions)
                Pause();
            else if (gameIsPaused && inOptions)
            {
                pauseMenuUI.SetActive(true);
                optionsMenu.SetActive(false);
                Options();
            }
        }
    }

    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    public void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Options()
    {
        if (!inOptions)
        {
            inOptions = true;
        }
        else
        {
            inOptions = false;
        }
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
