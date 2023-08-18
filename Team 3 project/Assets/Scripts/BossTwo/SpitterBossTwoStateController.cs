using System.Collections;
using System.Collections.Generic;
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
    
    public Animator animator;

    [SerializeField]
    int startHealth;

    float spitterHealth;

    [SerializeField] private SkinnedMeshRenderer skinnedMesh;
    [SerializeField] private List<Material> materials;

    [SerializeField] private float dissolveRate = 0.0125f;
    [SerializeField] private float refreshRate = 0.025f;


    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator=GetComponentInChildren<Animator>();
        spitterHealth = startHealth;
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
            StartCoroutine(SpitterBossDies());
        }

        if (destroyMe)
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator SpitterBossDies()
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
