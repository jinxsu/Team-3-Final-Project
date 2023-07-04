using UnityEngine;

public class SmallBossTwoDeathState : State
{
    float deathCountdown = 3f;
    SmallBossTwoStateController bsc;

    protected override void OnEnter()
    {
        bsc = (SmallBossTwoStateController)sc;
    }

    protected override void OnUpdate()
    {
        deathCountdown -= Time.deltaTime;
        if (deathCountdown < 0)
        {
            bsc.destroyMe = true;
        }
    }
}
