using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private string colour;
    [SerializeField] private int level;
    [SerializeField] private GameObject keyColourText;
    [SerializeField] private Collider doorCollider;

    TextMesh requiredText;
    bool doorShouldMove = false;
    bool playerHasKey = false;
    Renderer doorColourRenderer;
    PlayerPresentColliderScript playerPresentScript;

    private void Start()
    {
        playerPresentScript = doorCollider.GetComponent<PlayerPresentColliderScript>();
        doorColourRenderer = gameObject.GetComponent<Renderer>();

        if (colour.Equals("red"))
        {
            doorColourRenderer.material.SetColor("_Color", Color.red);
        }
        else if (colour.Equals("blue"))
        {
            doorColourRenderer.material.SetColor("_Color", Color.blue);
        }
        else if (colour.Equals("green"))
        {
            doorColourRenderer.material.SetColor("_Color", Color.green);
        }
        else
        {
            doorColourRenderer.material.SetColor("_Color", Color.black);
        }

        requiredText = keyColourText.GetComponent<TextMesh>();
        requiredText.text = colour + " key required";

        keyColourText.SetActive(false);
    }

    private void Update()
    {
        // Manage the showing of the various stuff here in update as its called more reliably.
        if (playerPresentScript.playerPresent)
        {
            if (PlayerStats.hasKey(colour, level))
            {
                requiredText.text = "Use key? E to confirm";
                playerHasKey = true;
            }
            else
            {
                requiredText.text = colour + " key required";
                playerHasKey = false;
            }

            if (!doorShouldMove)
            {
                keyColourText.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E) && playerHasKey)
                {
                    if (PlayerStats.RemoveKeyOfColourForLevel(colour, level))
                    {
                        doorShouldMove = true;
                        keyColourText.SetActive(false);
                    }
                }
            }
        }
        else
        {
            playerHasKey = false;
            keyColourText.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        // move the door upwards a bit cause it looks nice
        if (doorShouldMove)
        {
            if (gameObject.transform.position.y < 5)
            {
                gameObject.transform.position += Vector3.up * 1f * Time.deltaTime;

                // slowly fade the door out so it looks like its vanishing from the world
                Color doorColour = doorColourRenderer.material.color;
                float fadeAmount = doorColour.a - (5f * Time.deltaTime);
                doorColour = new Color(doorColour.r, doorColour.g, doorColour.b, fadeAmount);
                doorColourRenderer.material.color = doorColour;

                // then once fully faded out destroy the object (doesn't NEED to happen)
                if (doorColour.a <= 0)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
