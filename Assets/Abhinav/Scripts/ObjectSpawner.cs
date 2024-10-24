using System.Collections;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    // Object to be spawned
    public GameObject objectToSpawn;

    // Spawn interval in seconds
    public float spawnInterval = 2f;

    // Reference to the boundaries or specific spawn area (optional)
    public Transform spawnArea;

    // Angle at which to shoot the object
    public float shootAngle = 45f;

    // Speed at which to shoot the object
    public float shootSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnObjectsOverTime());
    }

    // Coroutine that spawns objects continuously
    IEnumerator SpawnObjectsOverTime()
    {
        while (true)  // Infinite loop to keep spawning objects
        {
            // Define the position to spawn the object
            Vector3 spawnPosition = GetRandomPosition();

            // Instantiate the object at the specified position
            GameObject spawnedObject = Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);

            // Apply a velocity based on the specified angle and speed
            Rigidbody2D rb2D = spawnedObject.GetComponent<Rigidbody2D>();
            if (rb2D != null)
            {
                // Calculate the direction based on the angle
                Vector2 direction = new Vector2(Mathf.Cos(shootAngle * Mathf.Deg2Rad), Mathf.Sin(shootAngle * Mathf.Deg2Rad));

                // Set the velocity of the Rigidbody2D
                rb2D.velocity = direction * shootSpeed;  // Apply velocity
            }

            // Wait for the spawn interval before spawning the next object
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    // Get a random position within the spawn area (optional)
    Vector3 GetRandomPosition()
    {
        if (spawnArea != null)
        {
            Vector3 randomPos = new Vector3(
                Random.Range(spawnArea.position.x - spawnArea.localScale.x / 2, spawnArea.position.x + spawnArea.localScale.x / 2),
                Random.Range(spawnArea.position.y - spawnArea.localScale.y / 2, spawnArea.position.y + spawnArea.localScale.y / 2),
                0);
            return randomPos;
        }

        // Default spawn at the spawner's position if no spawn area is defined
        return transform.position;
    }
}
