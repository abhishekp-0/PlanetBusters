using System.Collections;
using UnityEngine;

public class AlphaFader : MonoBehaviour
{
    // Minimum and maximum alpha values for the fade effect
    public float minAlpha = 0.2f; // Minimum alpha value
    public float maxAlpha = 1f;   // Maximum alpha value

    // Duration for fading from minAlpha to maxAlpha
    public float fadeDuration = 1f;

    // Reference to the SpriteRenderer
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        // Get the SpriteRenderer component attached to this object
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Start the alpha fading loop
        StartCoroutine(FadeAlphaLoop());
    }

    // Coroutine to smoothly fade the alpha value between minAlpha and maxAlpha
    IEnumerator FadeAlphaLoop()
    {
        while (true) // Infinite loop for continuous fade in and out
        {
            // Fade in from minAlpha to maxAlpha
            yield return StartCoroutine(FadeAlpha(minAlpha, maxAlpha));

            // Fade out from maxAlpha back to minAlpha
            yield return StartCoroutine(FadeAlpha(maxAlpha, minAlpha));
        }
    }

    // Coroutine to fade the alpha from startAlpha to endAlpha
    IEnumerator FadeAlpha(float startAlpha, float endAlpha)
    {
        float elapsedTime = 0f;

        // Get the current color of the sprite
        Color spriteColor = spriteRenderer.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;

            // Calculate the new alpha value based on elapsed time
            float newAlpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);

            // Set the new color with the updated alpha value
            spriteColor.a = newAlpha;
            spriteRenderer.color = spriteColor;

            // Wait until the next frame
            yield return null;
        }

        // Ensure the final alpha is exactly the target alpha
        spriteColor.a = endAlpha;
        spriteRenderer.color = spriteColor;
    }
}
