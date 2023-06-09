using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public class PlayerControllerScript : MonoBehaviour
{

    public PlayerControls controls;

    [SerializeField] 
    private float baseMoveSpeed = 6f;
    
    [SerializeField] 
    private float crouchSpeed = 3f;

    private float moveSpeed;

    private Vector3 velocity;

    [SerializeField]
    private float gravity = -9.81f;

    private Vector2 move;

    [SerializeField]
    private float jumpHeight = 2.4f;


    //Coyote timer is used to determine how long a player can have slipped off of a surface and still jump
    [SerializeField]
    private float coyoteTimerStart = 3f;

    private float coyoteTimer;

    private CharacterController controller;

    public Transform ground;

    public float distanceToGround = 0.4f;

    public LayerMask groundMask;

    private bool isGrounded;

    //This is janky. I don't like it, but I can't figure out better way to make coyote time not allow for a double jump
    private bool recentJump;

    private float recentJumpTimer = 0.5f;

    private Vector3 crouchScale = new Vector3(1f, 0.75f, 1f);

    private Vector3 standScale = new Vector3(1f,1f,1f);

    private float crouchCamHeight = 0f;

    private float standCamHeight = 1.567f;

    [SerializeField]
    private GameObject[] heldObject;

    private int activeObject = 0;


    void Awake()
    {
        controls = InputManager.inputActions;
        controller = GetComponent<CharacterController>();
        coyoteTimer = coyoteTimerStart;
        moveSpeed = baseMoveSpeed;
        
    }

    private void Start()
    {
        Instantiate(heldObject[0],transform.GetChild(0).transform.GetChild(0),false);
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
        Grav();
        Crouch();
        PlayerMovement();
        WeaponSwap();

        if (recentJump)
        {
            recentJumpTimer -= Time.deltaTime;

            if (recentJumpTimer < 0)
            {
                recentJumpTimer = 0.5f;
                recentJump = false;
            }
        }
        
        
    }

    private void WeaponSwap()
    {
        //This function is how the player swaps between items in their inventory.
        // It is built so that if we need to add items, or we chose to remove them, this script doesn't have to change
        float scrollVar = controls.Player.WeaponScroll.ReadValue<Vector2>().y;
        if (scrollVar > 0f)
        {
            
            if (activeObject -1 < 0)
            {
                activeObject = heldObject.Length - 1;
            }
            else
            {
                activeObject -= 1;
            }
            Destroy(transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).gameObject);
            Instantiate(heldObject[activeObject], transform.GetChild(0).transform.GetChild(0), false);
        }
        if (scrollVar < 0f || controls.Player.WeaponNext.triggered)
        {
            
            if (activeObject + 1 == heldObject.Length)
            {
                activeObject = 0;
            }
            else
            {
                activeObject += 1;
            }
            Destroy(transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).gameObject);
            Instantiate(heldObject[activeObject], transform.GetChild(0).transform.GetChild(0), false);
        }
    }

    private void Crouch()
    {
        if (controls.Player.Crouch.IsPressed())
        {
            moveSpeed = crouchSpeed;
            transform.localScale = crouchScale;
        }
        else
        {
            moveSpeed = baseMoveSpeed;
            transform.localScale = standScale;
        }
    }

    //Basic movement functions

    private void Jump()
    {
        //Jumping has "coyote time" as a quality of life thing. If the player presses the space bar just too late, they still jump
        if (controls.Player.Jump.triggered && (isGrounded || coyoteTimer > 0))
        {
            velocity.y = MathF.Sqrt(jumpHeight * -2f * gravity);
            coyoteTimer = 0f;
            recentJump = true;
        }
    }

    
    private void PlayerMovement()
    {
        move = controls.Player.Move.ReadValue<Vector2>();

        Vector3 movement = (move.y * transform.forward) + (move.x * transform.right);
        controller.Move(movement * moveSpeed * Time.deltaTime);
    }

    private void Grav()
    {
        isGrounded = Physics.CheckSphere(ground.position, distanceToGround, groundMask);

        if (isGrounded)
        {
            if (!recentJump)   coyoteTimer = coyoteTimerStart;
        }
        else
        {
            if (coyoteTimer > 0)    coyoteTimer -= Time.deltaTime;
        }

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
