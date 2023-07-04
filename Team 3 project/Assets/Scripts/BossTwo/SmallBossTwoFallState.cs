using UnityEngine;

public class SmallBossTwoFallState : State
{
    SmallBossTwoStateController bsc;
    Transform transform;
    Ray ray;
    RaycastHit hitData;
    float hitDistance;

    private float grav = 5;


    protected override void OnEnter()
    {
        bsc = (SmallBossTwoStateController)sc;
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
