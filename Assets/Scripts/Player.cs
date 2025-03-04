using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [HideInInspector]
    public Rigidbody rb;

    [HideInInspector]
    public Transform cameraTransform;

    [Header("Physics Settings")]
    public float playerHeight = 2f; // de
    public LayerMask whatIsGround; // Set to whatIsGround Layer in Inspector
    public float groundDrag; // used instead of friction, can be tweaked for more responsive input

    [Header("PlayerState")]
    public bool isGrounded = true;
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



    private void ApplyCommonPhysics()
    {
        if (isGrounded)
        {
            // manual friction instead of physics material
            rb.linearDamping = groundDrag;
            readyToDash = true;
        }
        else
        {
            rb.linearDamping = 0;
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
