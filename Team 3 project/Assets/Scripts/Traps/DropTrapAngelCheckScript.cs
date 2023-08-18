using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropTrapAngelCheckScript : MonoBehaviour
{
    [SerializeField]
    DropTrapScript trap;

    public void AngelGot()
    {
        Debug.Log("Angel Dropped");
        trap.droppedAngel = true;
    }
}
