using UnityEngine;

public class BigBossTwoChargeState : BigBossTwoVulnerable
{
    private float chargePause = 2f;
    private float chargePauseTimer;
    private int moveSpd = 20;
    private float chargeMax = 10f;
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
            if(chargeMaxTimer < 0 )
            {
                bsc.animator.SetBool("charge", false);
                bsc.animator.SetBool("chase", true);
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
