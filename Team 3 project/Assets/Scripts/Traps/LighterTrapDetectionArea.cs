using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LighterTrapDetectionArea : MonoBehaviour
{
    [SerializeField]
    LighterTrap ligherTrap;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boss")) ligherTrap.angelIsIn = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Boss")) ligherTrap.angelIsIn = false;

        
    }
}
