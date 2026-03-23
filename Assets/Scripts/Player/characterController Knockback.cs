using UnityEngine;

public class characterControllerKnockback : MonoBehaviour
{

    /*
     {
    public float knockbackDecay = 5f; // How quickly knockback fades
    public float gravity = -9.81f;    // Gravity for vertical motion

    private CharacterController controller;
    private Vector3 moveDirection;    // Normal movement
    private Vector3 knockbackVelocity; // Knockback movement

    private float verticalVelocity;   // For gravity handling

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Apply gravity
        if (controller.isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f; // Small downward force to keep grounded
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
        }

        // Decay knockback over time
        knockbackVelocity = Vector3.Lerp(knockbackVelocity, Vector3.zero, knockbackDecay * Time.deltaTime);

        // Combine movement
        Vector3 finalMove = moveDirection + knockbackVelocity;
        finalMove.y = verticalVelocity;

        controller.Move(finalMove * Time.deltaTime);
    }

    /// <summary>
    /// Call this to apply knockback to the player.
    /// </summary>
    /// <param name="direction">Direction of knockback (normalized)</param>
    /// <param name="force">Strength of knockback</param>
    public void ApplyKnockback(Vector3 direction, float force)
    {
        if (direction.sqrMagnitude > 0.01f)
        {
            knockbackVelocity = direction.normalized * force;
        }
    }

    // Example: Simulate knockback when pressing K
    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            // Knockback backwards and slightly upward
            ApplyKnockback(-transform.forward + Vector3.up * 0.5f, 5f);
        }
    }
    */
}
