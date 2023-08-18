using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LighterTrap : MonoBehaviour
{
    [SerializeField]
    GameObject trap;

    bool trapActive;
    
    public bool angelIsIn;

    public bool playerIsIn;

    ParticleSystem fire;

    PlayerControllerScript player;


    // Start is called before the first frame update
    void Start()
    {
        trap.SetActive(false);
        trapActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerIsIn && GameObject.Find("Lighter Variant(Clone)") && trapActive && angelIsIn)
        {
            Debug.Log("LIGHTER!!!!");
            if (player.controls.Player.Interact.triggered)
            {
                trap.SetActive(true);
                trapActive = false;
            }
        }
        
    }



    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")) playerIsIn = true;
        player = other.GetComponent<PlayerControllerScript>();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) playerIsIn = false;

        player = null;
    }
}