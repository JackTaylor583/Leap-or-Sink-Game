using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class StaticSettings : MonoBehaviour
{
    public static float Sensitivity = 400f;
    public Slider sensitivitySlider; 

    private const string PREF_KEY = "MouseSensitivity";

    
    void OnEnable() // Called whenever this GameObject is enabled
    {
        
        if (PlayerPrefs.HasKey(PREF_KEY)) // Load from PlayerPrefs if it exists, otherwise leave Sensitivity = 400
        {
            Sensitivity = PlayerPrefs.GetFloat(PREF_KEY);
        }

        if (sensitivitySlider != null)  // If the slider has been assigned, force it to show the loaded/default value
        {
            sensitivitySlider.onValueChanged.RemoveListener(OnSliderChanged); // Temporarily remove any existing listener (in case OnEnable runs multiple times)

            sensitivitySlider.value = Sensitivity; // Set the slider knob to match the saved sensitivity

            sensitivitySlider.onValueChanged.AddListener(OnSliderChanged); // Add the listener so changes will be saved
        }
        
    }

    void OnDisable() // Called whenever this GameObject is disabled
    {
        if (sensitivitySlider != null)
        {
            sensitivitySlider.onValueChanged.RemoveListener(OnSliderChanged);
        }
    }

    void OnSliderChanged(float newValue) // Called every time the user drags the slider
    {
        Sensitivity = newValue;
        PlayerPrefs.SetFloat(PREF_KEY, newValue);
        PlayerPrefs.Save();
    }
}

