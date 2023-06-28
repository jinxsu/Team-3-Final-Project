using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBossTwoChargeState : BigBossTwoVulnerable
{
    protected GameObject target;
    private float chargePause = 3f;
    private float chargePauseTimer;
    private int moveSpd = 20;
    private float chargeMax = 10f;
    private float chargeMaxTimer;

    protected override void OnUpdate()
    {
        if (chargePauseTimer > 0)
        {
            chargePauseTimer -= Time.deltaTime;
        }
        else
        {
            sc.transform.position += moveSpd * Time.deltaTime * sc.transform.forward;
            chargeMax -= Time.deltaTime;
            if(chargeMaxTimer < 0 )
            {
                bsc.ChangeState(bsc.ChaseState);
            }
        }
    }


    protected override void OnEnter()
    {
        base.OnEnter();

        target = GameObject.FindWithTag("Player");
        Vector3 chargeTarget = new Vector3(target.transform.position.x, bsc.transform.position.y, target.transform.position.z);
        sc.transform.LookAt(chargeTarget);
        chargePauseTimer = chargePause;
        chargeMaxTimer = chargeMax;

        bsc.navMeshAgent.enabled = false;
    }



}
