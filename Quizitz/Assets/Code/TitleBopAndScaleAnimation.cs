using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleBopAndScaleAnimation : MonoBehaviour
{
    public float amplitude = 20f;             // How high the title moves up and down
    public float frequency = 2f;              // How fast the title bops up and down
    public float startScale = 2f;             // Starting scale size (e.g., 2x original size)
    public float targetScale = 1f;            // Target scale size (1x original size)
    public float scaleSpeed = 2f;             // Speed of scaling animation

    private RectTransform rectTransform;
    private float initialY;                   // Store the initial Y position
    private bool isScaling = true;            // Whether the title is still scaling

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

        // Set the initial scale of the title
        rectTransform.localScale = Vector3.one * startScale;
    }

    private void Update()
    {
        // Handle the scaling animation at the start
        if (isScaling)
        {
            // Gradually scale down to the target scale
            rectTransform.localScale = Vector3.Lerp(rectTransform.localScale, Vector3.one * targetScale, Time.deltaTime * scaleSpeed);

            // Stop scaling once we're close enough to the target scale
            if (Mathf.Abs(rectTransform.localScale.x - targetScale) < 0.01f)
            {
                rectTransform.localScale = Vector3.one * targetScale;
                isScaling = false;
            }
        }

        // Bopping effect
        float newY = initialY + Mathf.Sin(Time.time * frequency) * amplitude;
        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, newY);
    }
}

