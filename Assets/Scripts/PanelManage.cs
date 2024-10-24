using UnityEngine;

public class PanelManage : MonoBehaviour
{
    public GameObject[] panels;  // Assign your panels in the Inspector
    private int currentPanelIndex = 0;  // Track the current panel index
    public GameManager GameManager;
    public GameObject additionalComponent; // Assign your additional component in the Inspector

    void Start()
    {
        ShowPanel(0);  // Start with the first panel
        GameManager = FindObjectOfType<GameManager>();
        Time.timeScale = 0f;
    }

    void Update()
    {
        // Check for a left mouse click (or touch input on mobile)
        if (Input.GetMouseButtonDown(0))
        {
            ShowNextPanel();
        }
    }

    // Move to the next panel when the screen is clicked
    private void ShowNextPanel()
    {
        if (currentPanelIndex < panels.Length - 1)
        {
            panels[currentPanelIndex].SetActive(false);
            currentPanelIndex++;
            ShowPanel(currentPanelIndex);
        }
        else
        {
            // Show additional component after the last panel
            StartLevel();
        }
    }

    // Start the level after the last panel
    private void StartLevel()
    {
        panels[currentPanelIndex].SetActive(false); // Hide the last panel
        additionalComponent.SetActive(true); // Show the additional component
        GameManager.NewGame(); // Start the game
        Debug.Log("Level Started!");
        Time.timeScale = 1f; // Resume the game
    }

    // Show the panel based on the index
    private void ShowPanel(int index)
    {
        panels[index].SetActive(true);
    }
}
