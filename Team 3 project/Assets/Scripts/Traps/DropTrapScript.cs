using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DropTrapScript : MonoBehaviour
{
    [SerializeField]
    GameObject dropTrigger;

    [SerializeField]
    GameObject trapDoor;

    [SerializeField]
    Transform trapdoorTarget;

    [SerializeField]
    GameObject trapDoorStart;

    PlayerControllerScript player;

    bool playerIsIn;

    bool movingTrapdoor;

    bool trapdoorMoved;

    public bool droppedAngel;

    float resetTimer;

    NavMeshObstacle obstacle;

    //This is the timer for the carve of the navmesh. Without a timer, the boss gets warped to the edge of the trap and it looks bad
    float carveTimer;

    bool carveIncoming;

    private void Start()
    {
        dropTrigger.SetActive(false);
        obstacle = dropTrigger.GetComponent<NavMeshObstacle>();
        obstacle.enabled = false;
        carveIncoming = false;
        carveTimer = 0.5f;
        resetTimer = 2f;
    }

    private void Update()
    {
        if (playerIsIn && player.controls.Player.Interact.triggered)
        {
            dropTrigger.SetActive(true);
            Debug.Log("BUtton PusHED");
            movingTrapdoor = true;
            resetTimer = 2f;
        }

        //This will need to be modified when implemented because the axis that moves will change depending on the direction of the trap
        if(movingTrapdoor)
        {
            
            if (trapDoor.transform.position.x < trapdoorTarget.position.x)
            {
                trapDoor.transform.position = new Vector3(trapDoor.transform.position.x + 0.1f, trapDoor.transform.position.y, trapDoor.transform.position.z);
            }
            else
            {
                trapdoorMoved = true;
                movingTrapdoor = false;
            }
        }

        if (carveIncoming)
        {
            carveTimer -= Time.deltaTime;
            if (carveTimer < 0)
            {
                obstacle.enabled = true;
                carveIncoming = false;
                carveTimer = 0.5f;
            }
        }

        if (trapdoorMoved && !droppedAngel)
        {
            if (resetTimer > 0)
            {
                resetTimer -= Time.deltaTime;
                return;
            }

            obstacle.enabled = false;
            dropTrigger.SetActive(false);

            if (trapDoor.transform.position.x > trapDoorStart.transform.position.x)
            {
                trapDoor.transform.position = new Vector3(trapDoor.transform.position.x - 0.1f, trapDoor.transform.position.y, trapDoor.transform.position.z);
            }
            else
            {
                trapdoorMoved = false;
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) playerIsIn = true;
        player = other.GetComponent<PlayerControllerScript>();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) playerIsIn = false;

        player = null;
    }

}
