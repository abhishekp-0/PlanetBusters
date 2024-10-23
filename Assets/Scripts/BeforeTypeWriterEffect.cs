using System.Collections;
using TMPro;
using UnityEngine;

public class BeforeTypewriterEffect : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;  // Assign your TextMeshPro text here
    public float typingSpeed = 0.05f;    // Adjust typing speed as needed
    private string fullText;             // Holds the complete text to be displayed
    private bool isTyping = false;       // Tracks if typing is ongoing

    private void Start()
    {
        fullText = textDisplay.text;     // Store the full text
        textDisplay.text = "";           // Initially set text to empty
    }

    // Coroutine to type text letter by letter
    public IEnumerator StartTyping()
    {
        isTyping = true;                 // Set typing flag to true
        textDisplay.text = "";           // Reset text before typing starts
        foreach (char letter in fullText.ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);  // Wait before typing next letter
        }
        isTyping = false;                // Mark typing as complete
    }

    // Method to check if typing is completed
    public bool IsTypingCompleted()
    {
        return !isTyping;  // Returns true if typing is done
    }
}
