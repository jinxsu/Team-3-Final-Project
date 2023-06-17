using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossTwoStateController : StateController
{

    public NavMeshAgent navMeshAgent;

    public State CheckCurrentState()
    {
        return currentState;
    }
}
