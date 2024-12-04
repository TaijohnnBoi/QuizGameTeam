using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public GameObject[] Levels; // Array of level objects
    public Image goodFlashScreen; // Reference to the "good" flash screen
    public float flashDuration = 0.5f; // Duration of the flash effect

    int currentLevel;

    void Start()
    {
        // Ensure the good flash screen is hidden at the start
        if (goodFlashScreen != null)
        {
            goodFlashScreen.gameObject.SetActive(false);
        }
    }

    public void correctAnswer()
    {
        if (currentLevel + 1 != Levels.Length)
        {
            Levels[currentLevel].SetActive(false);

            currentLevel++;
            Levels[currentLevel].SetActive(true);

            // Trigger the flash effect
            if (goodFlashScreen != null)
            {
                StartCoroutine(FlashGoodScreen());
            }
        }
    }

    private System.Collections.IEnumerator FlashGoodScreen()
    {
        // Activate the flash screen
        goodFlashScreen.gameObject.SetActive(true);

        // Gradually fade in the flash screen
        Color color = goodFlashScreen.color;
        float elapsedTime = 0f;

        // Fade in
        while (elapsedTime < flashDuration / 2)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(0, 1, elapsedTime / (flashDuration / 2));
            goodFlashScreen.color = color;
            yield return null;
        }

        // Reset elapsed time for fade-out
        elapsedTime = 0f;

        // Fade out
        while (elapsedTime < flashDuration / 2)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(1, 0, elapsedTime / (flashDuration / 2));
            goodFlashScreen.color = color;
            yield return null;
        }

        // Deactivate the flash screen
        goodFlashScreen.gameObject.SetActive(false);
    }
}
