using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RenderGUI : MonoBehaviour
{
    public string scoreText;
    private Text textComponent;

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
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
