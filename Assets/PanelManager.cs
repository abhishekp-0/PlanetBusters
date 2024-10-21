using UnityEngine;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour
{
    public GameObject[] panels;  // Assign your panels in the Inspector
    private int currentPanelIndex = 0;  // Track the current panel index
    public GameManager GameManager;

    void Start()
    {
        ShowPanel(0);  // Start with the first panel
        GameManager=FindObjectOfType<GameManager>();
        Time.timeScale = 0f;
    }

    // Call this when the "Next" button is clicked
    public void ShowNextPanel()
    {
        if (currentPanelIndex < panels.Length - 1)
        {
            panels[currentPanelIndex].SetActive(false);
            currentPanelIndex++;
            ShowPanel(currentPanelIndex);
        }
    }

    // Call this when the "Start Level" button is clicked
    public void StartLevel()
    {
        // Code to start your level (e.g., load the scene or start gameplay)
        panels[currentPanelIndex].SetActive(false);
        GameManager.NewGame();
        Debug.Log("Level Started!");
        Time.timeScale = 1f;
    }

    // Show the panel based on index
    private void ShowPanel(int index)
    {
        panels[index].SetActive(true);
    }
}
