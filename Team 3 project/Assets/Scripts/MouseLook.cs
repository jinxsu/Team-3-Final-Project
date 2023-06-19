using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class MouseLook : MonoBehaviour
{

    private PlayerControls controls;

    
    public float mouseSens;

    public float controllerSens;

    private Vector2 mouseLook;

    private float xRotation = 0f;

    private Transform playerBody;

    public int invertValX;

    public int invertValY;

    
    // Start is called before the first frame update

    void Awake()
    {
        
        playerBody = transform.parent;

        controls = new PlayerControls();
        Cursor.lockState = CursorLockMode.Locked;
        mouseSens = (float)PlayerPrefs.GetInt("knmSens",100);
        controllerSens = (float)PlayerPrefs.GetInt("ctrSens", 150);

    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void Look()
    {
        InputAction playerControllerInput = controls.Player.Look;
        mouseLook = controls.Player.Look.ReadValue<Vector2>();


        if (playerControllerInput.activeControl != null)
        {
            InputDevice device = playerControllerInput.activeControl.device;

            //When the player is using a keyboard
            if (device is Pointer)
            {
                

                float mouseX = mouseLook.x * mouseSens * Time.deltaTime * invertValX;
                float mouseY = mouseLook.y * mouseSens * Time.deltaTime * invertValY;

                xRotation -= mouseY;
                xRotation = Mathf.Clamp(xRotation, -90f, 90f);

                transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
                playerBody.Rotate(Vector3.up * mouseX);
            }

            //When the player is using a controller
            else if (device is Gamepad)
            {
                float controllerX = mouseLook.x * controllerSens * Time.deltaTime * invertValX;
                float controllerY = mouseLook.y * controllerSens * Time.deltaTime * invertValY;

                xRotation -= controllerY;
                xRotation = Mathf.Clamp(xRotation, -90f, 90f);

                transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
                playerBody.Rotate(Vector3.up * controllerX);
            }
        }
        

        

        

    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

       
        if (InputManager.yInvert)
        {
            invertValY = -1;
        }
        else
        {
            invertValY = 1;
        }
        if (InputManager.xInvert)
        {
            invertValX = -1;
        }
        else
        {
            invertValX = 1;
        }

        Look();
    }
}
