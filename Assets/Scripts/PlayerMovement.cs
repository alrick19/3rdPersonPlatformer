using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private InputHandler input;

    public float moveSpeed = 1.5f;
    public float airMultiplier = 0.4f;

    void Start()
    {
        input.OnMove.AddListener(Move);
    }

    void Update()
    {
        LimitSpeed();
    }

    private void Move(Vector2 input)
    {
        // Get forward/right relative to the camera
        Vector3 forward = player.cameraTransform.forward;
        Vector3 right = player.cameraTransform.right;
        forward.y = right.y = 0;
        forward.Normalize();
        right.Normalize();

        Vector3 moveDirection = (forward * input.y + right * input.x).normalized;

        // Use different multipliers if in air vs. on ground
        float multiplier = player.isGrounded ? 1f : airMultiplier;
        player.rb.AddForce(moveDirection * moveSpeed * 10f * multiplier, ForceMode.Force);
    }

    private void LimitSpeed()
    {
        // Get horizontal velocity (ignore vertical component)
        Vector3 horizontalVelocity = new Vector3(player.rb.linearVelocity.x, 0, player.rb.linearVelocity.z);
        if (horizontalVelocity.magnitude > moveSpeed)
        {
            // Clamp horizontal velocity to moveSpeed while preserving vertical velocity.
            Vector3 limitedHorizontal = horizontalVelocity.normalized * moveSpeed;
            player.rb.linearVelocity = new Vector3(limitedHorizontal.x, player.rb.linearVelocity.y, limitedHorizontal.z);
        }
    }

    private void SpeedControl()
    {
        Vector3 flatVelocity = new Vector3(player.rb.linearVelocity.x, 0f,
            player.rb.linearVelocity.z);

        if (flatVelocity.magnitude > moveSpeed * 10f)
        {
            Vector3 limitedVelocity = flatVelocity.normalized * moveSpeed;

            // Calculate necessary force to adjust speed smoothly
            Vector3 forceToApply = (limitedVelocity - flatVelocity) * player.rb.mass / Time.fixedDeltaTime;

            // Apply damping force instead of overriding velocity
            player.rb.AddForce(forceToApply, ForceMode.Force);
        }
    }

    private void SpeedControlV1()
    {
        Vector3 flatVelocity = new Vector3(player.rb.linearVelocity.x, 0f, player.rb.linearVelocity.z);

        if (flatVelocity.magnitude >= moveSpeed)
        {
            Vector3 limitedVelocity = flatVelocity.normalized * moveSpeed;
            player.rb.linearVelocity = new Vector3(limitedVelocity.x, player.rb.linearVelocity.y, limitedVelocity.z);
        }
    }

    // [SerializeField] private InputHandler inputHandler;

    // [SerializeField] public float moveSpeed = 1.5f;

    // public float groundDrag;


    // [Header("Ground Check")]
    // public float playerHeight;
    // public LayerMask whatIsGround;
    // bool isGrounded;


    // [Header("Jump")]
    // public float jumpForce = 1f;
    // public float airMultiplier = 0.4f;
    // public float gravityScale = 2f;
    // bool readyToJump = true;

    // [Header("Dash")]
    // public float dashForce = 1f;
    // bool readyToDash = true;

    // private Rigidbody rb;
    // private Transform cameraTransform;

    // void Awake()
    // {
    //     inputHandler = GetComponent<InputHandler>();
    //     rb = GetComponent<Rigidbody>();
    // }

    // void Start()
    // {

    //     cameraTransform = Camera.main.transform;

    //     inputHandler.OnMove.AddListener(Move);
    //     inputHandler.OnSpacePressed.AddListener(Jump);
    //     inputHandler.OnDash.AddListener(Dash);
    // }

    // void Update()
    // {
    //     isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

    //     SpeedControl();
    //     ScaleGravity();

    //     // handle drag 
    //     if (isGrounded)
    //     {
    //         rb.linearDamping = groundDrag;
    //         readyToJump = true;
    //         readyToDash = true;
    //     }
    //     else
    //         rb.linearDamping = 0;
    //     readyToJump = false;
    //     readyToDash = false;
    // }

    // private void Move(Vector2 input)
    // {
    //     Vector3 forward = cameraTransform.forward;
    //     Vector3 right = cameraTransform.right;
    //     forward.y = 0; right.y = 0;
    //     forward.Normalize(); right.Normalize();

    //     Vector3 moveDirection = (forward * input.y + right * input.x).normalized;
    //     if (isGrounded)
    //         rb.AddForce(moveDirection * moveSpeed * 10f, ForceMode.Force);
    //     else if (!isGrounded)
    //         rb.AddForce(moveDirection * moveSpeed * 10f * airMultiplier, ForceMode.Force);

    // }

    // /// <summary>
    // /// 
    // /// </summary>
    // private void SpeedControl()
    // {
    //     Vector3 flatVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

    //     if (flatVelocity.magnitude > moveSpeed)
    //     {
    //         Vector3 limitedVelocity = flatVelocity.normalized * moveSpeed;
    //         rb.linearVelocity = new Vector3(limitedVelocity.x, rb.linearVelocity.y, limitedVelocity.z);
    //     }
    // }

    // private void Jump()
    // {
    //     if (!readyToJump && !isGrounded) return;
    //     readyToJump = false;

    //     // reset vertical velocity 
    //     rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

    //     rb.AddForce(transform.up * jumpForce * 10f, ForceMode.Impulse);
    // }

    // private void Dash()
    // {
    //     if (isGrounded) return;
    //     Vector3 dashDirection = cameraTransform.forward;
    //     dashDirection.y = 0;
    //     dashDirection.Normalize();

    //     rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

    //     rb.AddForce(dashDirection * dashForce * 10f, ForceMode.Impulse);


    // }

    // private void ScaleGravity()
    // {
    //     if (!isGrounded && rb.linearVelocity.y < 0)
    //     {
    //         rb.AddForce(Vector3.down * gravityScale, ForceMode.Acceleration);
    //     }
    // }
}
