using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        // Ensure only one instance of the GameManager exists
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // Persist across scenes
    }

    /// <summary>
    /// Restarts the current scene.
    /// </summary>
    public void RestartScene()
    {
        Time.timeScale = 1; // Ensure the game is unfrozen
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene
    }

    /// <summary>
    /// Loads a specific scene by name.
    /// </summary>
    public void LoadScene(string sceneName)
    {
        Time.timeScale = 1; // Ensure the game is unfrozen
        SceneManager.LoadScene(sceneName); // Load the specified scene
    }

    /// <summary>
    /// Quits the application.
    /// </summary>
    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }
}
