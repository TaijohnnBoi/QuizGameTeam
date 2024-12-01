using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SplashScreenManager : MonoBehaviour
{
    public float displayTime = 3f; // Time in seconds the splash screen will be shown
    public string nextSceneName = "MainMenu"; // The name of the scene to load next
    public Image blackScreen; // Reference to the black screen Image
    public float fadeDuration = 1f; // Duration of the fade effect

    private void Start()
    {
        // Ensure the black screen starts fully transparent
        if (blackScreen != null)
        {
            blackScreen.color = new Color(0, 0, 0, 0); // Fully transparent
        }

        // Start the splash screen coroutine
        StartCoroutine(HandleSplashScreen());
    }

    private IEnumerator HandleSplashScreen()
    {
        // Wait for the display time
        yield return new WaitForSeconds(displayTime);

        // Start the fade-out effect
        if (blackScreen != null)
        {
            yield return StartCoroutine(FadeToBlack());
        }

        // Load the next scene
        SceneManager.LoadScene(nextSceneName);
    }

    private IEnumerator FadeToBlack()
    {
        float elapsedTime = 0f;

        // Gradually increase the alpha value of the black screen
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            blackScreen.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        // Ensure it's fully black
        blackScreen.color = new Color(0, 0, 0, 1);
    }
}
