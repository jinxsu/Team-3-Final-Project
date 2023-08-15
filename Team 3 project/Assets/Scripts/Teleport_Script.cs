using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport_Script : MonoBehaviour
{
    private PlayerControls player;
    bool playerDetected = false;

    private void OnTriggerEnter(Collider other)
    {

        //Checks if the player is inside the trigger zone that allows them to press the button. 
        if (other.gameObject.CompareTag("Player"))
        {
            //This fetches the player's inputcontrols componenet so that the script can check if the interact button is pressed.
            //Placing this in the Update function caused consistency issues
            player = other.gameObject.GetComponent<PlayerControllerScript>().controls;
            playerDetected = true;
            Debug.Log("player detected");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = null;
            playerDetected = false;
        }
    }

    void Update()
    {
        if (playerDetected)
        {
            if (player.Player.Interact.triggered)
            {
                
                Debug.Log("Loading New Scene");
                SceneManager.LoadScene("Starting Area");
            }
        }
    }
}
