using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpitterBossTwoDeathState : State
{
    SpitterBossTwoStateController bsc;

    float deathTimer = 3f;
    float deathTime;

    protected override void OnEnter()
    {
        base.OnEnter();
        deathTime = deathTimer;
        bsc = (SpitterBossTwoStateController)sc;
        bsc.navMeshAgent.enabled = false;
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();

        deathTime -= Time.deltaTime;

        Debug.Log("Death Timer :" + deathTime);

        if (deathTime < 0)
        {
            bsc.destroyMe = true;
        }
    }
}
