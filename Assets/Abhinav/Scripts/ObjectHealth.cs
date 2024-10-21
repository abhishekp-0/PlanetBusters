using System.Collections;
using UnityEngine;

public class ObjectHealth : MonoBehaviour
{
    // Public health variable
    public float health = 100f;

    // String variable to set the tag in the inspector
    [Tooltip("Tag of objects that will reduce health on trigger")]
    public string damagingTag;

    // Damage value to take when hit by an object with the specified tag
    [Tooltip("Amount of damage to take from the object")]
    public float damageAmount = 10f;
    public float crashAmount = 90f;

    // Color to flash when hit
    [Tooltip("Color to change to when hit")]
    public Color hitColor = Color.red;

    // Duration of the hit effect
    [Tooltip("Duration for hit effect color")]
    public float hitEffectDuration = 0.05f;  // Reduce for faster feedback

    // Reference to the SpriteRenderer component
    private SpriteRenderer spriteRenderer;

    // Original color to reset after the hit effect
    private Color originalColor;

    // Particle effect to spawn upon destruction
    [Tooltip("Particle effect to spawn when this object is destroyed")]
    public GameObject destructionEffect;

    public GameObject destroy;
    public GameObject destroy2;

    private void Start()
    {
        // Get the SpriteRenderer component
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Store the original color of the sprite
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }

        if(damagingTag != null)
        {
            damagingTag = "bulletNew";
        }
    }

    // Method to reduce health when triggering with the specified tag (for 2D collision)
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object has the specified tag
        if (other.CompareTag(damagingTag))
        {
            // Take damage based on the specified damageAmount
            TakeDamage(damageAmount);

            // Destroy the bullet instantly after collision (optional)
            Destroy(other.gameObject);
        }
        if(other.tag == "Player")
        {
            TakeDamage(crashAmount);

        }
    }

    // Function to reduce health and handle color change effect
    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log("Health: " + health);

        // Trigger the hit effect color change
        if (spriteRenderer != null)
        {
            StopAllCoroutines();  // Stop any previous hit effects in progress
            StartCoroutine(HitEffect());
        }

        // If health drops below or equal to 0, destroy the object and spawn particle effect
        if (health <= 0)
        {
            // Spawn the destruction particle effect, if any
            if (destructionEffect != null)
            {
                Instantiate(destructionEffect, transform.position, Quaternion.identity);
            }
            if(destroy == null)
            {
                Destroy(gameObject);

            }
            else if (destroy != null)
            {
                Destroy(destroy);
                Destroy(destroy2);
            }
            Debug.Log("Player is dead!");
        }
    }

    // Coroutine to change the sprite color for a short time
    private IEnumerator HitEffect()
    {
        // Change the color to the hitColor immediately
        spriteRenderer.color = hitColor;

        // Wait for the duration of the hit effect
        yield return new WaitForSeconds(hitEffectDuration);

        // Revert the color back to the original color (usually white)
        spriteRenderer.color = originalColor;
    }
}
