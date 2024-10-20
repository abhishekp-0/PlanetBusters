using UnityEngine;

public class PlayerMovementClamp2D : MonoBehaviour
{
    // Define the movement limits
    public float minX = -5f;
    public float maxX = 5f;
    public float minY = -3f;
    public float maxY = 3f;

    // Update is called once per frame
    void Update()
    {
        // Get the player's current position
        Vector3 position = transform.position;

        // Clamp the position within the specified limits
        position.x = Mathf.Clamp(position.x, minX, maxX);
        position.y = Mathf.Clamp(position.y, minY, maxY);

        // Apply the clamped position back to the player
        transform.position = position;
    }
}
