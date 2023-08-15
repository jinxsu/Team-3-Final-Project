using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SmushTrapScript : MonoBehaviour
{

    [SerializeField]
    GameObject wallLeft;

    [SerializeField]
    GameObject wallRight;

    [SerializeField]
    GameObject trapTrigger;

    PlayerControllerScript player;

    public bool playerIsIn;

    public bool closingWalls;

    public bool squishedAngel;

    // Start is called before the first frame update
    void Start()
    {
        trapTrigger.SetActive(false);
        closingWalls = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerIsIn && player.controls.Player.Interact.triggered)
        {
            trapTrigger.SetActive(true);

            closingWalls = true;
        }

        if(closingWalls)
        {
            //This will need to be adjusted based on the direction of the trap
            int wallsDone = 0;
            if (wallLeft.transform.position.x < transform.position.x)
            {
                wallLeft.transform.position = new Vector3(wallLeft.transform.position.x + 0.1f, wallLeft.transform.position.y, wallLeft.transform.position.z);
            }
            else
            {
                wallsDone++;
            }

            if (wallRight.transform.position.x > transform.position.x)
            {
                wallRight.transform.position = new Vector3(wallRight.transform.position.x - 0.1f, wallRight.transform.position.y, wallRight.transform.position.z);
            }
            else
            {
                wallsDone++;
            }

            if (wallsDone == 2) closingWalls = false;
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
