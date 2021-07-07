using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathScreenScript : MonoBehaviour
{
    [SerializeField] private GameObject fadeInPanel;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject gameScreen;

    Image fadeInPanelImage;

    private void Start()
    {
        fadeInPanelImage = fadeInPanel.GetComponent<Image>();
    }

    private void FixedUpdate()
    {
        if(IsPlayerDead())
        {

            Color fadeInPanelColour = fadeInPanelImage.color;
            float fadeAmount = fadeInPanelColour.a + (5f * Time.deltaTime);
            fadeInPanelColour = new Color(fadeInPanelColour.r, fadeInPanelColour.g, fadeInPanelColour.b, fadeAmount);
            fadeInPanelImage.color = fadeInPanelColour;
            Debug.Log(fadeInPanelColour.a);

            // then once fully faded in, display the gameOverScreen
            if (fadeInPanelColour.a >= 1)
            {
                gameOverScreen.SetActive(true);
                fadeInPanelColour.a = 0;
                Cursor.lockState = CursorLockMode.None;
                gameScreen.SetActive(false);
                Time.timeScale = 0f;
            }
        }
    }

    private bool IsPlayerDead()
    {
        return PlayerStats.Health <= 0;
    }
}
