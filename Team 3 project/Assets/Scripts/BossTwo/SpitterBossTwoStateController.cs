using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpitterBossTwoStateController : StateController
{

    public SpitterBossTwoChaseState ChaseState = new SpitterBossTwoChaseState();

    public SpitterBossTwoSpitState SpitState = new SpitterBossTwoSpitState();

    public SpitterBossTwoDeathState DeathState = new SpitterBossTwoDeathState();

    public GameObject bossSpitProjectile;

    public NavMeshAgent navMeshAgent;

    public GameObject player;


    int startHealth = 60;

    float spitterHealth;


    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        spitterHealth = startHealth;
        player = GameObject.FindWithTag("Player");
    }

    // Start is called before the first frame update
    void Start()
    {
        ChangeState(ChaseState);
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
        spitterHealth -= Time.deltaTime;

        if ( spitterHealth < 0 )
        {
            ChangeState(DeathState);
        }

        if (destroyMe)
        {
            Destroy(gameObject);
        }
    }

    
}
