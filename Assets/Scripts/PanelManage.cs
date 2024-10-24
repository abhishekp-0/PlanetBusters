using UnityEngine;

public class PanelManage : MonoBehaviour
{
    public GameObject[] panels;  // Assign your panels in the Inspector
    private int currentPanelIndex = 0;  // Track the current panel index
    public GameObject newComponent; // New component to show after panels are completed

    void Start()
    {
        // Ensure all panels are inactive except for the first one
        for (int i = 1; i < panels.Length; i++)
        {
            panels[i].SetActive(false);
        }
        ShowPanel(0);  // Start with the first panel
        Time.timeScale = 0f; // Pause the game initially
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
        // Debug logs to check the current panel index
        Debug.Log("Current Panel Index: " + currentPanelIndex);
        
        if (currentPanelIndex < panels.Length - 1)
        {
            panels[currentPanelIndex].SetActive(false);
            currentPanelIndex++;
            ShowPanel(currentPanelIndex);
        }
        else
        {
            // Show new component after the last panel
            ShowNewComponent();
        }
    }

    // Show the new component after the last panel
    private void ShowNewComponent()
    {
        panels[currentPanelIndex].SetActive(false); // Hide the last panel
        newComponent.SetActive(true); // Show the new component
        Debug.Log("New Component Displayed!");
        Time.timeScale = 1f; // Resume the game if you want to continue
    }

    // Show the panel based on the index
    private void ShowPanel(int index)
    {
        panels[index].SetActive(true);
        Debug.Log("Showing Panel: " + index);
    }
}
