using System.Collections;
using UnityEngine;

public class RadiusOfInfluence : MonoBehaviour
{
    // Radius within which the object will be attracted to the player
    public float attractionRadius = 5f;

    // Initial speed when the object starts moving towards the player
    public float initialSpeed = 1f;

    // Maximum speed that the object will reach as it accelerates
    public float maxSpeed = 5f;

    // How fast the object accelerates
    public float accelerationRate = 2f;

    // Reference to the player object
    private Transform player;

    // Flag to check if the object is currently being attracted
    private bool isAttracting = false;

    // Current speed of the object (starts at initialSpeed and increases)
    private float currentSpeed;

    // Start is called before the first frame update
    void Start()
    {
        // Find the player object by tag
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Initialize the current speed to the initial speed
        currentSpeed = initialSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the player is within the attraction radius
        if (!isAttracting && Vector3.Distance(transform.position, player.position) <= attractionRadius)
        {
            isAttracting = true; // Start attracting the object
        }

        // If the object is attracted to the player, move towards the player
        if (isAttracting)
        {
            AttractToPlayer();
        }
    }

    // Method to move the object towards the player with acceleration
    private void AttractToPlayer()
    {
        // Accelerate the object towards the maximum speed over time
        currentSpeed = Mathf.Min(currentSpeed + accelerationRate * Time.deltaTime, maxSpeed);

        // Move the object towards the player's position using the current speed
        float step = currentSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, player.position, step);
    }

    // Trigger detection to destroy the object on collision with the player
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the object collides with the player
        if (collision.CompareTag("Player"))
        {
            // Destroy the object when it reaches the player
            Destroy(gameObject);
        }
    }

    // Optional: To visualize the attraction radius in the editor
    private void OnDrawGizmosSelected()
    {
        // Draw a wire sphere around the object to show the attraction radius
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attractionRadius);
    }
}
