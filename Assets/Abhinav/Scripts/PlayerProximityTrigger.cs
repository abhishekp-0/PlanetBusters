using System.Collections; // Ensure this is included
using UnityEngine;

public class PlayerProximityTrigger : MonoBehaviour
{
    // Distance at which the script will be enabled
    public float triggerDistance = 5f;

    // The script component to enable on the target object
    public MonoBehaviour scriptToEnable;

    // Reference to the player's transform
    private Transform playerTransform;

    // The GameObject to scale (this should be assigned in the Inspector)
    public GameObject targetObject;

    // Original scale of the target object
    private Vector3 originalScale;

    // Scale speed (how fast the object scales to its original size)
    public float scaleSpeed = 1f;

    // Flag to check if scaling is in progress
    private bool isScaling = false;

    // Start is called before the first frame update
    void Start()
    {
        // Find the player object (make sure the player has a tag "Player")
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }

        // Store the original scale of the target object
        if (targetObject != null)
        {
            originalScale = targetObject.transform.localScale;
            // Set the initial scale of the target object to zero
            targetObject.transform.localScale = Vector3.zero;
        }
        else
        {
            Debug.LogWarning("Target object is not assigned!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform != null && targetObject != null)
        {
            // Check the distance to the player
            float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
            if (distanceToPlayer <= triggerDistance)
            {
                // Start scaling if not already scaling
                if (!isScaling)
                {
                    isScaling = true;
                    StartCoroutine(ScaleToOriginalSize());
                }

                // Enable the specified script if it's not already enabled
                if (scriptToEnable != null && !scriptToEnable.enabled)
                {
                    scriptToEnable.enabled = true;
                }
            }
            else
            {
                // Optionally disable the script when the player is out of range
                if (scriptToEnable != null && scriptToEnable.enabled)
                {
                    scriptToEnable.enabled = false;
                }
            }
        }
    }

    // Coroutine to scale the target object to its original size
    private IEnumerator ScaleToOriginalSize()
    {
        while (targetObject.transform.localScale.x < originalScale.x)
        {
            // Gradually increase the size of the target object
            targetObject.transform.localScale = Vector3.Lerp(targetObject.transform.localScale, originalScale, scaleSpeed * Time.deltaTime);
            yield return null; // Wait for the next frame
        }

        // Ensure the target object reaches its original scale
        targetObject.transform.localScale = originalScale;
    }

    // Draw gizmos in the editor to visualize the trigger distance
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green; // Set the color for the gizmo
        Gizmos.DrawWireSphere(transform.position, triggerDistance); // Draw a wire sphere
    }
}
