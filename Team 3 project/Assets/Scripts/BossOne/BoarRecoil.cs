using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarRecoil : State
{
    BoarBossStateController bsc;

    private float recoilStun;
    private float recoilStunTimer;
    private int moveSpd = 4;
    protected override void OnEnter()
    {
        // Code placed here can be overridden
        bsc = (BoarBossStateController)sc;
        recoilStunTimer = 4f;
        Debug.Log("Recoil State");
    }

    protected override void OnUpdate()
    {
        sc.transform.position -= sc.transform.forward * moveSpd * Time.deltaTime;
        recoilStunTimer -= Time.deltaTime;
        if (recoilStunTimer < 0)
        {
            bsc.ChangeState(bsc.PatrolState);
        }
    }
    // Start is called before the first frame update

}
