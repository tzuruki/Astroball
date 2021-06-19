using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour
{
    [SerializeField] private string colour;
    [SerializeField] private int level;

    private void Start()
    {
        Renderer keyColourRenderer = gameObject.GetComponent<Renderer>();

        if (colour.Equals("red"))
        {
            keyColourRenderer.material.SetColor("_Color", Color.red);
        }
        else if (colour.Equals("blue"))
        {
            keyColourRenderer.material.SetColor("_Color", Color.blue);
        }
        else if (colour.Equals("green"))
        {
            keyColourRenderer.material.SetColor("_Color", Color.green);
        }
        else
        {
            keyColourRenderer.material.SetColor("_Color", Color.black);
        }
    }

    public string GetColour()
    {
        return colour;
    }

    public int GetLevel()
    {
        return level;
    }

}
