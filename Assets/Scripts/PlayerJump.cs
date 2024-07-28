using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public float jumpForceMultiplier = 0.1f; // Adjust the jump force multiplier
    public float dragThreshold = 1f; // Minimum drag distance to trigger a jump
    public float maxDragDistance = 100f; // Maximum drag distance
    public LineRenderer lineRenderer; // Reference to the LineRenderer
    public int trajectoryResolution = 30; // Number of points in the trajectory line

    private Rigidbody rb;
    private Vector2 dragStartPosition;
    private bool isDragging = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        lineRenderer.positionCount = trajectoryResolution;
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    dragStartPosition = touch.position;
                    isDragging = true;
                    break;

                case TouchPhase.Moved:
                    if (isDragging)
                    {
                        Vector2 dragCurrentPosition = touch.position;
                        Vector2 dragVector = dragStartPosition - dragCurrentPosition;

                        // Limit the drag distance
                        if (dragVector.magnitude > maxDragDistance)
                        {
                            dragVector = dragVector.normalized * maxDragDistance;
                        }

                        if (dragVector.magnitude > dragThreshold)
                        {
                            Vector3 jumpDirection = new Vector3(dragVector.x, dragVector.y, dragVector.y).normalized;
                            float jumpForce = dragVector.magnitude * jumpForceMultiplier;
                            ShowTrajectory(transform.position, jumpDirection * jumpForce);
                        }
                    }
                    break;

                case TouchPhase.Ended:
                    if (isDragging)
                    {
                        Vector2 dragEndPosition = touch.position;
                        Vector2 dragVector = dragStartPosition - dragEndPosition;

                        // Limit the drag distance
                        if (dragVector.magnitude > maxDragDistance)
                        {
                            dragVector = dragVector.normalized * maxDragDistance;
                        }

                        if (dragVector.magnitude > dragThreshold)
                        {
                            Vector3 jumpDirection = new Vector3(dragVector.x, dragVector.y, dragVector.y).normalized;
                            float jumpForce = dragVector.magnitude * jumpForceMultiplier;
                            rb.AddForce(jumpDirection * jumpForce, ForceMode.Impulse);
                        }

                        lineRenderer.positionCount = 0; // Hide the trajectory line
                        isDragging = false;
                    }
                    break;
            }
        }
    }

    void ShowTrajectory(Vector3 start, Vector3 velocity)
    {
        Vector3[] points = new Vector3[trajectoryResolution];
        float timeStep = 0.1f;
        for (int i = 0; i < trajectoryResolution; i++)
        {
            float time = i * timeStep;
            points[i] = start + velocity * time + 0.5f * Physics.gravity * time * time;
        }
        lineRenderer.positionCount = points.Length;
        lineRenderer.SetPositions(points);
    }
}