using UnityEngine;

public class SunDamage : MonoBehaviour
{
    public float maxDamageRadius = 10f;  // Minimum distance for maximum damage (closest to the sun)
    public float minDamageRadius = 50f;  // Maximum distance for no damage (farthest from the sun)
    public float maxDamagePerSecond = 20f; // Maximum damage dealt per second when close

    private GameObject player;           // Reference to the player
    private HealthSystem healthSystem;   // Reference to the player's HealthSystem

    void Start()
    {
        // Find the player object by tag
        player = GameObject.FindWithTag("Player");

        if (player != null)
        {
            // Get the HealthSystem component from the player
            healthSystem = player.GetComponent<HealthSystem>();
        }
    }

    void Update()
    {
        if (player != null && healthSystem != null)
        {
            // Calculate the distance between the player and the sun
            float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

            // If the player is within the damage radius, apply linear damage
            if (distanceToPlayer < minDamageRadius)
            {
                // Calculate linear factor (1 when close to maxDamageRadius, 0 at minDamageRadius)
                float linearFactor = Mathf.InverseLerp(minDamageRadius, maxDamageRadius, distanceToPlayer);

                // Calculate damage to apply based on how close the player is
                float damage = maxDamagePerSecond * (1f - linearFactor);

                // Apply damage per second (scaled by Time.deltaTime to make it frame-independent)
                healthSystem.TakeDamage(Mathf.RoundToInt(damage * Time.deltaTime));
            }
        }
    }

    // Draw Gizmos to visualize the damage radii in the editor
    private void OnDrawGizmos()
    {
        // Set Gizmo color for the minimum damage radius (no damage beyond this point)
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, minDamageRadius);

        // Set Gizmo color for the maximum damage radius (max damage near the sun)
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, maxDamageRadius);
    }
}
