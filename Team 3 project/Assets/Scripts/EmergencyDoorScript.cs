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
    public bool canDoorOpen;

    private void Start()
    {
        areaLight.GetComponent<Light>().enabled = false;
        lightbulbMat = lightbulb.GetComponent<Renderer>().material;
        doorOpenSound= GetComponent<AudioSource>();
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
        if (other.CompareTag("Player"))
        {
            doorAnim.SetTrigger("close");
            Debug.Log("Closing door");
        }
    }
}
