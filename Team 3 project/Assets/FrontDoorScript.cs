using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontDoorScript : MonoBehaviour
{
    private Animator doorAnim;

    private bool playerDetected;
    private PlayerControls player;

    private void Start()
    {
        doorAnim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (playerDetected && player.Player.Interact.triggered)
        {
            doorAnim.SetTrigger("open");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerDetected = true;
            player = other.gameObject.GetComponent<PlayerControllerScript>().controls;
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
}
