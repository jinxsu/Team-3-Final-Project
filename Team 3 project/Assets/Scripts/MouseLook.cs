using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class MouseLook : MonoBehaviour
{

    private PlayerControls controls;

    [SerializeField]
    private float mouseSens = 100f;

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
        mouseLook = controls.Player.Look.ReadValue<Vector2>();

        float mouseX = mouseLook.x * mouseSens * Time.deltaTime * invertValX;
        float mouseY = mouseLook.y * mouseSens * Time.deltaTime * invertValY;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        playerBody.Rotate(Vector3.up * mouseX);

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
