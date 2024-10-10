using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    public int maxHealth = 100;
    public Slider healthBar; // Reference to the health bar UI
    public bool hasArmor = true; // Armor that can endure one hit

    private int currentHealth;
    private bool[] hitArray = new bool[3]; // Boolean array to track hits
    private int hitIndex = 0; // Index to track which element to mark as true
    [SerializeField]
    private PlayerMovement PlayerMovement;

    public SpriteRenderer sr;


    void Start()
    {
        currentHealth = maxHealth;
        // Initialize all elements in hitArray to false
        for (int i = 0; i < hitArray.Length; i++)
        {
            hitArray[i] = false;
        }
        UpdateHealthBar();

       
    }

    private void Update()
    {
        if(currentHealth <= 0)
        {
            sr.enabled = false;
            PlayerMovement.enabled = false;
        }
    }

    public void TakeDamage(int damage)
    {
        PlayerMovement.OnHit();
        if (hasArmor)
        {
            Debug.Log("Armor absorbed the hit!");
            hasArmor = false; // Armor takes one hit and is lost
        }
        else
        {
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                Die();
            }
            UpdateHealthBar(); // Update the UI
        }
        if (hitIndex < hitArray.Length)
        {
            hitIndex=Random.Range(0, hitArray.Length);
            hitArray[hitIndex] = true; // Set the next element in the array to true
            Debug.Log("Hit recorded in array at index: " + hitIndex);
        }
        Debug.Log("TakeDamage");
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        UpdateHealthBar(); // Update the UI
    }

    void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.value = (float)currentHealth / maxHealth;
        }
    }

    void Die()
    {

        Debug.Log("Player Died!");
        // Add death logic here
    }

    // Optional method to restore armor
    public void RestoreArmor()
    {
        hasArmor = true;
        Debug.Log("Armor restored!");
    }

    // Function to set a specific index in hitArray to false and check if all elements are false
    public void ResetHitAtIndex(int index)
    {
        if (index >= 0 && index < hitArray.Length)
        {
            hitArray[index] = false; // Set the specified element to false
            Debug.Log("Reset hitArray at index: " + index);

            // Check if all elements in the hitArray are false
            bool allFalse = true;

            for (int i = 0; i < hitArray.Length; i++)
            {
                if (hitArray[i] == true)
                {
                    allFalse = false;
                    break;
                }
            }

            // If all elements are false, restore armor
            if (allFalse)
            {
                RestoreArmor();
            }
        }
        else
        {
            Debug.LogWarning("Invalid index passed to ResetHitAtIndex.");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "asteroid")
        {
            TakeDamage(10);
        }
        if(collision.tag == "bullet")
        {
            TakeDamage(1);
        }
    }
}
