using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScreenScript : MonoBehaviour
{

    bool paused = false;
    [SerializeField] GameObject PauseScreen;
    [SerializeField] GameObject GameScreen;
    [SerializeField] GameObject cameraToDisable;
    CursorLockMode lockState;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
            {
                CloseMenu();
            }
            else
            {
                OpenMenu();
            }
        }
    }

    private void OpenMenu()
    {
        // Free the cursor before presenting the menu
        lockState = Cursor.lockState;
        Cursor.lockState = CursorLockMode.None;

        // Enable the pause screen overlay, and remove the game screen overlay
        PauseScreen.SetActive(true);
        GameScreen.SetActive(false);

        // Set the timescale of the game to 0 so that nothing happens when paused!
        Time.timeScale = 0f;
        cameraToDisable.SetActive(false);
        paused = true;
    }

    public void CloseMenu()
    {
        Cursor.lockState = lockState;
        PauseScreen.SetActive(false);
        GameScreen.SetActive(true);

        // Return to normal time
        Time.timeScale = 1f;
        cameraToDisable.SetActive(true);
        paused = false;
    }

    public void ExitGame()
    {
        Debug.Log("Quitting");
        Application.Quit();
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("StartMenu");
        Time.timeScale = 1f;
        paused = false;
    }
}
