using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.Android;

public class EmergencyDoorScript : MonoBehaviour
{
    [SerializeField] private GameObject lightbulb;
    [SerializeField] private GameObject areaLight;
    [SerializeField] private Animator doorAnim;
    private AudioSource doorOpenSound;
    private Material lightbulbMat;
    private bool canDoorOpen;

    private bool playerDetected;
    private PlayerControls player;

    private void Start()
    {
        areaLight.GetComponent<Light>().enabled = false;
        lightbulbMat = lightbulb.GetComponent<Renderer>().material;
    }

    private void Update()
    {
        if (canDoorOpen && playerDetected && player.Player.Interact.triggered)
        {
            doorAnim.SetTrigger("open");
        }
    }

    public void BossDefeated()
    {
        canDoorOpen = true;
        areaLight.GetComponent<Light>().enabled = true;
        lightbulbMat.EnableKeyword("_EMISSION");
        //play the buzzer sound
        doorOpenSound.Play();
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
