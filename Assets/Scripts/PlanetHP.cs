using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanetHP : MonoBehaviour
{
    public Slider HP;
    public float HPReduction;

    public GameObject WHolePlanet;
    public GameObject particle;

    public GameObject bulletParticle;

    // Array of GameObjects to spawn upon planet destruction (no repeats)
    public GameObject[] spawnObjects;

    // Range around the planet to spawn new objects
    public float spawnRadius = 2f;

    // Start is called before the first frame update
    void Start()
    {
        
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
        if (collision.tag == "bulletNew")
        {
            // Reduce HP when hit by bullet
            HP.value -= HPReduction / 100;

            // Instantiate particle effect for bullet impact
            Instantiate(bulletParticle, collision.transform.position, Quaternion.identity);
        }
    }

    // Function to spawn objects after planet destruction (no duplicate spawns)
    private void SpawnObjects()
    {
        // Loop through each object in the spawnObjects array and spawn them at random positions
        foreach (GameObject objectToSpawn in spawnObjects)
        {
            // Calculate random position within the spawn radius
            Vector3 randomPosition = transform.position + (Random.insideUnitSphere * spawnRadius);
            randomPosition.z = 0; // Ensure objects stay on the 2D plane

            // Instantiate the object at the random position
            Instantiate(objectToSpawn, randomPosition, Quaternion.identity);
        }
    }
}
