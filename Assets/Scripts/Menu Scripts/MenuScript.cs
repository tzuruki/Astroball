using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("DebugLevelScene");
    }

    public void StartLevel1()
    {
        SceneManager.LoadScene("Level1GreyBox");
    }

}
