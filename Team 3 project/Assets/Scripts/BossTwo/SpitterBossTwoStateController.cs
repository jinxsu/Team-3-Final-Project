using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpitterBossTwoStateController : StateController
{

    SpitterBossTwoChaseState ChaseState = new SpitterBossTwoChaseState();

    SpitterBossTwoSpitState SpitState = new SpitterBossTwoSpitState();

    public NavMeshAgent navMeshAgent;

    public GameObject player;

    [SerializeField]
    int startHealth = 10;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        hp = startHealth;
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
    }

    
}
