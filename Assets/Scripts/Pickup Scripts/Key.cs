using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{

    [SerializeField] private GameObject door;

    bool moveDoor;
    float verticalSpeed = 1f;
    Renderer doorRenderer;
    float fadeOutSpeed = 5f;
    bool fadeOut = true;
    bool collected = false;

     void Start()
    {
        doorRenderer = door.GetComponent<Renderer>();
    }


    private void FixedUpdate()
    {
        if (moveDoor)
        {
            // move the door upwards a bit cause it looks nice
            if (door.transform.position.y < 5)
            {
                door.transform.position += Vector3.up * verticalSpeed * Time.deltaTime;
            }
            else if (fadeOut)
            {
                // slowly fade the door out so it looks like its vanishing from the world
                Color doorColour = doorRenderer.material.color;
                float fadeAmount = doorColour.a - (fadeOutSpeed * Time.deltaTime);
                doorColour = new Color(doorColour.r, doorColour.g, doorColour.b, fadeAmount);
                doorRenderer.material.color = doorColour;

                // then once fully faded out destroy the object (doesn't NEED to happen)
                if (doorColour.a <= 0)
                {
                    fadeOut = false;
                    Destroy(door);
                    Destroy(gameObject);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!collected)
            {
                moveDoor = true;
                collected = true;
                Color keyColour = GetComponent<Renderer>().material.color;
                keyColour = new Color(keyColour.r, keyColour.g, keyColour.b, 0f);
                GetComponent<Renderer>().material.color = keyColour;
            }
        }
    }

}
