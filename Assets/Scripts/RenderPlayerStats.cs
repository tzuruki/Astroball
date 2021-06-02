using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RenderPlayerStats : MonoBehaviour
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
    }
}
