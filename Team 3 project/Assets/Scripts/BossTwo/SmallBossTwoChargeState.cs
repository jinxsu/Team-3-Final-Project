using UnityEngine;

public class SmallBossTwoChargeState : SmallBossTwoVulnerable
{
    private float chargePause = 3f;
    private float chargePauseTimer;
    private int moveSpd = 3;
    private float chargeMax = 2;
    private float chargeMaxTimer;

    protected override void OnUpdate()
    {
        if (chargePauseTimer > 0)
        {
            chargePauseTimer -= Time.deltaTime;
        }
        else
        {
            sc.transform.position += moveSpd * Time.deltaTime * sc.transform.forward;
            chargeMax -= Time.deltaTime;
            if (chargeMaxTimer < 0)
            {
                bsc.ChangeState(bsc.ChaseState);
            }
        }
    }

    protected override void OnEnter()
    {
        base.OnEnter();

        Vector3 chargeTarget = new Vector3(bsc.player.transform.position.x, bsc.transform.position.y, bsc.player.transform.position.z);
        sc.transform.LookAt(chargeTarget);
        chargePauseTimer = chargePause;
        chargeMaxTimer = chargeMax;

        bsc.navMeshAgent.enabled = false;
    }
}
