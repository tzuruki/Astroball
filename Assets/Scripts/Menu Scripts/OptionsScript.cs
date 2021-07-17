using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Slider slider;
    [SerializeField] Text text;

    void Start()
    {
        slider.value = PlayerStats.MouseSensitivity;
    }

    public void OnXSliderChanged(float value)
    {
        PlayerStats.MouseSensitivity = value;
    }

}
