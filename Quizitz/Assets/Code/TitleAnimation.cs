using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleAnimation : MonoBehaviour
{
    public float amplitude = 20f;    // How high the title will move up and down
    public float frequency = 2f;     // How fast the title bops up and down

    private RectTransform rectTransform;
    private float initialY;          // Store the initial Y position

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        if (rectTransform == null)
        {
            Debug.LogError("RectTransform component not found!");
            enabled = false;
            return;
        }

        // Store the initial Y position of the title
        initialY = rectTransform.anchoredPosition.y;
    }

    private void Update()
    {
        // Calculate the new Y position with a sine wave for bopping effect
        float newY = initialY + Mathf.Sin(Time.time * frequency) * amplitude;

        // Apply the new Y position
        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, newY);
    }
}