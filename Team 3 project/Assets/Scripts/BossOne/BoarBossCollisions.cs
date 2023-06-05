using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class BoarBossCollisions : MonoBehaviour
{
    [SerializeField] BoarBossStateController bsc;
    [SerializeField] LayerMask groundMask;

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
        }
        

        //if(bsc.CheckCurrentState() is BoarCharge)
        else if (collision.gameObject.layer == 8)
        {
            Debug.Log("Collided with not Bullet or ground");

            bsc.ChangeState(bsc.RecoilState);

        }
        else if (collision.gameObject.layer == 6)
        {
            Debug.Log("Collided with  ground");
          //  bsc.ChangeState(bsc.RecoilState);
        }
        else if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Collided with  player");
            bsc.ChangeState(bsc.RecoilState);
        }
    }


    //Checking for trap trigger, that will be bigger than the trap collider
    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Trap" && bsc.CheckCurrentState() == bsc.ChargeState)
        {
            if (bsc.CheckCurrentState() is BoarVulnerable)
            {
                bsc.ChangeState(bsc.RecoilState);
                Debug.Log("OW! A TRAP");
            }
        }
    }
}
