using UnityEngine;
using UnityEngine.UI;

public class EnemyCursorWatcher : MonoBehaviour
{
    public float moveSpeed = 500f;          // Speed at which the enemy scrolls in
    public float waitTime = 2f;            // Time the player must remain still
    public float respawnTime = 5f;         // Time before the enemy reappears
    public float maxMouseMovement = 50f;   // Maximum distance the cursor can move without triggering a jumpscare

    private RectTransform enemyRect;       // Reference to the RectTransform of the enemy
    private Vector2 lastMousePosition;     // Tracks the last mouse position
    private bool isWatchingCursor = false; // If the enemy is currently checking cursor movement
    private bool isActive = false;         // If the enemy is currently on screen
    private float stillTime = 0f;          // Tracks how long the player has kept the mouse still

    void Start()
    {
        enemyRect = GetComponent<RectTransform>();
        HideEnemy(); // Start with the enemy hidden
        Invoke(nameof(Respawn), respawnTime); // Start respawn timer
    }

    void Update()
    {
        if (isWatchingCursor)
        {
            Vector2 currentMousePosition = Input.mousePosition;

            // Check if the mouse moved too far
            if (Vector2.Distance(currentMousePosition, lastMousePosition) > maxMouseMovement)
            {
                TriggerJumpscare();
                return;
            }

            // Increment still time if mouse hasn't moved too much
            stillTime += Time.deltaTime;

            if (stillTime >= waitTime)
            {
                HideEnemy(); // Hide the enemy after the player stops moving the mouse
                Invoke(nameof(Respawn), respawnTime);
            }

            lastMousePosition = currentMousePosition; // Update last mouse position
        }
    }

    public void ShowEnemy()
    {
        isActive = true;
        isWatchingCursor = false;
        stillTime = 0f;

        // Animate the enemy scrolling up quickly
        enemyRect.anchoredPosition = new Vector2(0, -enemyRect.rect.height);
        StartCoroutine(ScrollUp());
    }

    private System.Collections.IEnumerator ScrollUp()
    {
        while (enemyRect.anchoredPosition.y < 0)
        {
            enemyRect.anchoredPosition += Vector2.up * moveSpeed * Time.deltaTime;
            yield return null;
        }

        // Enemy is fully visible
        enemyRect.anchoredPosition = Vector2.zero;
        isWatchingCursor = true; // Start watching the cursor
        lastMousePosition = Input.mousePosition; // Initialize mouse position tracking
    }

    public void HideEnemy()
    {
        isActive = false;
        isWatchingCursor = false;

        // Animate the enemy scrolling down quickly
        StartCoroutine(ScrollDown());
    }

    private System.Collections.IEnumerator ScrollDown()
    {
        while (enemyRect.anchoredPosition.y > -enemyRect.rect.height)
        {
            enemyRect.anchoredPosition -= Vector2.up * moveSpeed * Time.deltaTime;
            yield return null;
        }

        // Enemy is fully hidden
        enemyRect.anchoredPosition = new Vector2(0, -enemyRect.rect.height);
    }

    private void Respawn()
    {
        if (!isActive) ShowEnemy(); // Respawn the enemy if not already active
    }

    private void TriggerJumpscare()
    {
        Debug.Log("JUMPSCARE! The player moved the mouse too much!");
        // Add your jumpscare logic here, e.g., load a jumpscare animation or sound
        HideEnemy(); // Hide the enemy after the jumpscare
        Invoke(nameof(Respawn), respawnTime); // Respawn the enemy after a delay
    }
}
