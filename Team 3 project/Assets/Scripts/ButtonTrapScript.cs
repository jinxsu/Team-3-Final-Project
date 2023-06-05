using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTrapScript : MonoBehaviour
{
    [SerializeField] private GameObject trapToSpawn;
    [SerializeField] private GameObject spawnLocation;
    [SerializeField] private float trapTimer;
    [SerializeField] private float trapTime;
    
    private PlayerControls player;
    bool playerDetected = false;


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        { 
            player = other.gameObject.GetComponent<PlayerControllerScript>().controls;
            playerDetected = true;
            Debug.Log("player detected");
        }
    }

    private void Update()
    {
        if (playerDetected)
        {
            if (player.Player.Interact.triggered)
            {
                Instantiate(trapToSpawn, spawnLocation.transform.position, spawnLocation.transform.rotation);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            player = null;
            playerDetected = false;
        }
    }



}
