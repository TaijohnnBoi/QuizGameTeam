using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreenManager : MonoBehaviour
{
    public float displayTime = 3f;  // Time in seconds the splash screen will be shown
    public string nextSceneName = "MainMenu";  // The name of the scene to load next

    private void Start()
    {
        // Start the coroutine to wait and then load the next scene
        StartCoroutine(LoadNextSceneAfterDelay());
    }

    private IEnumerator LoadNextSceneAfterDelay()
    {
        // Wait for the specified display time
        yield return new WaitForSeconds(displayTime);

        // Load the next scene
        SceneManager.LoadScene(nextSceneName);
    }
}

