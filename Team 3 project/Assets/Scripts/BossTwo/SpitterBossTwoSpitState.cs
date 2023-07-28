using UnityEngine;

public class SpitterBossTwoSpitState : State
{
    SpitterBossTwoStateController bsc;

    float spitTimer = 1.5f;
    float spitTime;
    int forwardMomentum = 1200;
    int upwardMomentum = 70;
    
    protected override void OnEnter()
    {
        bsc = (SpitterBossTwoStateController)sc;
        bsc.navMeshAgent.enabled = false;

        spitTime = spitTimer;
    }

    protected override void OnUpdate()
    {

        bsc.transform.LookAt(bsc.player.transform);

        if (spitTime > 0)
        {
            spitTime -= Time.deltaTime;
        }
        else
        {
            Debug.Log("I SPIT AT YOU");
            var spitProjectile = Object.Instantiate(bsc.bossSpitProjectile, bsc.spitPoint.transform.position, bsc.spitPoint.transform.rotation);
            spitProjectile.GetComponent<Rigidbody>().AddForce(spitProjectile.transform.forward * forwardMomentum + new Vector3(0, upwardMomentum, 0));
            //check if playing has left the spitting range, leaving needs the player to be further away than they need to be to enter the spitting range
            if ((bsc.player.transform.position - bsc.transform.position).magnitude > 35)
            {
                bsc.ChangeState(bsc.ChaseState);
            }
            else
            {
                spitTime = spitTimer;
            }
        }


        
    }

}
