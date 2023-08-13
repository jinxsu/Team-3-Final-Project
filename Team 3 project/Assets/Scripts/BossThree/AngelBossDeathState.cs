using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngelBossDeathState : State
{
    AngelBossStateController bsc;
    protected override void OnEnter()
    {
        bsc = (AngelBossStateController)sc;
        bsc.destroyMe = true;
    }
}
