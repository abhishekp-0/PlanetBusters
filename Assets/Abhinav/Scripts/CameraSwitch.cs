using UnityEngine;
using Cinemachine;

public class CameraSwitch : MonoBehaviour
{
    // Array of Cinemachine Virtual Cameras
    public CinemachineVirtualCamera[] virtualCameras;

    // Currently active camera index
    private int currentCameraIndex = 0;

    void Start()
    {
        // Ensure that only the initial camera is active
        SetActiveCamera(currentCameraIndex);
    }

    void Update()
    {
        // Check if the Z key is pressed
        if (Input.GetKeyDown(KeyCode.Z))
        {
            // Switch to the next camera when Z is pressed
            SwitchToNextCamera();
        }
    }

    // Switch to the next camera in the array
    public void SwitchToNextCamera()
    {
        // Disable the current camera
        virtualCameras[currentCameraIndex].Priority = 0;

        // Increment the camera index (loop back if needed)
        currentCameraIndex = (currentCameraIndex + 1) % virtualCameras.Length;

        // Enable the next camera
        virtualCameras[currentCameraIndex].Priority = 10;
    }

    // Helper function to ensure only one camera is active
    void SetActiveCamera(int cameraIndex)
    {
        for (int i = 0; i < virtualCameras.Length; i++)
        {
            virtualCameras[i].Priority = (i == cameraIndex) ? 10 : 0;
        }
    }
}
