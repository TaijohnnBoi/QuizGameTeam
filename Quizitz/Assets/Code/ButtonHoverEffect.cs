using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Vector3 hoverScale = new Vector3(0.9f, 0.9f, 0.9f);  // Scale when hovering
    public float animationSpeed = 10f;                          // Speed of the scaling effect

    private Vector3 originalScale;
    private RectTransform rectTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        originalScale = rectTransform.localScale;               // Store the original scale
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Start scaling to the hover scale when the mouse enters
        StopAllCoroutines();
        StartCoroutine(ScaleTo(hoverScale));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Return to the original scale when the mouse exits
        StopAllCoroutines();
        StartCoroutine(ScaleTo(originalScale));
    }

    private System.Collections.IEnumerator ScaleTo(Vector3 targetScale)
    {
        while (Vector3.Distance(rectTransform.localScale, targetScale) > 0.01f)
        {
            rectTransform.localScale = Vector3.Lerp(rectTransform.localScale, targetScale, Time.deltaTime * animationSpeed);
            yield return null;
        }
        rectTransform.localScale = targetScale;  // Snap to the target scale when close enough
    }
}

