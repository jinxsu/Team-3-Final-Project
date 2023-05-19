using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoarPatrol : BoarVulnerable
{
    protected GameObject target;
    // Start is called before the first frame update
    protected override void OnUpdate()
    {
        // Code placed here can be overridden
        //Debug.Log("Chase coords: " + target.transform.position);
        
    }

    protected override void OnEnter()
    {
        base.OnEnter();
        target = GameObject.FindWithTag("Player");
        Debug.Log("Chase State");


    }

    protected override void OnExit()
    {
        // Code placed here can be overridden
    }

    



}
