using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseDoor : MonoBehaviour
{
    private EmergencyDoorScript m_DoorScript;
    private Animator m_Animator;
    private bool playerDetected;
    private PlayerControls player;
    [SerializeField] private AudioSource doorOpen;
    [SerializeField] private float openDelay = 0;
   
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
            doorOpen.PlayDelayed(openDelay);
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
