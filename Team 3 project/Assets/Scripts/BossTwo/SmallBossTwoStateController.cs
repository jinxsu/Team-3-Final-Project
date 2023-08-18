using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SmallBossTwoStateController : StateController
{
    public SmallBossTwoChargeState ChargeState = new SmallBossTwoChargeState();

    public SmallBossTwoChaseState ChaseState = new SmallBossTwoChaseState();

    public SmallBossTwoRecoilState RecoilState = new SmallBossTwoRecoilState();

    public SmallBossTwoDeathState DeathState = new SmallBossTwoDeathState();

    public SmallBossTwoFallState FallState = new SmallBossTwoFallState();

    public Animator animator;

    public NavMeshAgent navMeshAgent;

    public GameObject player;

    [SerializeField]
    int startHealth = 10;

    [SerializeField] private SkinnedMeshRenderer skinnedMesh;
    [SerializeField] private MeshRenderer valveMesh;

    [SerializeField] private List<Material> materials;

    [SerializeField] private float dissolveRate = 0.0125f;
    [SerializeField] private float refreshRate = 0.025f;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator=GetComponentInChildren<Animator>();
        hp = startHealth;
        player = GameObject.FindWithTag("Player");

    }

    // Start is called before the first frame update
    void Start()
    {
        ChangeState(ChaseState);

        if (skinnedMesh != null)
        {
            materials.Add(skinnedMesh.materials[0]);
        }

        if (valveMesh != null)
        {
            materials.Add(valveMesh.material);
        }
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
        if (hp <= 0 && currentState != DeathState)
        {
            ChangeState(DeathState);
            StartCoroutine(SmallBossDies());
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

    private IEnumerator SmallBossDies()
    {
        if (materials.Count > 0)
        {
            float counter = 0f;
            while (materials[0].GetFloat("_DissolveAmount") < 1)
            {
                counter += dissolveRate;
                for (int i = 0; i < materials.Count; i++)
                {
                    materials[i].SetFloat("_DissolveAmount", counter);
                }
                yield return new WaitForSeconds(refreshRate);
            }
        }
    }
}
