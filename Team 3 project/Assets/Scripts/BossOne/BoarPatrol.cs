using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoarPatrol : BoarVulnerable
{
    protected GameObject target;

    
    private int moveSpd = 7;
    
    private int minDist = 5;
    
    
    // Start is called before the first frame update
    protected override void OnUpdate()
    {
        // Code placed here can be overridden
        //Debug.Log("Chase coords: " + target.transform.position);
        sc.transform.LookAt(target.transform);

        if (Vector3.Distance(sc.transform.position, target.transform.position) >= minDist)
        {
            sc.transform.position += sc.transform.forward * moveSpd * Time.deltaTime;
        }
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
