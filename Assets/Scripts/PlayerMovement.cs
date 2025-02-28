using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private InputHandler inputHandler;
    [SerializeField] public float moveSpeed = 1.5f;
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

        
    }

    private void Move(Vector2 input)
    {
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;
        forward.y = 0; right.y = 0;
        forward.Normalize(); right.Normalize();

        Vector3 moveDirection = (forward * input.y  + right * input.x).normalized;
        rb.AddForce(moveDirection * moveSpeed, ForceMode.Acceleration);
    }
}
