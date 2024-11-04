using UnityEngine;
using System;

/// <summary>
/// TimeManager class is responsible for managing the time in the game.
/// </summary>
public class TimeManager : MonoBehaviour
{
    // Action to be called when a minute or hour changes.
    public static Action OnMinuteChanged;
    public static Action OnHourChanged;

    // Static properties to get the current minute and hour.
    public static int Minute{get; private set;}
    public static int Hour{get;private set;}

    // Time to pass in the game (1 minute in real life is 0.5 minutes in the game).
    private float minuteToRealTime = 0.5f;
    private float timer;


    /// <summary>
    /// Start is called before the first frame update.
    /// </summary>
    void Start()
    {  
        // Initialize minute and hour.
        Minute = 0;
        Hour = 0;

        // Initialize the timer.
        timer = minuteToRealTime;
    }

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    void Update()
    {
        // Decrease the timer.
        timer -= Time.deltaTime;

        // If the timer is less than or equal to 0, increase the minute.
        if(timer <= 0)
        {
            // Increase the minute.
            Minute++;

            // Call the event.
            OnMinuteChanged?.Invoke();

            // If the minute is greater than or equal to 60, increase the hour.
            if(Minute >= 60)
            {
                // Increase the hour.
                Hour++;

                // Call the event.
                OnHourChanged?.Invoke();

                // Reset the minute.
                Minute = 0;
            }

            // Reset the timer.
            timer = minuteToRealTime;
        }
    }
}
