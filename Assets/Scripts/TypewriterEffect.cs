using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TypewriterEffect : MonoBehaviour
{
    public Text uiText;  // Legacy Text component reference
    [TextArea(3, 10)] public string[] textEntries;  // Array of strings (editable in Inspector)
    public float typingSpeed = 0.05f;  // Delay between each character
    public float delayBetweenEntries = 3f;  // Delay between typing each entry

    private string currentText = "";  // Text displayed so far

    void Start()
    {
        if (textEntries.Length > 0)
        {
            StartCoroutine(TypeTextEntries());
        }
    }

    IEnumerator TypeTextEntries()
    {
        for (int entryIndex = 0; entryIndex < textEntries.Length; entryIndex++)
        {
            // Get the full text from the current string entry
            string fullText = textEntries[entryIndex];
            uiText.text = "";  // Clear the text initially
            currentText = "";

            // Type out the current text entry
            for (int i = 0; i < fullText.Length; i++)
            {
                currentText = fullText.Substring(0, i + 1);  // Get part of the string
                uiText.text = currentText;  // Update the text component
                yield return new WaitForSeconds(typingSpeed);  // Wait before typing next character
            }

            // Wait for a delay before typing the next entry
            yield return new WaitForSeconds(delayBetweenEntries);
        }
    }
}
