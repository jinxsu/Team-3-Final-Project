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
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();

        deathTime -= Time.deltaTime;

        if (deathTime < 0)
        {
            bsc.destroyMe = true;
        }
    }
}
