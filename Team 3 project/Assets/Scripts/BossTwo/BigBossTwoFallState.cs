using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBossTwoFallState : State
{
    BigBossTwoStateController bsc;
    Transform transform;
    Ray ray;
    RaycastHit hitData;
    float hitDistance;

    private float grav = 5;

    protected override void OnEnter()
    {
        bsc = (BigBossTwoStateController)sc;
        base.OnEnter();
        transform = bsc.transform;
        ray = new Ray(transform.position, (-1 * transform.up));


        Physics.Raycast(ray, out hitData);
        hitDistance = hitData.distance;
        if (hitDistance < 3)
        {
            bsc.ChangeState(bsc.RecoilState);
        }
    }

    protected override void OnUpdate()
    {
        bsc.transform.position = grav * Time.deltaTime * bsc.transform.up;
    }
}
