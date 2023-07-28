using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class AngelBossSlowState : State
{
    AngelBossStateController bsc;
    float slowTimer;
    GameObject target;

    protected override void OnEnter()
    {
        slowTimer = 6f;
        bsc = (AngelBossStateController)sc;
        target = bsc.player;
        bsc.navMeshAgent.speed = 1.5f;
        bsc.navMeshAgent.enabled = true;
        bsc.navMeshAgent.ResetPath();
    }

    protected override void OnUpdate()
    {
        Debug.Log("Angel slow chasing");
        bsc.navMeshAgent.destination = target.transform.position;
        slowTimer -= Time.deltaTime;
        if (slowTimer < 0 )
        {
            bsc.ChangeState(bsc.ChaseState);
        }
    }
}
