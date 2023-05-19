using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarBossCollisions : MonoBehaviour
{
    [SerializeField] BoarBossStateController bsc;

    //Checking for Bullet hit, will always override chase and update charge
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "PistolBullet")
        {
            Destroy(collision.gameObject);

            //check if in vulnerable state (boarpatrol and boarcharge)
            if (bsc.CheckCurrentState() is BoarVulnerable)
            {
                bsc.ChangeState(bsc.ChargeState);

            }
            
            Debug.Log("OW HIT BY BULLET!");
        }
    }


    //Checking for trap trigger, that will be bigger than the trap collider
    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Trap")
        {
            if (bsc.CheckCurrentState() is BoarVulnerable)
            {
                bsc.ChangeState(bsc.RecoilState);

            }
        }
    }
}
