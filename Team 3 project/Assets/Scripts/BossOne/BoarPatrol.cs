using UnityEngine;

public class BoarPatrol : BoarVulnerable
{
    //This is the boss' main state where it chases the player

    protected GameObject target;

    
    
    // Start is called before the first frame update
    protected override void OnUpdate()
    {
        
        bsc.navMeshAgent.destination = target.transform.position;

    }

    protected override void OnEnter()
    {
        //BoarVulnerable OnEnter
        base.OnEnter();

        // Initiation of variables for this state
        target = GameObject.FindWithTag("Player");
        Debug.Log("Chase State");

        //re-enabling navmesh agent after a charge
        bsc.navMeshAgent.enabled = true;
        bsc.navMeshAgent.ResetPath();

    }


}
