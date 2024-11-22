using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GhostEnemy : MonoBehaviour
{
    public int clicksToDisappear = 4;    // Number of clicks required to remove the ghost
    public float respawnTime = 3f;       // Time in seconds before the ghost reappears
    public float shrinkTime = 1f;        // Time for the ghost to shrink to normal size
    public float disappearTime = 2f;     // Time for the ghost to disappear after being clicked
    private int currentClicks = 0;       // Tracks the player's clicks

    private Button ghostButton;          // Reference to the button component
    private RectTransform ghostTransform; // Reference to the ghost's RectTransform
    private Vector3 originalSize;       // Original size of the ghost

    void Start()
    {
        ghostButton = GetComponent<Button>();
        ghostButton.onClick.AddListener(OnGhostClicked); // Add click listener

        ghostTransform = GetComponent<RectTransform>();
        originalSize = ghostTransform.localScale; // Store the original size of the ghost

        Respawn(); // Make sure the ghost is active at the start
    }

    void OnGhostClicked()
    {
        currentClicks++;

        if (currentClicks >= clicksToDisappear)
        {
            // Start disappearing process after enough clicks
            StartCoroutine(FadeOutGhost());
        }
    }

    // Coroutine to handle the ghost shrinking and then disappearing
    private IEnumerator FadeOutGhost()
    {
        // Shrink the ghost back to normal size over time
        float timeElapsed = 0f;
        Vector3 largeSize = originalSize * 1.5f;  // Start size (1.5x normal size)
        while (timeElapsed < shrinkTime)
        {
            ghostTransform.localScale = Vector3.Lerp(largeSize, originalSize, timeElapsed / shrinkTime);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        ghostTransform.localScale = originalSize;

        // Now fade out the ghost over time
        timeElapsed = 0f;
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>(); // Add CanvasGroup if it doesn't exist
        }

        while (timeElapsed < disappearTime)
        {
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, timeElapsed / disappearTime);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = 0f;
        HideGhost();
        Invoke(nameof(Respawn), respawnTime); // Respawn the ghost after a delay
    }

    void HideGhost()
    {
        gameObject.SetActive(false); // Hide the ghost
    }

    void Respawn()
    {
        currentClicks = 0;            // Reset the click count
        gameObject.SetActive(true);  // Show the ghost
        StartCoroutine(ShrinkGhost()); // Start by shrinking the ghost to its normal size
    }

    // Coroutine to handle the ghost's size shrinking animation when it spawns
    private IEnumerator ShrinkGhost()
    {
        float timeElapsed = 0f;
        Vector3 largeSize = originalSize * 1.5f;  // Start size (1.5x normal size)
        ghostTransform.localScale = largeSize;

        while (timeElapsed < shrinkTime)
        {
            ghostTransform.localScale = Vector3.Lerp(largeSize, originalSize, timeElapsed / shrinkTime);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        ghostTransform.localScale = originalSize;
    }
}
