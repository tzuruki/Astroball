using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
        textComponent.text = "Score: " + PlayerStats.Points;

        // Press g to unlock the cursor
        if (Input.GetKey(KeyCode.G))
        {
            if(isLocked)
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

        if(Input.GetKey(KeyCode.G) && isLocked)
        {
            
        }
    }
}
