using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class BoarDeath : State
{
    float deathCountdown = 3f;
    BoarBossStateController bsc;

    protected override void OnEnter()
    {
        bsc = (BoarBossStateController)sc;
    }

    protected override void OnUpdate()
    {
        deathCountdown -= Time.deltaTime;
        if (deathCountdown < 0 )
        {
            bsc.destroyMe = true;
        }
    }

}

