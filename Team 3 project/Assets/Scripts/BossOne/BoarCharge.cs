using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoarCharge : BoarVulnerable
{
    protected Transform target;
    // Start is called before the first frame update
    protected override void OnUpdate()
    {
        // Code placed here can be overridden

        Debug.Log("Charge coords: " + target.position);
    }

    protected override void OnEnter()
    {
        base.OnEnter();
        target = GameObject.FindWithTag("Player").transform;
        Debug.Log("Charge State");

    }

    protected override void OnExit()
    {
        // Code placed here can be overridden
    }


    
}
