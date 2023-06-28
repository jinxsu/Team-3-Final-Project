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
}
