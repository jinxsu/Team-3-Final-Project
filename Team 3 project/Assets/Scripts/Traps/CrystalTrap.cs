using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalTrap : MonoBehaviour
{
    public TrapSpawn spawn;
    public AudioSource audio;
    public AudioClip crystalBreakSound;
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Boss"))
        {
            audio.PlayOneShot(crystalBreakSound);
            other.GetComponent<StateController>().hp -= 1;
            spawn.isOccupied = false;
            Destroy(gameObject);
        }
    }
}
