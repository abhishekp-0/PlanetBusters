using UnityEngine;

public class PlayerKnockback : MonoBehaviour
{
    // Reference to the player's Rigidbody2D
    private Rigidbody2D rb;

    // Knockback force properties
    public float knockbackForce = 10f;    // How strong the knockback is
    public float knockbackDuration = 0.2f; // How long the knockback lasts

    private bool isKnockedBack = false;   // To check if the player is in knockback state
    private float knockbackTimer = 0f;

    void Start()
    {
        // Get the Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();
    }

    // This function is called when the player collides with an object (like an enemy or projectile)
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision object is an enemy or a harmful object
        if (collision.gameObject.CompareTag("Planet"))
        {
            // Apply knockback
            ApplyKnockback(collision.transform.position);
        }
    }

    // This function is called when the player enters a trigger collider
    void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the trigger object is an enemy or a harmful object
        if (collision.CompareTag("asteroid"))
        {
            // Apply knockback
            ApplyKnockback(collision.transform.position);
        }
    }

    void ApplyKnockback(Vector2 hitPosition)
    {
        // Get the direction opposite to the hit point
        Vector2 knockbackDirection = (transform.position - (Vector3)hitPosition).normalized;

        // Apply the knockback force to the player
        rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);

        // Set the knockback timer and flag
        isKnockedBack = true;
        knockbackTimer = knockbackDuration;
    }

    void Update()
    {
        // Handle knockback duration and control
        if (isKnockedBack)
        {
            knockbackTimer -= Time.deltaTime;

            // When the knockback timer ends, reset the knockback state
            if (knockbackTimer <= 0f)
            {
                isKnockedBack = false;
                rb.velocity = Vector2.zero; // Stop any residual velocity after knockback
            }
        }
    }
}
