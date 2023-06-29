using UnityEngine.AI;

public class BoarBossStateController : StateController
{
    public State PatrolState = new BoarPatrol();

    public State RecoilState = new BoarRecoil();
    
    public State ChargeState = new BoarCharge();

    public State FallState = new BoarFall();
    
    public State DeathState = new BoarDeath();

    public int startHealth;

    public bool destroyMe = false;

    public NavMeshAgent navMeshAgent;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        hp = startHealth;
    }

    private new void Update()
    {
        base.Update();
        
        if (hp <= 0 && currentState != DeathState)
        {
            ChangeState(DeathState);
        }

        if (destroyMe)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        ChangeState(PatrolState);
    }

    

    public override void BossHitByRay()
    {
        if (currentState is BoarVulnerable )
        {
            ChangeState(ChargeState);
        }
    }
}
