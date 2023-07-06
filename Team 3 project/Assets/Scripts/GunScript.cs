using UnityEngine;

public class GunScript : MonoBehaviour
{
    
    public PlayerControls controls;

    public Camera playerCam;

    public LayerMask layerMask;



    private void Awake()
    {
        controls = InputManager.inputActions;
        playerCam = GameObject.FindWithTag("Player").GetComponentInChildren<Camera>();
    }

    private void Update()
    {
        if (controls.Player.Fire.triggered && !PauseMenuScript.isPaused)
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hit, 500, layerMask))
        {
            Debug.Log("Shot: " + hit.transform.tag);
            Debug.DrawRay(playerCam.transform.position, playerCam.transform.forward, Color.green);
            if (hit.transform.tag == "Boss")
            {
                hit.transform.SendMessage("BossHitByRay");
            }
        }
    }
}
