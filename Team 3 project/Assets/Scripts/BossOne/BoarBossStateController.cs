using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarBossStateController : StateController
{
    public State PatrolState = new BoarPatrol();

    public State RecoilState = new BoarRecoil();
    
    public State ChargeState = new BoarCharge();

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
