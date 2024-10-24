using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanetHP : MonoBehaviour
{
    public Slider HP;                   // Reference to the health slider
    public float HPReduction;           // Amount to reduce HP per bullet hit

    public GameObject WHolePlanet;      // Reference to the whole planet object
    public GameObject particle;          // Particle effect for planet destruction
    public GameObject bulletParticle;    // Particle effect for bullet impact

    // Array of GameObjects to spawn upon planet destruction
    public GameObject[] spawnObjects;

    // Range around the planet to spawn new objects
    public float spawnRadius = 2f;

    // Number of objects to spawn after destruction
    public int numberOfObjectsToSpawn = 2; // Adjust this as needed

    // Start is called before the first frame update
    void Start()
    {
        // Optionally initialize other variables here
    }

    // Update is called once per frame
    void Update()
    {
        if (HP.value <= 0)
        {
            // Instantiate particle effect for planet destruction
            Instantiate(particle, transform.position, transform.rotation);

            // Call function to spawn objects
            SpawnObjects();

            // Deactivate the planet
            WHolePlanet.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("bulletNew"))
        {
            // Reduce HP when hit by bullet
            HP.value -= HPReduction / 100;

            // Instantiate particle effect for bullet impact
            Instantiate(bulletParticle, collision.transform.position, Quaternion.identity);
        }
    }

    // Function to spawn a random number of objects after planet destruction
    private void SpawnObjects()
    {
        // Create a list to hold the eligible objects to spawn
        List<GameObject> eligibleObjects = new List<GameObject>(spawnObjects);

        // Randomly select objects from the eligible list
        for (int i = 0; i < numberOfObjectsToSpawn; i++)
        {
            if (eligibleObjects.Count == 0)
                break; // Exit if there are no eligible objects left

            // Randomly select an index from the eligible objects
            int randomIndex = Random.Range(0, eligibleObjects.Count);
            GameObject objectToSpawn = eligibleObjects[randomIndex];

            // Calculate random position within the spawn radius
            Vector3 randomPosition = transform.position + (Random.insideUnitSphere * spawnRadius);
            randomPosition.z = 0; // Ensure objects stay on the 2D plane

            // Instantiate the object at the random position
            Instantiate(objectToSpawn, randomPosition, Quaternion.identity);

            // Remove the spawned object from the list to prevent duplicates
            eligibleObjects.RemoveAt(randomIndex);
        }
    }
}
