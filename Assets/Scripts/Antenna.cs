using UnityEngine;
using UnityEngine.UI;

public class Antenna : MonoBehaviour
{
    public GameObject asteroidPrefab;       // The asteroid prefab to spawn
    public float minSpawnDistance = 50f;    // Minimum distance from the player to spawn asteroids
    public float maxSpawnDistance = 100f;    // Maximum distance from the player to spawn asteroids
    public float spawnInterval = 2f;        // Time between individual asteroid spawns
    public int numberOfAsteroids = 5;       // Number of asteroids to spawn in one batch

    private GameObject player;               // Reference to the player
    private float spawnTimer;                // Timer to track spawn intervals
    private float batchSpawnTimer;           // Timer to track batch spawn intervals

    public float leftAngle = -45f;          // The leftmost angle (in degrees)
    public float rightAngle = 45f;          // The rightmost angle (in degrees)
    public float swingSpeed = 2f;           // The speed at which the pendulum swings
    public bool isClockwise = true;         // Control initial direction

    private float timer = 0f;               // Timer to track the pendulum motion
    public Transform PendulumAxis;


    void Start()
    {
        // Find the player by tag
        player = GameObject.FindWithTag("Player");
        spawnTimer = spawnInterval;          // Initialize the spawn timer
        batchSpawnTimer = 10f;               // Initialize the batch spawn timer
    }

    void Update()
    {
        Pendulum();

        if (player == null) return; // If no player is found, do nothing

        // Update the individual spawn timer
        spawnTimer -= Time.deltaTime;
        // Update the batch spawn timer
        batchSpawnTimer -= Time.deltaTime;

        // Check if it's time to spawn a new asteroid
        if (spawnTimer <= 0f)
        {
            SpawnAsteroid();
            spawnTimer = spawnInterval; // Reset the timer
        }

        // Check if it's time to spawn multiple asteroids
        if (batchSpawnTimer <= 0f)
        {
            SpawnMultipleAsteroids();
            batchSpawnTimer = 10f; // Reset the batch spawn timer
        }
    }

    void SpawnAsteroid()
    {
        // Generate a random distance between min and max spawn distance
        float randomDistance = Random.Range(minSpawnDistance, maxSpawnDistance);

        // Generate a random direction
        Vector2 randomDirection = Random.insideUnitCircle.normalized;

        // Calculate the spawn position (random distance away from the player in the random direction)
        Vector2 spawnPosition = (Vector2)player.transform.position + randomDirection * randomDistance;

        // Instantiate the asteroid at the spawn position
        GameObject asteroid = Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity);

        // Get the Rigidbody2D component of the asteroid
        Rigidbody2D rb = asteroid.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            // Calculate the direction to the player
            Vector2 directionToPlayer = (Vector2)player.transform.position - spawnPosition;

            // Apply force towards the player's position using ForceMode2D.Impulse
            rb.AddForce(directionToPlayer.normalized * rb.mass * 10f, ForceMode2D.Impulse);
        }
    }

    void SpawnMultipleAsteroids()
    {
        for (int i = 0; i < numberOfAsteroids; i++)
        {
            SpawnAsteroid(); // Call the method to spawn individual asteroids
        }
    }
    void Pendulum()
    {
        timer += Time.deltaTime * swingSpeed;

        // Use a sine wave for smoother motion (between -1 and 1)
        float sineValue = Mathf.Sin(timer);

        // Map sineValue (-1 to 1) to the desired angle range (leftAngle to rightAngle)
        float localAngle = Mathf.Lerp(leftAngle, rightAngle, (sineValue + 1f) / 2f);

        // Apply the new local rotation on the Z axis
        PendulumAxis.localRotation = Quaternion.Euler(new Vector3(0, 0, localAngle));
    }
}
