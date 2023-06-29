using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBossTwoChaseState : BigBossTwoVulnerable
{
    protected GameObject target;

    protected override void OnUpdate()
    {
        bsc.navMeshAgent.destination = target.transform.position;
    }

    protected override void OnEnter()
    {
        base.OnEnter();

        target = bsc.player;

        bsc.navMeshAgent.enabled = true;
        bsc.navMeshAgent.ResetPath();
    }
}
