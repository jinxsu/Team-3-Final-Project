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

    // Start is called before the first frame update
    void Start()
    {
        trapTrigger.SetActive(false);
        closingWalls = false;
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
            wallsReset = 0;
            trapTrigger.SetActive(false);
            Debug.Log("Opening");
            if (wallLeft.transform.position.z < wallTarget.transform.position.z)
            {
                Debug.Log("Left Wall moving");
                wallLeft.transform.position = new Vector3(wallLeft.transform.position.x, wallLeft.transform.position.y, wallLeft.transform.position.z + 0.06f);
                if (wallLeft.transform.position.x < wallTarget.transform.position.x)
                {
                    wallLeft.transform.position = new Vector3(wallLeft.transform.position.x + 0.06f, wallLeft.transform.position.y, wallLeft.transform.position.z);
                }
            }
            else
            {
                wallsReset++;
            }

            if (wallRight.transform.position.z > wallTarget.transform.position.z)
            {
                Debug.Log("Right Wall moving");
                wallRight.transform.position = new Vector3(wallRight.transform.position.x, wallRight.transform.position.y, wallRight.transform.position.z - 0.06f);

                if (wallRight.transform.position.x > wallTarget.transform.position.x)
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
        if (other.CompareTag("Player")) playerIsIn = true;
        player = other.GetComponent<PlayerControllerScript>();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) playerIsIn = false;

        player = null;
    }
}
