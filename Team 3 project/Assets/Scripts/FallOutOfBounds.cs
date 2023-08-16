using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallOutOfBounds : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<PlayerControllerScript>().KillPlayer();
        }
    }

}
