using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPresentColliderScript : MonoBehaviour
{
    public bool playerPresent = false;
    
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
