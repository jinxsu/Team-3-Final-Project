using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class BoarBossCollisions : MonoBehaviour
{
    //This script is to handle the collisions with the boar boss


    [SerializeField] BoarBossStateController bsc;
    [SerializeField] LayerMask groundMask;

    //Checking for Bullet hit, will always override chase and update charge
    public void OnCollisionEnter(Collision collision)
    {
        //If a bullet hits the boss
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

        //if the boss hits a wall 
        else if (collision.gameObject.layer == 8)
        {
            Debug.Log("Collided with not Bullet or ground");

            bsc.ChangeState(bsc.FallState);

        }
        //if the boss hits the ground, this does proc over and over as the boss is walking around
        else if (collision.gameObject.layer == 6)
        {
            Debug.Log("Collided with  ground");
            if (bsc.CheckCurrentState() == bsc.FallState)
            {
                bsc.ChangeState(bsc.PatrolState);
            }
          //  bsc.ChangeState(bsc.RecoilState);
        }

        //if the boss hits the player
        else if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Collided with  player");
            bsc.ChangeState(bsc.FallState);
        }
    }


    //Checking for trap trigger, that will be bigger than the trap collider
    void OnTriggerEnter(Collider other)
    {

        //if the boss hits a trap's trigger zone this is what will be used to determine damage
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
