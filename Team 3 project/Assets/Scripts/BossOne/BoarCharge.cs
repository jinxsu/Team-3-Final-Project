using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoarCharge : BoarVulnerable
{
    protected Vector3 target;
    private float chargePause = 3f;
    private float chargePauseTimer;
    private int moveSpd = 20;
    private float chargeMax = 10f;
    private float chargeMaxTimer;
    // Start is called before the first frame update
    protected override void OnUpdate()
    {
        // Code placed here can be overridden
        if(chargePauseTimer > 0)
        {
            chargePauseTimer -= Time.deltaTime;
        }
        else
        {
            sc.transform.position += sc.transform.forward * moveSpd * Time.deltaTime;
            chargeMaxTimer -= Time.deltaTime;
            if (chargeMaxTimer < 0)
            {
                bsc.ChangeState(bsc.PatrolState);
            }
        }
        
    }

    protected override void OnEnter()
    {
        base.OnEnter();
        target = GameObject.FindWithTag("Player").transform.position;
        Debug.Log("Charge State");

        sc.transform.LookAt(target);
        chargePauseTimer = chargePause;
        chargeMaxTimer = chargeMax;
    }

    protected override void OnExit()
    {
        // Code placed here can be overridden
    }


    
}
