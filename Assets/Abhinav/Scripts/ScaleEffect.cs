using UnityEngine;

public class ScaleEffect : MonoBehaviour
{
    // Array of GameObjects to scale
    public GameObject[] gameObjectsToScale;

    // Duration to reach full size
    public float scaleDuration = 2f;

    // Flag to trigger the scaling effect
    private bool startScaling = false;

    // Original sizes of the GameObjects
    private Vector3[] originalSizes;

    void Start()
    {
        // Initialize the original sizes array
        originalSizes = new Vector3[gameObjectsToScale.Length];

        // Store the original sizes of all GameObjects and set their scale to 0
        for (int i = 0; i < gameObjectsToScale.Length; i++)
        {
            originalSizes[i] = gameObjectsToScale[i].transform.localScale;
            gameObjectsToScale[i].transform.localScale = Vector3.zero;

            // Initially, deactivate the GameObjects
            gameObjectsToScale[i].SetActive(false);
        }
    }

    void Update()
    {
        // If scaling is triggered, start scaling all objects
        if (startScaling)
        {
            for (int i = 0; i < gameObjectsToScale.Length; i++)
            {
                // Activate each GameObject before scaling it
                if (!gameObjectsToScale[i].activeSelf)
                {
                    gameObjectsToScale[i].SetActive(true);
                }

                // Smoothly scale the GameObjects from 0 to their original size
                gameObjectsToScale[i].transform.localScale = Vector3.Lerp(
                    gameObjectsToScale[i].transform.localScale,
                    originalSizes[i],
                    Time.deltaTime / scaleDuration
                );
            }
        }
    }

    // OnTriggerEnter2D is called when another object enters the trigger collider attached to the GameObject
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the colliding object has the tag "bulletNew"
        if (other.CompareTag("bulletNew"))
        {
            // Set startScaling to true when the trigger happens
            startScaling = true;
        }
    }
}
