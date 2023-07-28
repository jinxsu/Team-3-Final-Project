using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpitterBossTwoChaseState : State
{
    SpitterBossTwoStateController bsc;

    protected override void OnUpdate()
    {
        bsc.navMeshAgent.destination = bsc.player.transform.position;
        //check if playing has entered the spitting range, entering needs the player to be closer than they need to be to leave the spitting range
        if ((bsc.player.transform.position - bsc.transform.position).magnitude < 25)
        {
            bsc.ChangeState(bsc.SpitState);
        }
    }

    protected override void OnEnter()
    {
        base.OnEnter();
        bsc = (SpitterBossTwoStateController)sc;

        bsc.navMeshAgent.enabled = true;
        bsc.navMeshAgent.ResetPath();
    }
}
