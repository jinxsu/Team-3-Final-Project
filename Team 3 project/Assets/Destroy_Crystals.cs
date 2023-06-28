using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy_Crystals : MonoBehaviour
{
    public bool TimeOver = false;

    

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            Destroy(this.gameObject, 1f);
            
        }
    }
    

    
}
