using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    // Singleton instance
    public static CameraShake instance { get; private set; }

    // Reference to the Cinemachine Virtual Camera
    private CinemachineVirtualCamera cinemachineCamera;

    // Shake properties
    private float shakeTimer;
    private float shakeTimerTotal;
    private float startingIntensity;
    private Vector3 originalPosition;
    private Quaternion originalRotation;  // To store original rotation

    void Awake()
    {
        // Singleton setup
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return; // Exit if another instance exists
        }

        // Get the Cinemachine Virtual Camera component
        cinemachineCamera = GetComponent<CinemachineVirtualCamera>();

        if (cinemachineCamera == null)
        {
            Debug.LogError("Cinemachine Virtual Camera component missing.");
        }

        // Store the original position and rotation of the camera
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    /// <summary>
    /// Triggers the camera shake in X and Y directions with a given intensity and duration, keeping Z-axis and rotation fixed.
    /// </summary>
    /// <param name="intensity">The shake intensity for X and Y.</param>
    /// <param name="duration">The shake duration.</param>
    public void ShakeCamera(float intensity, float duration)
    {
        CinemachineBasicMultiChannelPerlin perlinNoise =
            cinemachineCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        if (perlinNoise != null)
        {
            // Set the noise intensity (amplitude gain) for shake
            perlinNoise.m_AmplitudeGain = intensity;

            startingIntensity = intensity;
            shakeTimerTotal = duration;
            shakeTimer = duration;
        }
        else
        {
            Debug.LogError("CinemachineBasicMultiChannelPerlin component missing.");
        }
    }

    void Update()
    {
        // If the shake timer is running, continue shaking the camera
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;

            // Gradually reduce the shake intensity over time
            CinemachineBasicMultiChannelPerlin perlinNoise =
                cinemachineCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

            if (perlinNoise != null)
            {
                perlinNoise.m_AmplitudeGain = Mathf.Lerp(startingIntensity, 0f, 1 - (shakeTimer / shakeTimerTotal));
            }

            // Reset shake when timer ends
            if (shakeTimer <= 0f)
            {
                perlinNoise.m_AmplitudeGain = 0f;
            }
        }

        // Keep the Z-axis fixed at -20 and prevent any unwanted rotation
        Vector3 currentPosition = transform.position;
        transform.position = new Vector3(currentPosition.x, currentPosition.y, -20f);

        // Restore the original rotation to prevent any rotational shake
        transform.rotation = originalRotation;
    }
}
