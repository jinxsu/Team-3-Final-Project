using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//Unoptimized because I'm not done working on this one

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
            bsc.SpawnBosses();

            bsc.destroyMe = true;
        }
    }


}
