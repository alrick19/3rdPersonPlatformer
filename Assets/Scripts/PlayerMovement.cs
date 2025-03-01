using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private InputHandler inputHandler;

    [SerializeField] public float moveSpeed = 1.5f;

    public float groundDrag;


    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool isGrounded;


    [Header("Jump")]
    public float jumpForce;
    public float jumpCoolDown;
    public float airMultiplier;
    bool readyToJump = true;

    private Rigidbody rb;
    private Transform cameraTransform;

    void Awake()
    {
        inputHandler = GetComponent<InputHandler>();
        rb = GetComponent<Rigidbody>(); 
    }

    void Start()
    {

        cameraTransform = Camera.main.transform;

        inputHandler.OnMove.AddListener(Move);
        inputHandler.OnSpacePressed.AddListener(Jump);
    }

    void Update()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        SpeedControl();

        // handle drag 
        if (isGrounded)
        {
            rb.linearDamping = groundDrag;
            readyToJump = true;
        }
        else
            rb.linearDamping = 0;
            readyToJump = false;
    }

    private void Move(Vector2 input)
    {
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;
        forward.y = 0; right.y = 0;
        forward.Normalize(); right.Normalize();

        Vector3 moveDirection = (forward * input.y  + right * input.x).normalized;
        if(isGrounded)
            rb.AddForce(moveDirection * moveSpeed * 10f, ForceMode.Force);
        else if (!isGrounded)
            rb.AddForce(moveDirection * moveSpeed * 10f * airMultiplier, ForceMode.Force);

    }

    /// <summary>
    /// 
    /// </summary>
    private void SpeedControl()
    {
        Vector3 flatVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        if (flatVelocity.magnitude > moveSpeed)
        {
            Vector3 limitedVelocity = flatVelocity.normalized * moveSpeed;
            rb.linearVelocity = new Vector3(limitedVelocity.x, rb.linearVelocity.y, limitedVelocity.z);
        }
    }

    private void Jump()
    {
        if(!readyToJump  && !isGrounded) return;
        readyToJump = false;
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        rb.AddForce(transform.up * jumpForce * 10f, ForceMode.Impulse);
    }

}
