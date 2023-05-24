using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarVulnerable : State
{
    protected BoarBossStateController bsc;


    protected override void OnEnter()
    {
        // Code placed here can be overridden
        bsc = (BoarBossStateController)sc;
        Debug.Log("Vulnerable Enter");
    }


    // Start is called before the first frame update
    protected override void  OnHurt()
    {
        // Code placed here can be overridden

        
    }


    //Checking for Bullet hit, will always override chase and update charge
    public void OnCollisionEnter(Collision collision)
    {
       
    }
}
