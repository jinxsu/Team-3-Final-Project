using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmushTrapAngelCheckScript : MonoBehaviour
{
    [SerializeField]
    SmushTrapScript trap;

    public void AngelGot()
    {
        Debug.Log("Angel Smushed");
        trap.squishedAngel = true;
    }
}
