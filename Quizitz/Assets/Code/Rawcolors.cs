using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rawcolor : MonoBehaviour
{
    public float colorChangeSpeed = 1f;   // Speed at which the color changes

    private RawImage background;             // Reference to the Image component
    private float hue;                    // Current hue value for the rainbow effect

    private void Start()
    {
        // Get the Image component attached to the background
        background = GetComponent<RawImage>();

        if (background == null)
        {
            Debug.LogError("Image component not found!");
            enabled = false;
        }
    }

    private void Update()
    {
        // Increment the hue value over time, looping back to 0 when it reaches 1
        hue += colorChangeSpeed * Time.deltaTime;
        if (hue > 1f) hue -= 1f;

        // Convert hue to a Color using HSV (Hue, Saturation, Value) and apply it to the background
        background.color = Color.HSVToRGB(hue, 0.8f, 0.8f);
    }
}
