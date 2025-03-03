using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Rigidbody rb;
    public Transform cameraTransform;

    [Header("Physics Settings")]
    public float playerHeight;
    public LayerMask whatIsGround;
    public float groundDrag;

    [Header("PlayerState")]
    public bool isGrounded = true;
    public bool readyToJump = true;
    public bool readyToDash = true;

    [SerializeField] private TextMeshProUGUI speedText;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void Start()
    {
        cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        UpdateGroundState();
        ApplyCommonPhysics();
        UpdateSpeedUI();
    }

    private void UpdateGroundState()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Collided with: {other.gameObject.name}, Layer: {LayerMask.LayerToName(other.gameObject.layer)} ({other.gameObject.layer})");

        if ((whatIsGround.value & (1 << other.gameObject.layer)) != 0)
        {
            isGrounded = true;
        }
    }

    private void ApplyCommonPhysics()
    {
        if (isGrounded)
        {
            // manual friction instead of physics material
            rb.linearDamping = groundDrag;
            readyToJump = true;
            readyToDash = true;
        }
        else
        {
            rb.linearDamping = 0;
            readyToJump = false;
            readyToDash = false;
        }
    }

    private void UpdateSpeedUI()
    {
        if (speedText != null)
        {
            Vector3 flatVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
            float flatSpeed = flatVelocity.magnitude;

            speedText.text = "Flat Speed: " + flatSpeed.ToString("F2") + "m/s";
        }
    }
}
