using UnityEngine;
using UnityEngine.UI;

public class GhostEnemy : MonoBehaviour
{
    public int clicksToDisappear = 4;    // Number of clicks required to remove the ghost
    public float respawnTime = 3f;       // Time in seconds before the ghost reappears
    private int currentClicks = 0;       // Tracks the player's clicks

    private Button ghostButton;          // Reference to the button component

    void Start()
    {
        ghostButton = GetComponent<Button>();
        ghostButton.onClick.AddListener(OnGhostClicked); // Add click listener
        Respawn(); // Make sure the ghost is active at the start
    }

    void OnGhostClicked()
    {
        currentClicks++;

        if (currentClicks >= clicksToDisappear)
        {
            HideGhost();
            Invoke(nameof(Respawn), respawnTime); // Respawn the ghost after a delay
        }
    }

    void HideGhost()
    {
        gameObject.SetActive(false); // Hide the ghost
    }

    void Respawn()
    {
        currentClicks = 0;            // Reset the click count
        gameObject.SetActive(true);  // Show the ghost
    }
}
