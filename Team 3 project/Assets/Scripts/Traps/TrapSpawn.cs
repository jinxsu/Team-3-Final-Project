using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSpawn : MonoBehaviour
{

    [SerializeField]
    GameObject trap;

    public bool isOccupied;
    
    public void SpawnTrap()
    {
        if (!isOccupied)
        {
            var newTrap = Instantiate(trap,transform);

            newTrap.GetComponent<CrystalTrap>().spawn = this;

        }
    }
}

