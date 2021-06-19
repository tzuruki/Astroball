using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private string colour;
    [SerializeField] private int level;
    [SerializeField] private GameObject keyColourText;

    bool playerPresent = false;
    TextMesh requiredText;
    bool doorShouldMove = false;
    bool playerHasKey = false;
    Renderer doorColourRenderer;

    private void Start()
    {
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
        if (playerPresent)
        {
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

    private void OnTriggerStay(Collider collider)
    {
        // the collider has a tag, which for the player we have assigned to "Player"
        if (collider.tag == "Player")
        {
            // we're colliding with the player, set this to true
            playerPresent = true;

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


        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            // the player has left the bounding box of the object we're near,
            // clean up everything
            playerPresent = false;
            playerHasKey = false;
            keyColourText.SetActive(false);
        }
    }
}
