using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Playables;

public class BossCutsceneScript : MonoBehaviour
{
    private PlayerControllerScript player;
    private PlayableDirector cutscene;
    public StateController boss;
    [SerializeField] GameObject cutsceneCam;

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerControllerScript>();
        boss = GameObject.FindWithTag("Boss").GetComponent<StateController>();
        cutscene = GetComponent<PlayableDirector>();
    }

    private void OnTriggerEnter(Collider other)
    {
        player.inCutscene = true;
        cutsceneCam.SetActive(true);
        cutscene.Play();
    }

    public void EndCutscene()
    {
        GetComponent<BoxCollider>().enabled = false;
        player.inCutscene = false;
        cutsceneCam.SetActive(false);

        if(boss.TryGetComponent(out BigBossTwoStateController bigboss))
        {
            bigboss.ChangeState(bigboss.ChaseState);
        }
        else if(boss.TryGetComponent(out BoarBossStateController boarboss))
        {
            boarboss.ChangeState(boarboss.ChargeState);
        }
    }
}