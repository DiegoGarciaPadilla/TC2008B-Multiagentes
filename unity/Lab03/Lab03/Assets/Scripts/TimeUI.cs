using UnityEngine;
using TMPro;

/// <summary>
/// This class is responsible for displaying the time in the game.
/// </summary>
public class TimeUI : MonoBehaviour
{
    // Reference to the TextMeshProUGUI component
    public TextMeshProUGUI timeText;

    /// <summary>
    /// This method is called when the object becomes enabled and active.
    /// </summary>
    private void OnEnable()
    {
        // Subscribe to the OnMinuteChanged and OnHourChanged events
        TimeManager.OnMinuteChanged += UpdateTime;
        TimeManager.OnHourChanged += UpdateTime;
    }

    /// <summary>
    /// This method is called when the object becomes enabled and active.
    /// </summary>
     private void OnDisable()
    {
        // Unsubscribe from the OnMinuteChanged and OnHourChanged events
        TimeManager.OnMinuteChanged -= UpdateTime;
        TimeManager.OnHourChanged -= UpdateTime;
    }

    /// <summary>
    /// Updates the time displayed in the UI.
    /// </summary>
    private void UpdateTime()
    {
        timeText.text = $"{TimeManager.Hour.ToString("00")}:{TimeManager.Minute:00}";
    }

}
