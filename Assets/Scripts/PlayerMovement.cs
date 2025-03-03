using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private InputHandler input;

    public float moveSpeed = 1.5f;
    public float airMultiplier = 0.4f; // tweaks air responsiveness

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
        // Get horizontal flat velocity
        Vector3 horizontalVelocity = new Vector3(player.rb.linearVelocity.x, 0, player.rb.linearVelocity.z);
        if (horizontalVelocity.magnitude > moveSpeed)
        {
            // Clamp horizontal velocity to moveSpeed while preserving vertical velocity.
            Vector3 limitedHorizontal = horizontalVelocity.normalized * moveSpeed;
            player.rb.linearVelocity = new Vector3(limitedHorizontal.x, player.rb.linearVelocity.y, limitedHorizontal.z);
        }
    }
}
