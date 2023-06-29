using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBossTwoStompState : State
{
    float stompCountdown;
    bool hasStomped;
    BigBossTwoStateController bsc;

    protected override void OnEnter()
    {
        stompCountdown = 3;
        bsc = (BigBossTwoStateController)sc;
        bsc.navMeshAgent.enabled = false;
        hasStomped = false;
    }

    protected override void OnUpdate()
    {
        stompCountdown -= Time.deltaTime;
        //wind up timer (gives time for the stomp animation to play so that the player can anticipate the shockwave and time their jump)
        if ( stompCountdown < 0 && !hasStomped)
        {
            Object.Instantiate(bsc.stompObject, new Vector3(bsc.transform.position.x, bsc.transform.position.y - 2.5f, bsc.transform.position.z), Quaternion.Euler(0,0,0));
            hasStomped = true;
            stompCountdown = 2;
        }
        //cooldown timer, so that the boss isn't instantly chasing the player after the stomp happens
        if (stompCountdown < 0 && hasStomped)
        {
            bsc.ChangeState(bsc.ChaseState);
        }
    }
}
