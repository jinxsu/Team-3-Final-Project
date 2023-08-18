using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BigBossTwoStateController : StateController
{
    public BigBossTwoChargeState ChargeState = new BigBossTwoChargeState();

    public BigBossTwoChaseState ChaseState = new BigBossTwoChaseState();

    public BigBossTwoDeathState DeathState = new BigBossTwoDeathState();

    public BigBossTwoFallState FallState = new BigBossTwoFallState();

    public BigBossTwoRecoilState RecoilState = new BigBossTwoRecoilState();

    public BigBossTwoStompState StompState = new BigBossTwoStompState();

    public BigBossTwoIdle IdleState = new BigBossTwoIdle();

    public NavMeshAgent navMeshAgent;

    public GameObject player;

    public GameObject stompObject;

    public GameObject spitterBoss;
    public GameObject spitterBossSpawn;

    public GameObject smallBoss;
    public GameObject smallBossSpawn;
    public Animator animator;
    public AudioSource audio;
    public AudioClip monsterHurtSound;
    public AudioClip monsterSlamSound;

    [SerializeField]
    int startHealth = 10;

    [SerializeField] private SkinnedMeshRenderer[] skinnedMeshes;
    [SerializeField] private List<Material> materials;

    [SerializeField] private float dissolveRate = 0.0125f;
    [SerializeField] private float refreshRate = 0.025f;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        hp = startHealth;
    }


    // Start is called before the first frame update
    void Start()
    {
        ChangeState(IdleState);

        if(skinnedMeshes.Length > 0)
        {
            foreach(SkinnedMeshRenderer skinnedMesh in skinnedMeshes)
            {
                materials.Add(skinnedMesh.materials[0]);
            }
        }
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();

        if (hp <= 0 && currentState != DeathState)
        {
            animator.SetBool("charge", false);
            animator.SetBool("chase", false);
            ChangeState(DeathState);
            StartCoroutine(BigBossDies());
        }

        if(destroyMe)
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

            animator.SetBool("charge", false);
            animator.SetBool("chase", false);
            ChangeState(FallState);

        }
        //if the boss hits the ground, this does proc over and over as the boss is walking around
        else if (collision.gameObject.layer == 6)
        {
            Debug.Log("Collided with  ground");
            if (currentState == FallState || currentState == ChargeState)
            {
                animator.SetBool("chase", true);
                ChangeState(ChaseState);
            }
        }

        //if the boss hits the player
        else if (collision.gameObject.CompareTag("Player"))
        {
            animator.SetBool("charge", false);
            animator.SetBool("chase", false);
            player.GetComponent<PlayerControllerScript>().HurtPlayer();
            ChangeState(FallState);
        }
    }

    void OnTriggerEnter(Collider other)
    {

        //if the boss hits a trap's trigger zone this is what will be used to determine damage
        if (other.transform.CompareTag("Trap"))
        {
            if (currentState is BigBossTwoVulnerable)
            {
                animator.SetBool("charge", false);
                animator.SetBool("chase", false);
                ChangeState(RecoilState);
                audio.PlayOneShot(monsterHurtSound);
                Debug.Log("OW! A TRAP");
            }
        }
    }

    public override void BossHitByRay()
    {
        animator.SetBool("charge", true);
        audio.PlayOneShot(monsterSlamSound);
        ChangeState(ChargeState);
    }

    private IEnumerator BigBossDies()
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

    public void SpawnBosses()
    {
        Instantiate(spitterBoss, spitterBossSpawn.transform.position, spitterBossSpawn.transform.rotation);
        Instantiate(smallBoss, smallBossSpawn.transform.position, smallBossSpawn.transform.rotation);
    }
}
