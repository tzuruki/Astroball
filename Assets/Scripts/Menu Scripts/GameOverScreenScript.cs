using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreenScript : MonoBehaviour
{
    [SerializeField] GameObject GameScreen;
    [SerializeField] GameObject PauseScreen;
    [SerializeField] GameObject GameOverScreen;

    private void FixedUpdate()
    {
        
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
    }

    public void RetryLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
        PauseScreen.SetActive(false);
        GameScreen.SetActive(true);
        GameOverScreen.SetActive(false);
    }
}
