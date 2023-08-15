using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngelBossDropState : State
{
    AngelBossStateController bsc;

    bool falling;

    float fallTimer;
    protected override void OnEnter()
    {
        bsc = (AngelBossStateController)sc;
        falling = false;
        fallTimer = 1f;
        bsc.navMeshAgent.enabled = false;
    }

    protected override void OnUpdate()
    {
        if (falling)
        {
            bsc.transform.position = new Vector3(bsc.transform.position.x, bsc.transform.position.y - 0.01f, bsc.transform.position.z);
        }
        else
        {
            fallTimer -= Time.deltaTime;
            if (fallTimer < 0f) falling = true;
        }
        
    }
}
