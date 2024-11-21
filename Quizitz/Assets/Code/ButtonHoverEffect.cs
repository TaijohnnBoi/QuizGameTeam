using UnityEngine;
using UnityEngine.EventSystems;  // Required for Event Triggers

public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Vector3 originalScale;
    [SerializeField] private float scaleFactor = 1.2f;  // How much bigger the button will get when hovered over
    [SerializeField] private float scaleSpeed = 0.2f;   // Speed of the scaling transition

    private void Start()
    {
        // Store the original size of the button
        originalScale = transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Scale up the button when mouse enters
        StopAllCoroutines();  // Stop any ongoing scaling animation
        StartCoroutine(ScaleButton(transform.localScale, originalScale * scaleFactor));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Scale down the button when mouse exits
        StopAllCoroutines();  // Stop any ongoing scaling animation
        StartCoroutine(ScaleButton(transform.localScale, originalScale));
    }

    private System.Collections.IEnumerator ScaleButton(Vector3 fromScale, Vector3 toScale)
    {
        float timeElapsed = 0;

        while (timeElapsed < scaleSpeed)
        {
            transform.localScale = Vector3.Lerp(fromScale, toScale, timeElapsed / scaleSpeed);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        transform.localScale = toScale;  // Ensure it ends at the final scale
    }
}
