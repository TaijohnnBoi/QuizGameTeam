using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleAnimation : MonoBehaviour
{
    public float targetYPosition = 0f;   // Final Y position for the title in the center
    public float speed = 2f;             // Speed of the animation

    private RectTransform rectTransform;
    private float velocity = 0f;         // Used for SmoothDamp

    private void Start()
    {
        // Get the RectTransform component of the title
        rectTransform = GetComponent<RectTransform>();

        // Check if the component exists
        if (rectTransform == null)
        {
            Debug.LogError("RectTransform component not found!");
            enabled = false;
        }
    }

    private void Update()
    {
        // Smoothly move the Y position to the target
        float newYPosition = Mathf.SmoothDamp(rectTransform.anchoredPosition.y, targetYPosition, ref velocity, 1 / speed);

        // Apply the new Y position
        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, newYPosition);

        // Stop the animation when the title is close enough to the target position
        if (Mathf.Abs(newYPosition - targetYPosition) < 0.1f)
        {
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, targetYPosition);
            enabled = false; // Stop updating once the position is reached
        }
    }
}
