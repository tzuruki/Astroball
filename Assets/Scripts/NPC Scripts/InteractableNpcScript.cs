using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableNpcScript : MonoBehaviour
{
    // each of these objects is something to display depending on when we're close to the player,
    // talking to the player, and something to add or remove on the NPC object when talking
    [SerializeField] private GameObject talkToMeText;
    [SerializeField] private GameObject UiObjectToShow;
    [SerializeField] private GameObject gameObjectToShow;
    [SerializeField] private GameObject canvas;
    [SerializeField] private float yPosCanvas = 3f;
    [SerializeField] private GameObject objToAttachTo;

    bool playerPresent, menuOpen;
    RectTransform canvasRect, UiObjRt;

    void Start()
    {
        // Initially don't show anything as we won't know if the player is close or not
        talkToMeText.SetActive(false);
        UiObjectToShow.SetActive(false);

        canvasRect = canvas.GetComponent<RectTransform>();
        UiObjRt = UiObjectToShow.GetComponent<RectTransform>();

        // ObjectToShow is optional, doesn't have to be populated.
        if (gameObjectToShow != null)
        {
            gameObjectToShow.SetActive(false);
        }

    }

    private void Update()
    {
        // Manage the showing of the various stuff here in update as its called more reliably.
        if (playerPresent)
        {
            if (!menuOpen)
            {
                talkToMeText.SetActive(true);
            }
            else
            {
                ChatboxScriptHelper.UpdateUiTextboxPos(objToAttachTo, yPosCanvas, canvasRect, UiObjRt);
            }
            
            if (Input.GetKeyDown(KeyCode.E) && !menuOpen)
            {
                // If the "npc menu" isn't open, open it, and show
                // any other objects/UiObjects
                OpenMenu();
            }
            else if(Input.GetKeyDown(KeyCode.E) && menuOpen)
            {
                // if the menu is already open, try to close it
                CloseMenu();
            }
        }
    }

    // called once per frame for each collider that collides with this object
    private void OnTriggerStay(Collider collider)
    {
        // the collider has a tag, which for the player we have assigned to "Player"
        if(collider.tag == "Player")
        {
            // we're colliding with the player, set this to true
            playerPresent = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            // the player has left the bounding box of the object we're near,
            // clean up everything
            playerPresent = false;
            talkToMeText.SetActive(false);
            if (gameObjectToShow != null)
            {
                gameObjectToShow.SetActive(false);
            }
            UiObjectToShow.SetActive(false);
            menuOpen = false;
        }
    }

    private void OpenMenu()
    {
        UiObjectToShow.SetActive(true);
        talkToMeText.SetActive(false);
        if (gameObjectToShow != null)
        {
            gameObjectToShow.SetActive(true);
        }
        menuOpen = true;
    }

    private void CloseMenu()
    {
        UiObjectToShow.SetActive(false);
        talkToMeText.SetActive(true);
        if (gameObjectToShow != null)
        {
            gameObjectToShow.SetActive(false);
        }
        menuOpen = false;
    }
}
