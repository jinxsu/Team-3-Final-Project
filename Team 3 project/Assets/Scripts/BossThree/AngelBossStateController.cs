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
    public AngelBossDropState DropState = new();

    public NavMeshAgent navMeshAgent;

    
    private int startHealth = 3;

    
    public Transform respawnPoint;


    public GameObject player;

    public bool visionDamage;

    public LayerMask layerMask;

    StaticEffectScript effectScript;

    public float fastSpeed;
    
    public float slowSpeed;

    Ray ray;

    bool potentiallyVisible;

    float visionCounter = 0f;
    public Animator animator;
    public AudioSource scream;
    public AudioClip screamClip;
    

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        navMeshAgent = GetComponent<NavMeshAgent>();
        hp = startHealth;
        effectScript = player.GetComponentInChildren<StaticEffectScript>();
        fastSpeed = 6f;
        slowSpeed = 2.5f;
        animator=GetComponentInChildren<Animator>();
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

        if(potentiallyVisible)
        {
            if (Physics.Linecast(transform.position, player.transform.position, layerMask))
            {
                effectScript.isStatic = false;
                visionDamage = false;
            }
            else
            {
                effectScript.isStatic = true;
                visionDamage = true;
            }
        }

        if(visionDamage)
        {
            visionCounter += Time.deltaTime;
        }
        else
        {
            if (visionCounter > 0)
            {
                visionCounter -= Time.deltaTime;
            }
        }

        if (visionCounter > 20)
        {
            player.GetComponent<PlayerControllerScript>().KillPlayer();
            visionCounter = 0;
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
            player.GetComponent<PlayerControllerScript>().KillPlayer();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger detected. Tag : " + other.transform.tag);
        //if the boss hits a trap's trigger zone this is what will be used to determine damage
        if (other.transform.CompareTag("Trap"))
        {
            ChangeState(RespawnState);
            Debug.Log("OW! A TRAP");
        }

        if (other.transform.CompareTag("DropTrap"))
        {
            scream.PlayOneShot(screamClip);
            ChangeState(DropState);
            Debug.Log("AAH I FALL!");
        }
    }

    public override void BossHitByRay()
    {
        ChangeState(SlowState);
    }

    private void OnBecameVisible()
    {
        potentiallyVisible = true;
    }

    private void OnBecameInvisible()
    {
        potentiallyVisible = false;
        effectScript.isStatic = false;
        visionDamage = false;
    }
}


