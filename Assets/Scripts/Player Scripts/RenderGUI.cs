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

    private void OnGUI()
    {
        if (GUI.Button(new Rect(0, 120, 100, 50), "Lock Cursor"))
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        //Press this button to confine the Cursor within the screen
        if (GUI.Button(new Rect(0, 240, 100, 50), "Confine Cursor"))
        {
            Cursor.lockState = CursorLockMode.Confined;
        }
    }
}
