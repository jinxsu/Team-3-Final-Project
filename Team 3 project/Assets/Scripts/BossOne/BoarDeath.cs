using UnityEngine;

public class BoarDeath : State
{
    float deathCountdown = 3f;
    BoarBossStateController bsc;

    protected override void OnEnter()
    {
        bsc = (BoarBossStateController)sc;
        bsc.animator.SetBool("chase", false);
    }

    protected override void OnUpdate()
    {
        deathCountdown -= Time.deltaTime;
        if (deathCountdown < 0 )
        {
            GameObject.FindGameObjectWithTag("EmergencyDoor").GetComponent<EmergencyDoorScript>().BossDefeated();
            bsc.destroyMe = true;
        }
    }
}

