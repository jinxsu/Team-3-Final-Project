using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseDoor : MonoBehaviour
{
    private EmergencyDoorScript m_DoorScript;
    private Animator m_Animator;
    private bool playerDetected;
    private PlayerControls player;
    public AudioClip doorOpenSound;
    public AudioSource audio;
    
   
    private void Start()
    {
        m_DoorScript = GetComponentInParent<EmergencyDoorScript>();
        m_Animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (m_DoorScript.canDoorOpen && playerDetected && player.Player.Interact.triggered)
        {
            m_Animator.SetTrigger("open");
            audio.PlayOneShot(doorOpenSound);
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
