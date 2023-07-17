using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpitProjectileScript : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerControllerScript>().HurtPlayer();
        }
        Destroy(gameObject);
    }
}
