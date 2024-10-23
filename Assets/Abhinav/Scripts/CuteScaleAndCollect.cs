using System.Collections;
using UnityEngine;

public class CuteScaleAndCollect : MonoBehaviour
{
    // Scaling parameters
    public float expandDuration = 0.2f;         // Duration for the scaling up effect (quick growth)
    public float shrinkDuration = 0.2f;          // Duration for the shrinking back effect
    public float additionalScale = 0.2f;         // Additional scale to expand beyond the original scale
    public float destroyDistance = 0.2f;         // Distance to player for collection

    // Attraction speed variables
    public float initialSpeed = 1f;              // Starting slow speed
    public float finalSpeed = 5f;                // Maximum speed
    public float accelerationDuration = 2f;       // Time to reach max speed

    public float waitTimeBeforeMove = 1f;

    // Target scale (original scale)
    private Vector3 originalScale;

    // Internal variables to control speed over time
    private float currentSpeed;
    private bool isAttracting = false;

    // Reference to the player (found by tag)
    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        // Store the original scale of the object
        originalScale = transform.localScale;

        // Set the scale to zero at the start
        transform.localScale = Vector3.zero;

        // Find the player object by tag "Player"
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Start the "cute" scaling effect
        StartCoroutine(CuteScale());
    }

    // Coroutine to handle the scaling effect
    IEnumerator CuteScale()
    {
        // First phase: Expand quickly from 0 to larger than original scale
        float elapsedTime = 0f;
        Vector3 expandedScale = originalScale * (1 + additionalScale);

        while (elapsedTime < expandDuration)
        {
            elapsedTime += Time.deltaTime;

            // Smoothly interpolate the scale from 0 to expanded scale
            transform.localScale = Vector3.Lerp(Vector3.zero, expandedScale, elapsedTime / expandDuration);

            // Wait until the next frame 
            yield return null;
        }

        // Ensure the final scale after expansion is exactly the expanded scale
        transform.localScale = expandedScale;

        // Second phase: Shrink back to the original scale
        elapsedTime = 0f;

        while (elapsedTime < shrinkDuration)
        {
            elapsedTime += Time.deltaTime;

            // Smoothly interpolate the scale from expanded scale back to original
            transform.localScale = Vector3.Lerp(expandedScale, originalScale, elapsedTime / shrinkDuration);

            // Wait until the next frame
            yield return null;
        }

        // Ensure the final scale after shrinking is exactly the original scale
        transform.localScale = originalScale;

        // Wait for 1 second before starting to attract
        yield return new WaitForSeconds(waitTimeBeforeMove);

        // Start attracting the collectible towards the player
        isAttracting = true;
    }

    // Update is called oncsjjhjdnnjve per frame
    void Update()
    {
        if (isAttracting && player != null)
        {
            // Move the object towards the player
            float step = currentSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, player.position, step);

            // Gradually increase the speed towards the final speed
            currentSpeed = Mathf.Lerp(initialSpeed, finalSpeed, Time.time / accelerationDuration);

            // Check if the collectible is close enough to the player to be "collected"
            if (Vector3.Distance(transform.position, player.position) < destroyDistance)
            {
                // Trigger collection effect or particle system here if needed

                // Destroy the collectible once it reaches the player
                Destroy(gameObject);
            }
        }
    }

    // Method to start the attraction when player enters the range
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Ensure the attracting only happens when colliding with the player tagged object
        if (collision.CompareTag("Player"))
        {
            isAttracting = true;
        }
    }
}
