using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Singleton instance to manage the game globally

    private void Awake()
    {
        // Ensure there is only one instance of the GameManager
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep this object across scene loads
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Method to restart the game and reset everything
    public void RestartGame()
    {
        // Optionally reset any static or global variables here
        ResetGameState();

        // Load the tutorial scene
        SceneManager.LoadScene("Tutorial");
    }

    // Method to reset game states (example)
    private void ResetGameState()
    {
        // Reset any static or persistent variables here (if applicable)
        LifeSystem.ResetLives(); // Example: Reset lives if you're using a life system
    }

    // Call this to start the main game
    public void StartMainGame()
    {
        SceneManager.LoadScene("MainGame"); // Load the main game scene
    }
}
