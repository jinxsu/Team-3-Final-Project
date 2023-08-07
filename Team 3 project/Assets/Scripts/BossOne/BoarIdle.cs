using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarIdle : State
{
    BoarBossStateController bsc;

    protected override void OnEnter()
    {
        base.OnEnter();

        bsc = (BoarBossStateController)sc;
        bsc.navMeshAgent.enabled = false;
    }
}
