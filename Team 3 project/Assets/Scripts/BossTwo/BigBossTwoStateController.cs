using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BigBossTwoStateController : StateController
{
    BigBossTwoChargeState ChargeState = new BigBossTwoChargeState();

    BigBossTwoChaseState ChaseState = new BigBossTwoChaseState();

    BigBossTwoDeathState DeathState = new BigBossTwoDeathState();

    BigBossTwoFallState FallState = new BigBossTwoFallState();

    BigBossTwoRecoilState RecoilState = new BigBossTwoRecoilState();

    public NavMeshAgent navMeshAgent;

    [SerializeField]
    int startHealth = 10;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        hp = startHealth;
    }


    // Start is called before the first frame update
    void Start()
    {
        ChangeState(ChaseState);
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }




    public void OnCollisionEnter(Collision collision)
    {
        //If a bullet hits the boss



        //if(bsc.CheckCurrentState() is BoarCharge)

        //if the boss hits a wall 
        if (collision.gameObject.layer == 8)
        {
            Debug.Log("Collided with not Bullet or ground");

            ChangeState(FallState);

        }
        //if the boss hits the ground, this does proc over and over as the boss is walking around
        else if (collision.gameObject.layer == 6)
        {
            Debug.Log("Collided with  ground");
            if (currentState == FallState || currentState == ChargeState)
            {
                ChangeState(ChaseState);
            }
            //  bsc.ChangeState(bsc.RecoilState);
        }

        //if the boss hits the player
        else if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Collided with  player");
            ChangeState(FallState);
        }
    }







    void OnTriggerEnter(Collider other)
    {

        //if the boss hits a trap's trigger zone this is what will be used to determine damage
        if (other.transform.tag == "Trap")
        {
            if (currentState is BoarVulnerable)
            {
                ChangeState(RecoilState);
                Debug.Log("OW! A TRAP");
            }
        }
    }

}
