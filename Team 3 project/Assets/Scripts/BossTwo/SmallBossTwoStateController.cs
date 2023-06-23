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
}
