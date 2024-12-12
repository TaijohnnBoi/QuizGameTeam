using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SwitchScene : MonoBehaviour
{
    public Image fadeImage; // Assign a black Image UI element in the Inspector
    public float fadeDuration = 1f; // Duration of the fade effect

    public void SwitchScenes(string sceneName)
    {
        Time.timeScale = 1; // Ensure the game is running at normal speed
        StartCoroutine(FadeAndSwitch(sceneName));
    }

    private IEnumerator FadeAndSwitch(string sceneName)
    {
        // Fade to black
        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            Color newColor = fadeImage.color;
            newColor.a = Mathf.Lerp(0, 1, timer / fadeDuration);
            fadeImage.color = newColor;
            yield return null;
        }

        // Load the new scene
        SceneManager.LoadScene(sceneName);

        // Fade from black (optional, if you want to fade in the new scene)
        timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            Color newColor = fadeImage.color;
            newColor.a = Mathf.Lerp(1, 0, timer / fadeDuration);
            fadeImage.color = newColor;
            yield return null;
        }
    }
}
