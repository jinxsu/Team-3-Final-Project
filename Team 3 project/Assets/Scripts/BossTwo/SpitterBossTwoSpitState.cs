using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpitterBossTwoSpitState : State
{
    SpitterBossTwoStateController bsc;

    float spitTimer = 2f;
    float spitTime;
    
    protected override void OnEnter()
    {
        bsc = (SpitterBossTwoStateController)sc;
        bsc.navMeshAgent.enabled = false;

        spitTime = spitTimer;
    }

    protected override void OnUpdate()
    {

        bsc.transform.LookAt(bsc.player.transform);

        if (spitTime > 0)
        {
            spitTime -= Time.deltaTime;
        }
        else
        {
            Debug.Log("I SPIT AT YOU");
            Object.Instantiate(bsc.bossSpitProjectile);
            //check if playing has left the spitting range, leaving needs the player to be further away than they need to be to enter the spitting range
            if ((bsc.player.transform.position - bsc.transform.position).magnitude > 25)
            {
                bsc.ChangeState(bsc.ChaseState);
            }
            else
            {
                spitTime = spitTimer;
            }
        }


        
    }

}
