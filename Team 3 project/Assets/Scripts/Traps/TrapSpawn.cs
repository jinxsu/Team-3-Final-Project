using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSpawn : MonoBehaviour
{

    [SerializeField]
    GameObject[] trap;

    public bool isOccupied;
    
    public void SpawnTrap()
    {
        if (!isOccupied)
        {
            var trapToSpawn = Random.Range(0, trap.Length);
            var newTrap = Instantiate(trap[trapToSpawn],transform);

            newTrap.GetComponent<CrystalTrap>().spawn = this;

        }
    }
}

