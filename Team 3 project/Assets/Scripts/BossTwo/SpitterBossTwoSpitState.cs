using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpitterBossTwoSpitState : State
{
    SpitterBossTwoStateController bsc;

    float spitTimer = 2f;
    float spitTime;
    int forwardMomentum = 500;
    int upwardMomentum = 100;
    
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
            var spitProjectile = Object.Instantiate(bsc.bossSpitProjectile, bsc.spitPoint.transform.position, bsc.spitPoint.transform.rotation);
            spitProjectile.GetComponent<Rigidbody>().AddForce(spitProjectile.transform.forward * forwardMomentum + new Vector3(0, upwardMomentum, 0));
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
