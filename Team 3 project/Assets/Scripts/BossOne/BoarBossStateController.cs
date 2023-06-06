using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BoarBossStateController : StateController
{
    public State PatrolState = new BoarPatrol();

    public State RecoilState = new BoarRecoil();
    
    public State ChargeState = new BoarCharge();

    public State FallState = new BoarFall();

    public NavMeshAgent navMeshAgent;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        ChangeState(PatrolState);
    }

    public State CheckCurrentState()
    {
        return currentState;
    }
}
