using UnityEngine;

public class PauseManager : MonoBehaviour
{
    private bool isPaused = false; // Track whether the game is paused
    public GameObject pauseMenu; // Reference to the pause menu UI

    void Update()
    {
        // Check for the pause key (Escape key in this example)
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    // Method to pause the game
    public void PauseGame()
    {
        Time.timeScale = 0f; // Pause the game
        isPaused = true; // Update the paused state
        pauseMenu.SetActive(true); // Show the pause menu
        Debug.Log("Game Paused");
    }

    // Method to resume the game
    public void ResumeGame()
    {
        Time.timeScale = 1f; // Resume the game
        isPaused = false; // Update the paused state
        pauseMenu.SetActive(false); // Hide the pause menu
        Debug.Log("Game Resumed");
    }
}
