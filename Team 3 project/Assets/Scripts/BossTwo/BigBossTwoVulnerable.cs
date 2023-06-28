using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBossTwoVulnerable : State
{

    protected BigBossTwoStateController bsc;

    protected override void OnEnter()
    {
        bsc = (BigBossTwoStateController)sc;
    }
}
