using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngelBossRespawnState : State
{
    AngelBossStateController bsc;
    float waitTimer;
    protected override void OnEnter()
    {
        bsc = (AngelBossStateController)sc;
        Debug.Log("Respawn State");
        bsc.navMeshAgent.enabled = false;
        bsc.transform.position = bsc.respawnPoint.position;

        //In testing, damage was inflicted by the traps used, uncomment next line if that changes in implementation
        //bsc.hp--;


        if (bsc.hp <= 0)
        {
            bsc.ChangeState(bsc.DeathState);
            Debug.Log("Boss dead!");
        }
        waitTimer = 2f;
        bsc.fastSpeed += 2f;
        bsc.slowSpeed += 2f;
    }

    protected override void OnUpdate()
    {
        waitTimer -= Time.deltaTime;
        Debug.Log(waitTimer);
        if (waitTimer < 0) bsc.ChangeState(bsc.ChaseState);
    }
}
