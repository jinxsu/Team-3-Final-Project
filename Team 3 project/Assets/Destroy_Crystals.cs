using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy_Crystals : MonoBehaviour
{



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject, 2f);
        }
    }
    

    
}
