using UnityEngine;
using UnityEngine.AI;

public class SmallBossTwoStateController : StateController
{
    public SmallBossTwoChargeState ChargeState = new SmallBossTwoChargeState();

    public SmallBossTwoChaseState ChaseState = new SmallBossTwoChaseState();

    public SmallBossTwoRecoilState RecoilState = new SmallBossTwoRecoilState();

    public SmallBossTwoDeathState DeathState = new SmallBossTwoDeathState();

    public SmallBossTwoFallState FallState = new SmallBossTwoFallState();

    public NavMeshAgent navMeshAgent;

    public GameObject player;

    [SerializeField]
    int startHealth = 10;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        hp = startHealth;
        player = GameObject.FindWithTag("Player");

    }

    // Start is called before the first frame update
    void Start()
    {
        ChangeState(ChaseState);
    }

    // Update is called once per frame
    new void Update()
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

    public void OnCollisionEnter(Collision collision)
    {
        //if the boss hits a wall 
        if (collision.gameObject.layer == 8)
        {
            
            Debug.Log("Collided with not Bullet or ground");

            if (collision.gameObject.GetComponent<TrapWall>() != null)
            {
                collision.gameObject.transform.SendMessage("HitByBoss");
            }
            if (currentState == RecoilState)
            {
                ChangeState(ChaseState);
            }
            else
            {
                ChangeState(FallState);
            }
            

        }



        //if the boss hits the ground, this does proc over and over as the boss is walking around
        else if (collision.gameObject.layer == 6)
        {
            Debug.Log("Collided with  ground");
            if (currentState == FallState || currentState == ChargeState)
            {
                ChangeState(ChaseState);
            }
        }

        //if the boss hits the player
        else if (collision.gameObject.CompareTag("Player"))
        {
            player.GetComponent<PlayerControllerScript>().HurtPlayer();
            ChangeState(FallState);
        }
    }

    void OnTriggerEnter(Collider other)
    {

        //if the boss hits a trap's trigger zone this is what will be used to determine damage
        if (other.transform.CompareTag("Trap"))
        {
            if (currentState is SmallBossTwoVulnerable)
            {
                ChangeState(RecoilState);
                Debug.Log("OW! A TRAP");
            }
        }
    }

    public override void BossHitByRay()
    {
        if (currentState is SmallBossTwoVulnerable)
        {
            Debug.Log("Small boss hit");
            ChangeState(ChargeState);
        }
    }
}
