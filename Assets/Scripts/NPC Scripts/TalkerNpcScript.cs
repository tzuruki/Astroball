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
            UpdateUiTextboxPos(objToAttachTo,canvasRect, UiObjRt, 3f);
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

    // wild bit of code that figures out where on the UI to display a textbox from an npc location in world
    // shamelessly stolen from:
    // https://forum.unity.com/threads/create-ui-health-markers-like-in-world-of-tanks.432935/
    private void UpdateUiTextboxPos(GameObject gameObject, RectTransform canvasRect, RectTransform uiObjRt, float yOffset)
    {
        // Offset position above object box (in world space)
        float offsetPosY = gameObject.transform.position.y + yOffset;

        // Final position of marker in world space
        Vector3 offsetPos = new Vector3(gameObject.transform.position.x, offsetPosY, gameObject.transform.position.z);

        // Calculate *screen* position (note, not a canvas/recttransform position)
        Vector2 canvasPos;
        Vector2 screenPoint = Camera.main.WorldToScreenPoint(offsetPos);

        // Convert screen position to Canvas / RectTransform space <- leave camera null if Screen Space Overlay
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, screenPoint, null, out canvasPos);

        // Set position of ui element we want to show
        uiObjRt.localPosition = canvasPos;
    }
}
