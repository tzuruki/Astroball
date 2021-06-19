using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static PlayerStats;

public class RenderGUI : MonoBehaviour
{
    public string scoreText;
    private Text textComponent;
    private bool isLocked = true;

    void Awake()
    {
        textComponent = GetComponent<Text>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        string textBlock = "Score: " + PlayerStats.Points + "\n";
        textBlock += "Keys: \n";
        foreach (DoorKey key in PlayerStats.GetKeys())
        {
            textBlock += key.colour + " Key" + " \n";
        }

        textComponent.text = textBlock;

        // Press g to unlock the cursor
        if (Input.GetKey(KeyCode.G))
        {
            if (isLocked)
            {
                Cursor.lockState = CursorLockMode.None;
                isLocked = !isLocked;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                isLocked = !isLocked;
            }

        }

        if (Input.GetKey(KeyCode.G) && isLocked)
        {

        }
    }
}
