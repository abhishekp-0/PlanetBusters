using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    public float maxHealth = 100;
    public float maxArmour = 100;
    public Slider healthBar; // Reference to the health bar UI
    public Slider armorBar;

    [SerializeField]
    private float currentHealth;
    [SerializeField]
    private float currentArmor;
    [SerializeField]
    private PlayerMovement PlayerMovement;

    public SpriteRenderer sr;
    public GameObject trailG;


    void Start()
    {
        currentHealth = maxHealth;
        currentArmor = maxArmour;
        UpdateHealthBar();

       
    }

    private void Update()
    {
        if(currentHealth <= 0)
        {
            sr.enabled = false;
            PlayerMovement.enabled = false;
            trailG.SetActive(false);
        }
    }

    public void TakeDamage(int damage)
    {
        if (currentArmor>0)
        {
            Debug.Log("Armor absorbed the hit!");
            currentArmor -= damage;
            if (currentArmor <= 0)
            {
                currentArmor = 0;
            }
            UpdateArmorBar(); // Update the UI
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

    void UpdateArmorBar()
    {
        if (armorBar != null)
        {
            armorBar.value = (float)currentArmor / maxHealth;
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
        
        Debug.Log("Armor restored!");
    }

    // Function to set a specific index in hitArray to false and check if all elements are false
    

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
        if(collision.tag == "missile")
        {
            TakeDamage(30);
        }
    }
}
