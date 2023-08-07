using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class StarterCutsceneScript : MonoBehaviour
{
    private PlayerControllerScript player;
    [SerializeField] GameObject cutsceneCam;
    [SerializeField] GameObject cutscenePlayer;

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerControllerScript>();
        player.inCutscene = true;
    }

    public void EndCutscene()
    {
        player.inCutscene = false;
        cutsceneCam.SetActive(false);
        cutscenePlayer.SetActive(false);
    }
}
