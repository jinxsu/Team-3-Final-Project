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
    private bool canDoorOpen;
    private bool isDoorOpen;

    private void Start()
    {
        areaLight.GetComponent<Light>().enabled = false;
        lightbulbMat = lightbulb.GetComponent<Renderer>().material;
    }

    public void BossDefeated()
    {
        canDoorOpen = true;
        areaLight.GetComponent<Light>().enabled = true;
        lightbulbMat.EnableKeyword("_EMISSION");
    }

    public void Interact()
    {
        if (canDoorOpen)
        {
            doorAnim.SetTrigger("open");
            isDoorOpen = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && isDoorOpen)
        {
            doorAnim.SetTrigger("close");
            canDoorOpen = false;
        }
    }
}
