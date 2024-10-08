using UnityEngine;

public class Turret : MonoBehaviour
{
    public float detectionRange = 10f;         // Range within which the turret detects the spaceship
    public GameObject projectilePrefab;        // The projectile the turret will shoot
    public Transform firePoint;                // The point from where the turret shoots
    public float fireRate = 1f;                // Time between shots
    public float bulletForce = 10f;            // Force applied to the projectile
    public float rotationOffset = 90f;         // Offset to adjust turret's look direction (in degrees)
    public float rotationSpeed = 5f;           // Speed at which the turret rotates towards the player
    public float rotationThreshold = 5f;       // How close the turret has to be to the target angle to start shooting

    private GameObject player;                 // Reference to the player
    private float fireCooldown = 0f;           // Cooldown tracker

    void Start()
    {
        // Find the player by tag
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        if (player == null) return; // If no player is found, do nothing

        // Calculate the distance between the turret and the player
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        // Check if the player is within range
        if (distanceToPlayer <= detectionRange)
        {
            // Rotate the turret smoothly towards the player
            RotateTowardsPlayer();

            // Shoot at the player only if the turret is facing the player
            if (IsFacingPlayer())
            {
                ShootAtPlayer();
            }
        }
        else if (distanceToPlayer > detectionRange)
        {
            // If the player is out of range, rotate the turret back to its initial position
            RotateToZeroRotation();
        }

        // Reduce the cooldown over time
        if (fireCooldown > 0f)
        {
            fireCooldown -= Time.deltaTime;
        }
    }

    // Rotates the turret smoothly to face the player with an optional offset
    void RotateTowardsPlayer()
    {
        Vector2 direction = player.transform.position - transform.position;
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + rotationOffset;

        // Get the current rotation and target rotation
        Quaternion currentRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, targetAngle));

        // Smoothly rotate towards the target
        transform.rotation = Quaternion.RotateTowards(currentRotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    // Check if the turret is currently facing the player (within a certain threshold)
    bool IsFacingPlayer()
    {
        Vector2 direction = player.transform.position - transform.position;
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + rotationOffset;

        // Compare the current rotation's Z angle with the target angle
        float currentAngle = transform.rotation.eulerAngles.z;
        float angleDifference = Mathf.DeltaAngle(currentAngle, targetAngle);

        // If the angle difference is within the threshold, the turret is considered facing the player
        return Mathf.Abs(angleDifference) < rotationThreshold;
    }

    // Rotate the turret back to zero rotation smoothly
    void RotateToZeroRotation()
    {
        // Create a target rotation that aligns with the planet's rotation but has a Z angle of zero
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, 0)) * Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z);

        // Smoothly rotate towards the target rotation
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    // Shoots at the player
    void ShootAtPlayer()
    {
        if (fireCooldown <= 0f)
        {
            // Instantiate a projectile at the fire point, inheriting its rotation
            GameObject bullet = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

            // Get the Rigidbody2D component from the projectile
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

            // Apply force to the projectile in the direction of firePoint's "up" direction (which aligns with the turret's facing direction)
            if (rb != null)
            {
                rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
            }

            // Reset the cooldown 
            fireCooldown = 1f / fireRate;
        }
    }
}
