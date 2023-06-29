using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SmallBossTwoStateController : StateController
{


    SmallBossTwoChargeState ChargeState = new SmallBossTwoChargeState();    

    SmallBossTwoChargeState ChaseState = new SmallBossTwoChargeState();

    SmallBossTwoRecoilState RecoilState = new SmallBossTwoRecoilState();

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
