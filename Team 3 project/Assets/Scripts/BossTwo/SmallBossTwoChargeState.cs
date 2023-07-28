using UnityEngine;

public class SmallBossTwoChargeState : SmallBossTwoVulnerable
{
    private float chargePause = 1f;
    private float chargePauseTimer;
    private int moveSpd = 14;
    private float chargeMax = 1.5f;
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
            chargeMaxTimer -= Time.deltaTime;
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

        Debug.Log("Charge enter. Charge pause " + chargePauseTimer + ". Charge max " + chargeMaxTimer);

        bsc.navMeshAgent.enabled = false;
    }
}
