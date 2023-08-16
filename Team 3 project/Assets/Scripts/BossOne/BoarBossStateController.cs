using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BoarBossStateController : StateController
{
    public State PatrolState = new BoarPatrol();

    public State RecoilState = new BoarRecoil();
    
    public State ChargeState = new BoarCharge();

    public State FallState = new BoarFall();
    
    public State DeathState = new BoarDeath();

    public State IdleState = new BoarIdle();

    public Animator animator;

    public int startHealth;

    public NavMeshAgent navMeshAgent;

    public AudioSource chargeWarning;
    public AudioClip chargeWarningClip;

    [SerializeField] private SkinnedMeshRenderer skinnedMesh;
    [SerializeField] private MeshRenderer valveMesh;

    [SerializeField] private List<Material> materials;

    [SerializeField] private float dissolveRate = 0.0125f;
    [SerializeField] private float refreshRate = 0.025f;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        hp = startHealth;
    }

    private new void Update()
    {
        base.Update();
        
        if (hp <= 0 && currentState != DeathState)
        {
            ChangeState(DeathState);
            StartCoroutine(BoarBossDies());
        }

        if (destroyMe)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        ChangeState(IdleState);

        if(skinnedMesh != null)
        {
            materials.Add(skinnedMesh.materials[0]);
        }
        
        if(valveMesh != null)
        {
            materials.Add(valveMesh.material);
        }
    } 

    public override void BossHitByRay()
    {
        if (currentState is BoarVulnerable )
        {
            chargeWarning.PlayOneShot(chargeWarningClip);
            ChangeState(ChargeState);
        }
    }

    private IEnumerator BoarBossDies()
    {
        if(materials.Count > 0)
        {
            float counter = 0f;
            while (materials[0].GetFloat("_DissolveAmount") < 1)
            {
                counter += dissolveRate;
                for (int i = 0;  i < materials.Count; i++)
                {
                    materials[i].SetFloat("_DissolveAmount", counter);
                }
                yield return new WaitForSeconds(refreshRate);
            }
        }
    }
}
