using UnityEngine;

public class RotateFish: MonoBehaviour
{
    public float jumpForce = 10f;        // The vertical force applied to the fish to make it jump
    public float jumpInterval = 2f;      // Time interval between jumps
    public float horizontalSpeed = 2f;   // The horizontal speed of the fish during the jump
    private Rigidbody rb;
    private float nextJumpTime;

    void Start()
    {
        // Get the Rigidbody component
        rb = GetComponent<Rigidbody>();

        // Set the initial next jump time
        nextJumpTime = Time.time + jumpInterval;
    }

    void Update()
    {
        // Check if it's time for the next jump
        if (Time.time >= nextJumpTime)
        {
            // Make the fish jump
            Jump();

            // Update the next jump time
            nextJumpTime = Time.time + jumpInterval;
        }
    }

    void Jump()
    {
        // Apply the vertical jump force
        rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);

        // Apply some horizontal movement for a more natural jump
        rb.AddForce(new Vector3(Random.Range(-1f, 1f) * horizontalSpeed, 0, Random.Range(-1f, 1f) * horizontalSpeed), ForceMode.VelocityChange);
    }
}