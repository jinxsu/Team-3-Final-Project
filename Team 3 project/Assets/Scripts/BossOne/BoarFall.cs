using UnityEngine;

public class BoarFall : State
{
    // Start is called before the first frame update

    BoarBossStateController bsc;
    Transform transform;
    Ray ray;
    RaycastHit hitData;
    float hitDistance;

    private float grav = 5;

    protected override void OnEnter()
    {
        bsc = (BoarBossStateController)sc; 
        base.OnEnter();
        transform = bsc.transform;
        ray = new Ray(transform.position, (-1 * transform.up));
        Debug.Log("Falling enter");
        
        
        Physics.Raycast(ray,out hitData);
        hitDistance = hitData.distance;
        if (hitDistance < 3)
        {
            bsc.ChangeState(bsc.RecoilState);
        }
    }

    // Update is called once per frame
    protected override void OnUpdate()
    {
        sc.transform.position -= grav * Time.deltaTime * sc.transform.up;
    }
}
