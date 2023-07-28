using UnityEngine;
using UnityEngine.AI;

public class SpitterBossTwoStateController : StateController
{

    public SpitterBossTwoChaseState ChaseState = new SpitterBossTwoChaseState();

    public SpitterBossTwoSpitState SpitState = new SpitterBossTwoSpitState();

    public SpitterBossTwoDeathState DeathState = new SpitterBossTwoDeathState();

    public GameObject bossSpitProjectile;

    public GameObject spitPoint;

    public NavMeshAgent navMeshAgent;

    public GameObject player;

    [SerializeField]
    int startHealth;

    float spitterHealth;


    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        spitterHealth = startHealth;
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
        Debug.Log(spitterHealth);
        spitterHealth -= Time.deltaTime;

        if ( spitterHealth < 0 && currentState != DeathState)
        {
            ChangeState(DeathState);
        }

        if (destroyMe)
        {
            Destroy(gameObject);
        }
    }

    
}
