using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseScreen; // Reference to the pause screen UI
    private int pauseLimit = 5;    // Maximum number of pauses allowed
    private int currentPauses = 0; // Number of times the game has been paused
    private bool isPaused = false; // Tracks if the game is currently paused

    void Update()
    {
        // Check if the player presses the space key
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isPaused && currentPauses < pauseLimit)
            {
                PauseGame();
            }
            else if (isPaused)
            {
                ResumeGame();
            }
        }
    }

    void PauseGame()
    {
        isPaused = true;
        currentPauses++;
        Time.timeScale = 0;       // Stop game time
        pauseScreen.SetActive(true); // Show the pause screen
        Debug.Log($"Game paused. Pauses left: {pauseLimit - currentPauses}");
    }

    void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1;        // Resume game time
        pauseScreen.SetActive(false); // Hide the pause screen
    }
}
