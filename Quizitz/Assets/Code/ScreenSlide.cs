using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenSlide : MonoBehaviour
{
    public RectTransform whiteScreen;  // Assign your white screen RectTransform
    public float slideDuration = 1.0f; // Duration of the slide-in effect

    public void PlaySlideIn()
    {
        StartCoroutine(SlideIn());
    }

    private IEnumerator SlideIn()
    {
        Vector2 startPosition = new Vector2(-Screen.width, 0); // Off-screen
        Vector2 endPosition = Vector2.zero;                   // Center screen

        float elapsedTime = 0f;

        while (elapsedTime < slideDuration)
        {
            elapsedTime += Time.deltaTime;
            whiteScreen.anchoredPosition = Vector2.Lerp(startPosition, endPosition, elapsedTime / slideDuration);
            yield return null;
        }

        whiteScreen.anchoredPosition = endPosition; // Ensure it's centered
    }
}
