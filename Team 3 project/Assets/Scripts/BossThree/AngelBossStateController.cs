using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.AI;

public class AngelBossStateController : StateController
{

    public AngelBossChaseState ChaseState = new();
    public AngelBossRespawnState RespawnState = new();
    public AngelBossSlowState SlowState = new();
    public AngelBossDeathState DeathState = new();

    public NavMeshAgent navMeshAgent;

    [SerializeField]
    private int startHealth;

    
    public Transform respawnPoint;


    public GameObject player;

    public bool visionDamage;

    Ray ray;
    

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        navMeshAgent = GetComponent<NavMeshAgent>();
        hp = startHealth;
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

        }
        //if the boss hits the ground, this does proc over and over as the boss is walking around
        else if (collision.gameObject.layer == 6)
        {
            Debug.Log("Collided with  ground");
        }

        //if the boss hits the player
        else if (collision.gameObject.CompareTag("Player"))
        {
            player.GetComponent<PlayerControllerScript>().HurtPlayer();
        }
    }

    void OnTriggerEnter(Collider other)
    {

        //if the boss hits a trap's trigger zone this is what will be used to determine damage
        if (other.transform.CompareTag("Trap"))
        {
            ChangeState(RespawnState);
            Debug.Log("OW! A TRAP");
        }
    }

    public override void BossHitByRay()
    {
        ChangeState(SlowState);
    }

    private void OnWillRenderObject()
    {
        RaycastHit hitData;
        Vector3 dir = (transform.position - player.transform.position).normalized;
        ray = new Ray(transform.position, dir);

        Physics.Raycast(transform.position, dir, out hitData);

        Debug.Log(hitData.transform.tag);

        Debug.DrawLine(transform.position, hitData.transform.position);
    }

}


