using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

public class SmushTrapScript : MonoBehaviour
{

    [SerializeField]
    GameObject wallLeft;

    [SerializeField]
    GameObject wallRight;

    [SerializeField]
    GameObject trapTrigger;

    [SerializeField]
    GameObject wallTarget;

    [SerializeField]
    GameObject wallLeftStart;

    [SerializeField]
    GameObject wallRightStart;

    PlayerControllerScript player;

    public bool playerIsIn;

    public bool closingWalls;

    public bool squishedAngel;

    public int wallsDone = 0;

    public int wallsReset = 0;

    float resetTimer;

    // Start is called before the first frame update
    void Start()
    {
        trapTrigger.SetActive(false);
        closingWalls = false;
        resetTimer = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerIsIn && player.controls.Player.Interact.triggered && wallsDone == 0)
        {
            Debug.Log("Button Pushed");
            trapTrigger.SetActive(true);
            wallsReset = 0;
            closingWalls = true;
            resetTimer = 2f;
        }

        if(closingWalls)
        {
            //This will need to be adjusted based on the direction of the trap
            wallsDone = 0;
            if (wallLeft.transform.position.z > wallTarget.transform.position.z)
            {
                wallLeft.transform.position = new Vector3(wallLeft.transform.position.x, wallLeft.transform.position.y, wallLeft.transform.position.z - 0.06f);
                if (wallLeft.transform.position.x > wallTarget.transform.position.x)
                {
                    wallLeft.transform.position = new Vector3(wallLeft.transform.position.x - 0.06f, wallLeft.transform.position.y, wallLeft.transform.position.z);
                }
            }
            else
            {
                wallsDone++;
            }

            if (wallRight.transform.position.z < wallTarget.transform.position.z)
            {
                wallRight.transform.position = new Vector3(wallRight.transform.position.x, wallRight.transform.position.y, wallRight.transform.position.z + 0.06f);

                if (wallRight.transform.position.x < wallTarget.transform.position.x)
                {
                    wallRight.transform.position = new Vector3(wallRight.transform.position.x + 0.06f, wallRight.transform.position.y, wallRight.transform.position.z);
                }
            }
            else
            {
                wallsDone++;
            }

            if (wallsDone == 2) closingWalls = false;
        }


        if (!squishedAngel && wallsDone == 2)
        {
            if (resetTimer > 0)
            {
                resetTimer -= Time.deltaTime;
                return;
            }

            wallsReset = 0;
            trapTrigger.SetActive(false);
            Debug.Log("Opening");
            if (wallLeft.transform.position.z < wallLeftStart.transform.position.z)
            {
                Debug.Log("Left Wall moving");
                wallLeft.transform.position = new Vector3(wallLeft.transform.position.x, wallLeft.transform.position.y, wallLeft.transform.position.z + 0.06f);
                if (wallLeft.transform.position.x < wallLeftStart.transform.position.x)
                {
                    wallLeft.transform.position = new Vector3(wallLeft.transform.position.x + 0.06f, wallLeft.transform.position.y, wallLeft.transform.position.z);
                }
            }
            else
            {
                wallsReset++;
            }

            if (wallRight.transform.position.z > wallRightStart.transform.position.z)
            {
                Debug.Log("Right Wall moving");
                wallRight.transform.position = new Vector3(wallRight.transform.position.x, wallRight.transform.position.y, wallRight.transform.position.z - 0.06f);

                if (wallRight.transform.position.x > wallRightStart.transform.position.x)
                {
                    wallRight.transform.position = new Vector3(wallRight.transform.position.x - 0.06f, wallRight.transform.position.y, wallRight.transform.position.z);
                }
            }
            else
            {
                wallsReset++;
            }

            if (wallsReset == 2)
            {
                wallsDone = 0;
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsIn = true;
            player = other.GetComponent<PlayerControllerScript>();
            player.intString = "Press E to activate trap";
            player.canInteract = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsIn = false;
            player.intString = "";
            player.canInteract = false;
            player = null;
        }
    }
}
