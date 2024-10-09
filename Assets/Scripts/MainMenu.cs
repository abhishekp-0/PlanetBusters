using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Load the main game scene when Play is clicked
    public void PlayGame()
    {
        SceneManager.LoadScene("SampleScene");  // Make sure your game scene name matches here
    }

    public void QuitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();  // This will not quit the editor but will work in a built game
    }
}
