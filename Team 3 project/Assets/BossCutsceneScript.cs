using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Playables;

public class BossCutsceneScript : MonoBehaviour
{
    private PlayerControllerScript player;
    private NavMeshAgent boss;
    private PlayableDirector cutscene;
    [SerializeField] GameObject cutsceneCam;

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerControllerScript>();
        boss = GameObject.FindWithTag("Boss").GetComponent<NavMeshAgent>();
        cutscene = GetComponent<PlayableDirector>();
        boss.isStopped = true;
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
        boss.isStopped = false;
    }
}
