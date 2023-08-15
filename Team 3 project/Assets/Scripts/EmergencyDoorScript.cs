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
    private Material lightbulbMat;
    public bool canDoorOpen;
    //when we defeat the boss the sound is to tell us that the emergency door is ready to be used
    private AudioSource doorReadySound;

    //sound for opening and closing the door
    [SerializeField] private AudioSource doorOpen;
    [SerializeField] private float openDelay = 0;
    [SerializeField] private AudioSource doorClose;
    [SerializeField] private float closeDelay = 0;

   

    private void Start()
    {
        areaLight.GetComponent<Light>().enabled = false;
        lightbulbMat = lightbulb.GetComponent<Renderer>().material;
        doorReadySound= GetComponent<AudioSource>();
    }

    public void BossDefeated()
    {
        canDoorOpen = true;
        areaLight.GetComponent<Light>().enabled = true;
        lightbulbMat.EnableKeyword("_EMISSION");
        //play the buzzer sound
        doorReadySound.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            doorAnim.SetTrigger("close");
            Debug.Log("Closing door");
            doorClose.PlayDelayed(closeDelay);
        }
    }
}
