using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBossTwoIdle : State
{
    BigBossTwoStateController bsc;
    protected override void OnEnter()
    {
        bsc = (BigBossTwoStateController)sc;
        bsc.navMeshAgent.enabled = false;
    }
}
