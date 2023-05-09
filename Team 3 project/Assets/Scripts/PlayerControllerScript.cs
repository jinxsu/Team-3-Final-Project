using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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

    private CharacterController controller;

    public Transform ground;

    public float distanceToGround = 0.4f;

    public LayerMask groundMask;

    private bool isGrounded;
    // Start is called before the first frame update
    void Awake()
    {
        controls = new PlayerControls();
        controller = GetComponent<CharacterController>();
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
        Grav();
        PlayerMovement();
        Jump();
    }

    private void Jump()
    {
        if (controls.Player.Jump.triggered)
        {
            velocity.y = MathF.Sqrt(jumpHeight * -2f * gravity);

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

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
