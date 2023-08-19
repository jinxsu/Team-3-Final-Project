using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LighterTrap : MonoBehaviour
{
    [SerializeField]
    GameObject trap;

    [SerializeField]
    ParticleSystem fire;

    [SerializeField]
    GameObject blocker;

    bool trapActive;
    
    public bool angelIsIn;

    public bool playerIsIn;

    public AudioSource audio;
    public AudioClip burningSound;

    PlayerControllerScript player;



    // Start is called before the first frame update
    void Start()
    {
        trap.SetActive(false);
        trapActive = true;
        fire.Stop();
        blocker.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerIsIn && GameObject.Find("Lighter Variant(Clone)") && trapActive && angelIsIn)
        {
            Debug.Log("LIGHTER!!!!");
            player.intString = "Light it up NOW!";
            if (player.controls.Player.Fire.triggered)
            {
                trap.SetActive(true);
                fire.Play();
                audio.PlayOneShot(burningSound);
                blocker.SetActive(true);
                trapActive = false;
                player.canInteract = false;
                GetComponent<BoxCollider>().enabled = false;
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsIn = true;
            player = other.GetComponent<PlayerControllerScript>();
            player.canInteract = true;
            player.intString = "I could light it from here!";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsIn = false;
            player.canInteract = false;
            player = null;
        }
    }
}
