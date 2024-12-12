using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LifeSystem : MonoBehaviour
{
    public int maxLives = 3;                  // Maximum number of lives
    private int currentLives;                 // Current number of lives

    public TextMeshProUGUI livesText;         // Reference to the lives text
    public GameObject gameOverScreen;         // Game over UI (Game Over Text + Retry Button)
    public Image flashImage;                  // Reference to the flash image
    public float flashDuration = 0.5f;        // Duration of the flash effect

    void Start()
    {
        currentLives = maxLives;              // Initialize lives
        UpdateLivesDisplay();                 // Update the lives display
        gameOverScreen.SetActive(false);      // Ensure the game over screen is hidden

        if (flashImage != null)
        {
            flashImage.color = new Color(1, 1, 1, 0); // Ensure flash starts invisible
        }
    }

    public void LoseLife()
    {
        currentLives--;                       // Decrease the number of lives
        UpdateLivesDisplay();                 // Update the display

        // Trigger the flash effect
        if (flashImage != null)
        {
            StartCoroutine(FlashEffect());
        }

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


   

    private System.Collections.IEnumerator FlashEffect()
    {
        // Fade in
        float elapsedTime = 0f;
        while (elapsedTime < flashDuration / 2)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(0, 1, elapsedTime / (flashDuration / 2));
            flashImage.color = new Color(1, 1, 1, alpha);
            yield return null;
        }

        // Fade out
        elapsedTime = 0f;
        while (elapsedTime < flashDuration / 2)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1, 0, elapsedTime / (flashDuration / 2));
            flashImage.color = new Color(1, 1, 1, alpha);
            yield return null;
        }

        // Ensure the flash is fully transparent after fading
        flashImage.color = new Color(1, 1, 1, 0);
    }
}
