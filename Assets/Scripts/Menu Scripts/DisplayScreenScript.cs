using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayScreenScript : MonoBehaviour
{

    [SerializeField] private GameObject fadeInPanelDeath;
    [SerializeField] private GameObject screenToDisplayDeath;
    [SerializeField] private GameObject gameScreen;

    [SerializeField] private GameObject fadeInPanelWin;
    [SerializeField] private GameObject screenToDisplayWin;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (PlayerStats.Health <= 0)
        {
            DisplayScreen(screenToDisplayDeath, gameScreen, fadeInPanelDeath);
        }

        if (PlayerStats.HasCollectedShipPart)
        {
            DisplayScreen(screenToDisplayWin, gameScreen, fadeInPanelWin);
        }
    }

    private void DisplayScreen(GameObject screenToDisplay, GameObject gameScreen, GameObject fadeInPanel)
    {
        Image fadeInPanelImage = fadeInPanel.GetComponent<Image>();

        Color fadeInPanelColour = fadeInPanelImage.color;
        float fadeAmount = fadeInPanelColour.a + (5f * Time.deltaTime);
        fadeInPanelColour = new Color(fadeInPanelColour.r, fadeInPanelColour.g, fadeInPanelColour.b, fadeAmount);
        fadeInPanelImage.color = fadeInPanelColour;
        Debug.Log(fadeInPanelColour.a);

        // then once fully faded in, display the gameOverScreen
        if (fadeInPanelColour.a >= 1)
        {
            screenToDisplay.SetActive(true);
            fadeInPanelColour.a = 0;
            Cursor.lockState = CursorLockMode.None;
            gameScreen.SetActive(false);
            Time.timeScale = 0f;
        }
    }

}
