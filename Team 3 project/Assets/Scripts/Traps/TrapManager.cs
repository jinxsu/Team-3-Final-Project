using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapManager : MonoBehaviour
{

    public TrapSpawn[] TrapSpawns;

    public void CheckSpawns()
    {
        bool canSpawn = true;
        foreach (TrapSpawn spawn in TrapSpawns)
        {
            if (spawn.isOccupied) canSpawn = false;
            Debug.Log("Found Trap");
        }

        if (canSpawn) FillSpawns();
    }


    private void FillSpawns()
    {
        foreach (TrapSpawn spawn in TrapSpawns)
        {
            spawn.SpawnTrap();
            Debug.Log("Populated Trap");
        }
    }
}
