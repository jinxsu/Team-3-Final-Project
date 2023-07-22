using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalTrap : MonoBehaviour
{
    public TrapSpawn spawn;
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Boss"))
        {
            other.GetComponent<StateController>().hp -= 1;
            spawn.isOccupied = false;
            Destroy(gameObject);
        }
    }
}
