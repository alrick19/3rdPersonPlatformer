using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    [SerializeField] protected float dashForce = 10f;
    [SerializeField] private float dashCooldown = 1f;
    [SerializeField] private InputHandler inputHandler;
    private Rigidbody rb;
    private bool canDash = true;
    private Transform cameraTransform;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        inputHandler = GetComponent<InputHandler>();
    }
    void Start()
    {
        cameraTransform = Camera.main.transform;

        inputHandler.OnDash.AddListener(Dash);
    }

    private void Dash()
    {
        if(!canDash) return;

        rb.linearVelocity = Vector3.zero;

        Vector3 forward = cameraTransform.forward;
        forward.y = 0; 
        forward.Normalize(); 

        rb.AddForce(forward * dashForce, ForceMode.Impulse);
        canDash = false;
        Invoke(nameof(ResetDash), dashCooldown);
    }

    private void ResetDash()
    {
        canDash = true;
    }
}
