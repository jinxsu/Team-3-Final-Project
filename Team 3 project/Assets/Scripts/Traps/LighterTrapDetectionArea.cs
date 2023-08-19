using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LighterTrapDetectionArea : MonoBehaviour
{
    [SerializeField]
    LighterTrap lighterTrap;
    private PlayerControllerScript player;

    private void Awake()
    {
        lighterTrap = GetComponentInParent<LighterTrap>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boss")) lighterTrap.angelIsIn = true;

        if(other.CompareTag("Player"))
        {
            player = other.GetComponent<PlayerControllerScript>();
            player.canInteract = true;
            player.intString = "This house looks flammable...";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Boss")) lighterTrap.angelIsIn = false;

        if (other.CompareTag("Player"))
        {
            player.canInteract = false;
            player = null;
        }
    }

    private void Update()
    {
        if(player != null)
        {
            if (GameObject.Find("Lighter Variant(Clone)") && player.controls.Player.Fire.triggered)
            {
                if (lighterTrap.angelIsIn) 
                    player.intString = "I can't do that now! I'll burn myself!";
                else 
                    player.intString = "That thing isn't here yet! I'll set up a trap.";
            }
        }
    }
}
