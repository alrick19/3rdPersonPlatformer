using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private InputHandler input;
    public float dashForce = 1f;

    void Start()
    {
        input.OnDash.AddListener(Dash);
    }

    private void Dash()
    {
        // Only dash when in air, for example
        if (player.isGrounded)
            return;

        // Calculate dash direction based on the camera's forward direction (ignoring vertical)
        Vector3 dashDirection = player.cameraTransform.forward;
        dashDirection.y = 0;
        dashDirection.Normalize();

        // Reset current velocity’s horizontal component if necessary
        Vector3 currentVelocity = player.rb.linearVelocity;
        player.rb.linearVelocity = new Vector3(currentVelocity.x, 0f, currentVelocity.z);

        // Apply the dash force
        player.rb.AddForce(dashDirection * dashForce * 10f, ForceMode.Impulse);
    }
}
