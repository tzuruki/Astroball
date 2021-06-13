using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FranksScript : MonoBehaviour
{
    [SerializeField] private GameObject talkToMeText;
    [SerializeField] private GameObject UiObjectToShow;
    [SerializeField] private GameObject gameObjectToShow;
    bool playerPresent, menuOpen;

    // Needs commenting sorry

    // Start is called before the first frame update
    void Start()
    {
        talkToMeText.SetActive(false);
        UiObjectToShow.SetActive(false);
        if(gameObjectToShow != null)
        {
            gameObjectToShow.SetActive(false);
        }

    }

    private void Update()
    {
        if (playerPresent)
        {
            talkToMeText.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E) && !menuOpen)
            {
                UiObjectToShow.SetActive(true);
                if (gameObjectToShow != null)
                {
                    gameObjectToShow.SetActive(true);
                }
                menuOpen = true;
            }
            else if(Input.GetKeyDown(KeyCode.E) && menuOpen)
            {
                UiObjectToShow.SetActive(false);
                if (gameObjectToShow != null)
                {
                    gameObjectToShow.SetActive(false);
                }
                menuOpen = false;
            }
        }
    }

    private void OnTriggerStay(Collider collider)
    {
        if(collider.tag == "Player")
        {
            playerPresent = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            playerPresent = false;
            talkToMeText.SetActive(false);
            if (gameObjectToShow != null)
            {
                gameObjectToShow.SetActive(false);
            }
            UiObjectToShow.SetActive(false);
        }
    }
}
