using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LifeSystem : MonoBehaviour
{
    public int maxLives = 3;                  // Maximum number of lives
    private int currentLives;                 // Current number of lives

    public TextMeshProUGUI livesText;         // Reference to the lives text
    public GameObject gameOverScreen;         // Game over UI (Game Over Text + Retry Button)

    void Start()
    {
        currentLives = maxLives;              // Initialize lives
        UpdateLivesDisplay();                 // Update the lives display
        gameOverScreen.SetActive(false);      // Ensure the game over screen is hidden
    }

    public void LoseLife()
    {
        currentLives--;                       // Decrease the number of lives
        UpdateLivesDisplay();                 // Update the display

        if (currentLives <= 0)
        {
            GameOver();                       // Trigger game over if no lives left
        }
    }

    private void UpdateLivesDisplay()
    {
        livesText.text = $"Lives: {currentLives}"; // Update the lives text
    }

    private void GameOver()
    {
        gameOverScreen.SetActive(true);       // Show the game over screen
        Time.timeScale = 0;                   // Pause the game (optional)
    }

    public void RetryGame()
    {
        currentLives = maxLives;              // Reset lives
        UpdateLivesDisplay();                 // Update the display
        gameOverScreen.SetActive(false);      // Hide the game over screen
        Time.timeScale = 1;                   // Resume the game (if paused)
    }
}

