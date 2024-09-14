using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    // Set the timer duration in seconds
    public float timeRemaining = 180;
    public bool timerIsRunning = false;

    // Reference to the TextMeshPro text component
    public TMP_Text timerText;

    private void Start()
    {
        // Start the timer
        timerIsRunning = true;
    }

    private void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                // Subtract the time passed from the remaining time
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                // Timer has finished
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;
                DisplayTime(timeRemaining); // Display final time as 00:00
            }
        }
    }

    // Function to display the time in MM:SS format
    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1; // Ensure the timer shows 00:00 at the end instead of stopping at 01.

        // Get minutes and seconds
        int minutes = Mathf.FloorToInt(timeToDisplay / 60);
        int seconds = Mathf.FloorToInt(timeToDisplay % 60);

        // Format the time string
        timerText.text = string.Format("{0}:{1:00}", minutes, seconds);
    }
}
