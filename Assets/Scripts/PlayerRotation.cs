using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    public float rotationSpeed = 100f;

    // Rotate the player to the left
    public void RotateLeft()
    {
        Rotate( new Vector3(0,0,-1));
    }

    // Rotate the player to the right
    public void RotateRight()
    {
        Rotate( new Vector3(0,0,1));
    }

    // General rotation method
    private void Rotate(Vector3 direction)
    {
        transform.Rotate(direction * rotationSpeed * Time.deltaTime);
    }
}
