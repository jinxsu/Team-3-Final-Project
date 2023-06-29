using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BigBossTwoDeathState : State
{
    float deathCountdown = 3f;
    BigBossTwoStateController bsc;

    protected override void OnEnter()
    {
        bsc = (BigBossTwoStateController)sc;
    }

    protected override void OnUpdate()
    {
        deathCountdown -= Time.deltaTime;
        if (deathCountdown < 0 )
        {
            //need to add spawning the second phase bosses
            bsc.destroyMe = true;
        }
    }
}
