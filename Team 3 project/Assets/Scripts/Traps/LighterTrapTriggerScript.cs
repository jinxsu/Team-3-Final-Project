using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LighterTrapTriggerScript : MonoBehaviour
{
    LighterTrap lighterTrap;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Boss"))
        {
            Destroy(gameObject);
        }
    }
}
