using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkerNpcScript : MonoBehaviour
{
    // each of these objects is something to display depending on when we're close to the player,
    // talking to the player, and something to add or remove on the NPC object when talking
    [SerializeField] private GameObject UiObjectToShow;
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject objToAttachTo;

    bool playerPresent;
    RectTransform canvasRect, UiObjRt;

    void Start()
    {
        // Initially don't show anything as we won't know if the player is close or not
        UiObjectToShow.SetActive(false);

        canvasRect = canvas.GetComponent<RectTransform>();
        UiObjRt = UiObjectToShow.GetComponent<RectTransform>();

    }

    private void Update()
    {
        // Manage the showing of the various stuff here in update as its called more reliably.
        if (playerPresent)
        {
            if (!UiObjectToShow.activeSelf)
            {
                UiObjectToShow.SetActive(true);
            }
            ChatboxScriptHelper.UpdateUiTextboxPos(objToAttachTo, 3f, canvasRect, UiObjRt);
        }
        else
        {
            UiObjectToShow.SetActive(false);
        }
    }

    // called once per frame for each collider that collides with this object
    private void OnTriggerStay(Collider collider)
    {
        // the collider has a tag, which for the player we have assigned to "Player"
        if (collider.tag == "Player")
        {
            // we're colliding with the player, set this to true
            playerPresent = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            // the player has left the bounding box of the object we're near,
            // clean up everything
            playerPresent = false;
        }
    }
}
