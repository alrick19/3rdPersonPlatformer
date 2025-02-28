using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] private InputHandler inputHandler;
    [SerializeField] private float gravityScale = 1f;
    public float jumpForce = 10f;
    private Rigidbody rb;
    private int jumpCount = 0;
    public int maxJumps = 2;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        inputHandler = GetComponent<InputHandler>();   
    }
    private void Start()
    {
        inputHandler.OnSpacePressed.AddListener(Jump);
    } 

    private void Jump()
    {
        if (jumpCount < maxJumps)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpCount++;
        }
    }

    private void FixedUpdate()
    {
        if (jumpCount > 0)
        {
            rb.AddForce(Physics.gravity * gravityScale, ForceMode.Acceleration);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpCount = 0;
        }      
    }
}
