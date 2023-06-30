
using UnityEngine;

public class SmallBossTwoChaseState : SmallBossTwoVulnerable
{

    protected override void OnUpdate()
    {
        bsc.navMeshAgent.destination = bsc.player.transform.position;
    }

    protected override void OnEnter()
    {
        base.OnEnter();

        bsc.navMeshAgent.enabled = true;
        bsc.navMeshAgent.ResetPath();
    }
}
