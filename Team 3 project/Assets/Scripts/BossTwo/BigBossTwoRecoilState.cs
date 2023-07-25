using UnityEngine;

public class BigBossTwoRecoilState : State
{
    BigBossTwoStateController bsc;

    private float recoilStunTimer;
    private int moveSpd = 5;

    protected override void OnEnter()
    {
        bsc = (BigBossTwoStateController)sc;
        recoilStunTimer = 1f;
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
