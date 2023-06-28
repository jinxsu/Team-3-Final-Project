using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour
{
    protected State currentState;
    public int hp;

    // Update is called once per frame
    protected void Update()
    {
        if (currentState != null)
        {
            currentState.OnStateUpdate();
        }
    }
    public void ChangeState(State newState)
    {
        if (currentState != null)
        {
            currentState.OnStateExit();
        }
        currentState = newState;
        currentState.OnStateEnter(this);
    }

    public State CheckCurrentState()
    {
        return currentState;
    }

    public virtual void BossHitByRay()
    {
        Debug.Log("I was hit by a ray, but this hasn't been overriden.");
    }
}
