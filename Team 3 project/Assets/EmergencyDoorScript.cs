using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class EmergencyDoorScript : MonoBehaviour
{
    [SerializeField] private GameObject emLightbulb;
    [SerializeField] private GameObject emLight;
    [SerializeField] private Animator doorAnim;
    private bool canDoorOpen = true;
    private bool isDoorOpen = false;

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
