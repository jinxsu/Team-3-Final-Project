using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBossTwoRecoilState : State
{
    BigBossTwoStateController bsc;

    private float recoilStunTimer;
    private int moveSpd = 4;

    protected override void OnEnter()
    {
        bsc = (BigBossTwoStateController)sc;
        recoilStunTimer = 4f;
        bsc.navMeshAgent.enabled = false;

    }

    protected override void OnUpdate()
    {
        bsc.transform.position -= moveSpd * Time.deltaTime * bsc.transform.forward;
        recoilStunTimer -= Time.deltaTime;
        if (recoilStunTimer < 0)
        {
            bsc.ChangeState(bsc.ChaseState);
        }
    }
}
