using UnityEditor.Callbacks;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private InputHandler input;

    [Header("Jump Settings")]
    public float jumpForce = 1f;
    public float gravityScale = 2f;
    public float maxJumpHeight = 4f;

    private float jumpStartY;
    private int maxJumps = 2;
    private int jumpsRemaining = 2;

    void Start()
    {
        input.OnSpacePressed.AddListener(Jump);
    }

    void Update()
    {
        JumpFallOff();
        JumpReset();
        HeightControl();
    }

    private void Jump()
    {

        // if (!player.readyToJump || !player.isGrounded)
        // {

        //     return;
        // }
        if (jumpsRemaining > 0)
        {
            jumpsRemaining--;
            jumpStartY = player.transform.position.y;

            player.rb.linearVelocity = new Vector3(player.rb.linearVelocity.x, 0f, player.rb.linearVelocity.z);
            player.rb.AddForce(Vector3.up * jumpForce * 10f, ForceMode.Impulse);
        }


    }

    private void JumpFallOff()
    {
        // Apply extra gravity if falling
        if (!player.isGrounded && player.rb.linearVelocity.y < 0)
        {
            player.rb.AddForce(Vector3.down * gravityScale, ForceMode.Acceleration);
        }
    }
    private void JumpReset()
    {
        if (player.isGrounded)
        {
            jumpsRemaining = maxJumps;
        }
    }

    private void HeightControl()
    {
        if (player.transform.position.y > jumpStartY + maxJumpHeight)
        {
            player.rb.linearVelocity = new Vector3(player.rb.linearVelocity.x, -1f, player.rb.linearVelocity.z);
        }
    }
}
