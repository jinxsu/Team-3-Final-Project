using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarRecoil : State
{
    BoarBossStateController bsc;
    protected override void OnEnter()
    {
        // Code placed here can be overridden
        bsc = (BoarBossStateController)sc;

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
