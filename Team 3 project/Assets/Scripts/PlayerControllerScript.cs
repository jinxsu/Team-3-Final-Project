using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public class PlayerControllerScript : MonoBehaviour
{

    private PlayerControls controls;

    [SerializeField] 
    private float moveSpeed = 6f;

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

    void Awake()
    {
        controls = new PlayerControls();
        controller = GetComponent<CharacterController>();
        coyoteTimer = coyoteTimerStart;
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
