using UnityEngine;

public class ButtonTrapScript : MonoBehaviour
{
    [SerializeField] private GameObject trapToSpawn;
    [SerializeField] private GameObject spawnLocation;
    [SerializeField] private float trapTimer;
    [SerializeField] private bool canBreak;
    [SerializeField] private BoxCollider interactCollider;
    private bool isBroken;
    public float trapTime;
    public GameObject spawnedTrap;
    
    private PlayerControllerScript player;
    bool playerDetected = false;
    bool buttonEnabled = true;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        interactCollider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {

        //Checks if the player is inside the trigger zone that allows them to press the button. 
        if(other.gameObject.CompareTag("Player"))
        {
            //This fetches the player's inputcontrols componenet so that the script can check if the interact button is pressed.
            //Placing this in the Update function caused consistency issues
            player = other.gameObject.GetComponent<PlayerControllerScript>();
            playerDetected = true;
            player.intString = "activate trap";
            player.canInteract = true;
        }
    }

    private void Update()
    {
        if (!isBroken)
        {
            if (playerDetected)
            {

                if (player.controls.Player.Interact.triggered && buttonEnabled)
                {

                    //Spawns an instance of a trap prefab at the position of the spawnLocation gameobject. 
                    //This allows for both the location of the trap to change and which trap is spawned to change as well
                    spawnedTrap = Instantiate(trapToSpawn, spawnLocation.transform.position, spawnLocation.transform.rotation);
                    trapTime = trapTimer;
                    animator.SetBool("enabled", false);
                    animator.SetTrigger("pressed");
                }
            }
        }
        
        if(spawnedTrap == null && trapTime > 0 && canBreak)
        {
            isBroken = true;
            interactCollider.enabled = false;

            if(player != null)
            {
                ResetPlayer();
            }
        }

        //The trap spawned despawns after some time. Also disables the button so that more traps can't be spawned while one is out
        if (trapTime > 0)
        {
            trapTime -= Time.deltaTime;
            buttonEnabled = false;
        }
        if (trapTime < 0)
        {
            if(spawnedTrap != null)
            {
                Destroy(spawnedTrap);
                spawnedTrap = null;
            }

            if(!isBroken)
            {
                buttonEnabled = true;
                animator.SetBool("enabled", true);
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ResetPlayer();
        }
    }

    private void ResetPlayer()
    {
        player.intString = "";
        player.canInteract = false;
        player = null;
        playerDetected = false;
    }



}
