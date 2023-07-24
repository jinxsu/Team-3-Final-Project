using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapWall : MonoBehaviour
{
    public TrapManager trapManager;

    public void HitByBoss()
    {
        trapManager.CheckSpawns();
    }

}
