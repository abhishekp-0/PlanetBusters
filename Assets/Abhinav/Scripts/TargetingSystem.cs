using UnityEngine;

public class TargetingSystem : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // Check for mouse button click (left button)
        if (Input.GetMouseButtonDown(1))
        {
            // Create a ray from the mouse position
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            // Check if we hit something
            if (hit.collider != null && hit.collider.CompareTag("Planet"))
            {
                // Log the name of the GameObject
                Debug.Log("Target locked: " + hit.collider.gameObject.name);

                // Optional: Lock the target (you can store the target if needed)
                // Example: GameObject target = hit.collider.gameObject;
            }
        }
    }
}
