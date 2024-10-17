using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderValueToText : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI percentageText;
    public float timerDuration = 10f; // Duration in seconds
    private float timer;
    public bool active = true; // Timer control

    void Start()
    {
        timer = timerDuration;
        slider.value = 1f;
        UpdateSliderText(slider.value);
        slider.onValueChanged.AddListener(UpdateSliderText);
    }

    void Update()
    {
        if (active && timer > 0)
        {
            timer -= Time.deltaTime;
            slider.value = Mathf.Clamp01(timer / timerDuration);
            UpdateSliderText(slider.value);
        }
    }

    void UpdateSliderText(float value)
    {
        percentageText.text = $"{Mathf.RoundToInt(value * 100)}%"; // Update percentage text
    }

    void OnDestroy() => slider.onValueChanged.RemoveListener(UpdateSliderText); // Clean up
}
