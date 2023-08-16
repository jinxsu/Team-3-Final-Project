using UnityEngine;

public class BoarRecoil : State
{

    //This is the state that happens after a boss either hits a trap, charges into a wall, or hits the player

    BoarBossStateController bsc;


    private float recoilStunTimer;
    private int moveSpd = 4;
    protected override void OnEnter()
    {
        // Initiation of variables for this state
        bsc = (BoarBossStateController)sc;
        recoilStunTimer = 2f;
        Debug.Log("Recoil State");
        bsc.navMeshAgent.enabled = false;
        bsc.animator.SetBool("chase", false);
    }

    protected override void OnUpdate()
    {
        //This makes the boss back up, it doesn't account for anything such as gravity or edges for now
        sc.transform.position -= moveSpd * Time.deltaTime * sc.transform.forward;
        recoilStunTimer -= Time.deltaTime;
        if (recoilStunTimer < 0)
        {
            bsc.ChangeState(bsc.PatrolState);
        }
    }

}
