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

    private PlayerControls controls;

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


    void Awake()
    {
        controls = new PlayerControls();
        controller = GetComponent<CharacterController>();
        coyoteTimer = coyoteTimerStart;
        moveSpeed = baseMoveSpeed;
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
