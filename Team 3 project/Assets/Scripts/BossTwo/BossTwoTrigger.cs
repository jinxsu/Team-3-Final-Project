using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTwoTrigger : MonoBehaviour
{
    public GameObject boss;
    BigBossTwoStateController bossController;

    private void Start()
    {
        bossController = boss.GetComponent<BigBossTwoStateController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (boss != null)
        {
            bossController.ChangeState(bossController.ChaseState);
            Destroy(gameObject);
        }
    }
}
