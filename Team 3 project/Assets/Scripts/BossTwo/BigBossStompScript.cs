using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBossStompScript : MonoBehaviour
{
    float stompTimer;
    // Start is called before the first frame update
    void Start()
    {
        stompTimer = 0.75f;
    }

    // Update is called once per frame
    void Update()
    {
        stompTimer -= Time.deltaTime;

        if (stompTimer < 0 )
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<PlayerControllerScript>().HurtPlayer();
            Destroy(gameObject);
        }
    }
}
