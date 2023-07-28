using UnityEngine;

public class BigBossTwoChaseState : BigBossTwoVulnerable
{
    protected GameObject target;
    float proximityTimer;

    protected override void OnUpdate()
    {
        bsc.navMeshAgent.destination = target.transform.position;
        if ((target.transform.position - bsc.transform.position).magnitude < 15)
        {
            Debug.Log("Player close");
            proximityTimer -= Time.deltaTime;
            if (proximityTimer < 0 )
            {
                bsc.ChangeState(bsc.StompState);
            }
        }
        else
        {
            proximityTimer = 2f;
        }
    }

    protected override void OnEnter()
    {
        base.OnEnter();
        proximityTimer = 3f;
        target = bsc.player;

        bsc.navMeshAgent.enabled = true;
        bsc.navMeshAgent.ResetPath();
    }
}
