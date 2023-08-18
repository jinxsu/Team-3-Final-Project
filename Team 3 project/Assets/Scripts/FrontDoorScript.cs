using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontDoorScript : MonoBehaviour
{
    private Animator doorAnim;

    private bool playerDetected;
    private PlayerControllerScript player;
    [SerializeField] private AudioSource doorOpen;
    [SerializeField] private float openDelay = 0;


    private void Start()
    {
        doorAnim = GetComponent<Animator>();
    }

    private void Update()
    {
        if(playerDetected)
        {
            if (player.controls.Player.Interact.triggered)
            {
                doorAnim.SetTrigger("open");
                player.intString = "";
                player.canInteract = false;
                GetComponent<BoxCollider>().enabled = false;
                doorOpen.PlayDelayed(openDelay);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerDetected = true;
            player = other.gameObject.GetComponent<PlayerControllerScript>();
            player.intString = "Press E to open";
            player.canInteract = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player.intString = "";
            player.canInteract = false;
            player = null;
            playerDetected = false;
        }
    }
}
