using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoarCharge : BoarVulnerable
{
    //This is the boss' charge state. It can be reset by being shot again and doesn't account for gravity

    protected GameObject target;
    private float chargePause = 3f;
    private float chargePauseTimer;
    private int moveSpd = 20;
    private float chargeMax = 10f;
    private float chargeMaxTimer;
    // Start is called before the first frame update
    protected override void OnUpdate()
    {
        // Code placed here can be overridden
        if(chargePauseTimer > 0)
        {
            chargePauseTimer -= Time.deltaTime;
        }
        else
        {
            sc.transform.position += moveSpd * Time.deltaTime * sc.transform.forward;
            chargeMaxTimer -= Time.deltaTime;
            if (chargeMaxTimer < 0)
            {
                bsc.ChangeState(bsc.PatrolState);
            }
        }
        
    }

    protected override void OnEnter()
    {
        //BoarVulnerable OnEnter
        base.OnEnter();

        // Initiation of variables for this state
        target = GameObject.FindWithTag("Player");
        Debug.Log("Charge State");
        Vector3 chargeTarget = new Vector3(target.transform.position.x, bsc.transform.position.y, target.transform.position.z);
        sc.transform.LookAt(chargeTarget);
        chargePauseTimer = chargePause;
        chargeMaxTimer = chargeMax;

        //disabling the navmesh agent so that the boar can charge off of cliffs
        bsc.navMeshAgent.enabled = false;
    }

    
}
