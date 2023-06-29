using System;
using UnityEngine;

public class PlayerControllerScript : MonoBehaviour
{
    public PlayerControls controls;

    [Header("Player Movement")]

    [SerializeField]
    private float baseMoveSpeed = 6f;

    [SerializeField]
    private float crouchSpeed = 3f;

    private float moveSpeed;

    private Vector3 velocity;

    [SerializeField]
    private float gravity = -9.81f;

    private Vector2 move;

    [Header("Jumping")]

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

    [Header("Crouching")]

    [SerializeField]
    private GameObject POV;

    [SerializeField]
    private float camSpeed = 5f;

    [SerializeField]
    private Transform standCamHeight;

    [SerializeField]
    private Transform crouchCamHeight;

    private Vector3 standCenter = new Vector3(0f, 1.15f, 0.15f);
    private Vector3 crouchCenter = new Vector3(0f, 0.75f, 0.15f);

    private float standPlayerHeight = 2.2f;
    private float crouchPlayerHeight = 1.5f;

    [Header("Arm Movement")]

    [SerializeField]
    private GameObject[] heldObject;

    private int activeObject = 0;

    private bool swapping;
    private bool goingUp;
    private bool armWentDown;

    [SerializeField]
    private GameObject holdPoint;

    [SerializeField]
    private Transform[] armPositions;
    private Transform armTarget;

    [SerializeField]
    private float armSpeed = 5f;

    [Header("Animation")]

    [SerializeField]
    private Animator anim;

    [SerializeField]
    private Animator fullbodyAnim;

    private int maxHp = 4;

    private int currentHp;

    void Awake()
    {
        controls = InputManager.inputActions;
        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
        coyoteTimer = coyoteTimerStart;
        moveSpeed = baseMoveSpeed;
        currentHp = maxHp;
    }

    public void HurtPlayer()
    {
        currentHp--;
        Debug.Log("PlayerHurt.wav");
    }

    private void Start()
    {
        Instantiate(heldObject[0], holdPoint.transform, false);
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
        if (!PauseMenuScript.isPaused)
        {
            Jump();
            Grav();
            Crouch();
            Interact();
            PlayerMovement();
            WeaponSwap();
            MoveArm();

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
    }

    private void WeaponSwap()
    {
        //This function is how the player swaps between items in their inventory.
        // It is built so that if we need to add items, or we chose to remove them, this script doesn't have to change
        float scrollVar = controls.Player.WeaponScroll.ReadValue<Vector2>().y;
        if (scrollVar > 0f)
        {
            goingUp = true;
            swapping = true;
        }
        if (scrollVar < 0f || controls.Player.WeaponNext.triggered)
        {
            goingUp = false;
            swapping = true;
        }
    }

    private void MoveArm()
    {
        if (swapping)
        {
            if (armWentDown)
            {
                armTarget = armPositions[0];

                if (holdPoint.transform.position == armTarget.position)
                {
                    swapping = false;
                    armWentDown = false;
                }
            }
            else
            {
                armTarget = armPositions[1];

                if (holdPoint.transform.position == armTarget.position)
                {
                    ChangeWeapon();
                    armWentDown = true;
                }
            }

            Vector3 newPos = Vector3.MoveTowards(holdPoint.transform.position, armTarget.position, armSpeed * Time.deltaTime);
            holdPoint.transform.position = newPos;
        }
    }



    public void ChangeWeapon()
    {
        if (goingUp)
        {
            if (activeObject - 1 < 0)
            {
                activeObject = heldObject.Length - 1;
            }
            else
            {
                activeObject -= 1;
            }
        }
        else
        {
            if (activeObject + 1 == heldObject.Length)
            {
                activeObject = 0;
            }
            else
            {
                activeObject += 1;
            }
        }
        Destroy(GameObject.FindWithTag("HeldItem"));
        Instantiate(heldObject[activeObject], holdPoint.transform, false);
    }

    private void Crouch()
    {
        if (controls.Player.Crouch.IsPressed())
        {
            moveSpeed = crouchSpeed;
            controller.center = crouchCenter;
            controller.height = crouchPlayerHeight;

            anim.SetBool("crouching", true);
            fullbodyAnim.SetBool("crouching", true);

            if (POV.transform.position != crouchCamHeight.position)
            {
                Vector3 newPos = Vector3.MoveTowards(POV.transform.position, crouchCamHeight.position, camSpeed * Time.deltaTime);
                POV.transform.position = newPos;
            }

        }
        else
        {
            moveSpeed = baseMoveSpeed;
            controller.center = standCenter;
            controller.height = standPlayerHeight;

            anim.SetBool("crouching", false);
            fullbodyAnim.SetBool("crouching", false);

            if (POV.transform.position != standCamHeight.position)
            {
                Vector3 newPos = Vector3.MoveTowards(POV.transform.position, standCamHeight.position, camSpeed * Time.deltaTime);
                POV.transform.position = newPos;
            }
        }
    }

    private void Interact()
    {
        if (controls.Player.Interact.triggered)
        {
            //interact with item code here
            anim.SetTrigger("interact");
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
            fullbodyAnim.SetTrigger("isJumping");
        }
    }


    private void PlayerMovement()
    {
        move = controls.Player.Move.ReadValue<Vector2>();

        Vector3 movement = (move.y * transform.forward) + (move.x * transform.right);
        controller.Move(moveSpeed * Time.deltaTime * movement);

        anim.SetFloat("velocity", move.magnitude);
        fullbodyAnim.SetFloat("velocity", move.magnitude);
    }

    private void Grav()
    {
        isGrounded = Physics.CheckSphere(ground.position, distanceToGround, groundMask);

        if (isGrounded)
        {
            if (!recentJump)
            {
                coyoteTimer = coyoteTimerStart;

                fullbodyAnim.SetBool("isGrounded", true);
                fullbodyAnim.SetBool("isFalling", false);
            }

        }
        else
        {
            if (coyoteTimer > 0) coyoteTimer -= Time.deltaTime;

            if (recentJump || velocity.y < -2)
            {
                fullbodyAnim.SetBool("isFalling", true);
                fullbodyAnim.SetBool("isGrounded", false);
            }
        }

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
