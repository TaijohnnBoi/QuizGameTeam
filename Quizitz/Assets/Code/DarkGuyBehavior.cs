using UnityEngine;
using TMPro;

public class DarkGuyBehavior : MonoBehaviour
{
    // Timer variables
    public float timeToRise = 10f; // Total time before the enemy rises
    private float currentTimer;
    private float lightsOffDuration = 3f; // Time to keep lights off after rising
    private float lightsOffTimer;

    // References
    public RectTransform enemyRect; // RectTransform for enemy movement
    public TextMeshProUGUI timerText; // Timer display
    public GameObject darkOverlay; // Black overlay for "lights off" effect
    public GameObject jumpscareImage; // Jumpscare UI element
    public float jumpscareDuration = 2f; // How long the jumpscare lasts

    // Shake effect variables
    public float shakeIntensity = 20f; // How far the image shakes
    public float shakeSpeed = 50f; // How fast the image shakes

    private Vector3 originalJumpscarePosition; // Original position of the jumpscare image

    // State variables
    private bool isRising = false; // Is the enemy rising?
    private bool lightsOff = false; // Are the lights off?
    private bool isWaitingToDescend = false; // Is the enemy waiting to go back down?
    private bool jumpscareActive = false; // Is the jumpscare currently active?

    // Positions
    private Vector2 startPosition = new Vector2(0, -200); // Off-screen
    private Vector2 risePosition = new Vector2(0, 0); // On-screen
    public float riseSpeed = 300f; // Speed of rising animation

    void Start()
    {
        // Initialize timers
        currentTimer = timeToRise;
        lightsOffTimer = lightsOffDuration;

        UpdateTimerUI();

        // Set enemy to starting position
        enemyRect.anchoredPosition = startPosition;

        // Ensure the overlay and jumpscare are hidden
        darkOverlay.SetActive(false);
        jumpscareImage.SetActive(false);

        // Store the original position of the jumpscare image
        if (jumpscareImage != null)
        {
            originalJumpscarePosition = jumpscareImage.transform.localPosition;
        }
    }

    void Update()
    {
        // Handle player input to toggle lights
        if (Input.GetKeyDown(KeyCode.S))
        {
            ToggleLights();
        }

        // If the lights are on and the enemy isn't rising, update the timer
        if (!isRising && !isWaitingToDescend && !jumpscareActive)
        {
            currentTimer -= Time.deltaTime;
            UpdateTimerUI();

            // Check if it's time for the enemy to rise
            if (currentTimer <= 0)
            {
                StartRising();
            }
        }

        // Handle rising animation
        if (isRising)
        {
            enemyRect.anchoredPosition = Vector2.MoveTowards(enemyRect.anchoredPosition, risePosition, riseSpeed * Time.deltaTime);

            // Check if the enemy has fully risen
            if (Vector2.Distance(enemyRect.anchoredPosition, risePosition) < 0.1f)
            {
                if (lightsOff)
                {
                    EnemyFullyRisen();
                }
                else
                {
                    TriggerJumpscare(); // Lights are on; trigger the jumpscare
                }
            }
        }

        // Handle lights-off countdown when enemy is waiting to descend
        if (lightsOff && isWaitingToDescend)
        {
            lightsOffTimer -= Time.deltaTime;

            if (lightsOffTimer <= 0)
            {
                StartDescending();
            }
        }

        // Handle descending animation
        if (!isRising && isWaitingToDescend && !lightsOff && !jumpscareActive)
        {
            enemyRect.anchoredPosition = Vector2.MoveTowards(enemyRect.anchoredPosition, startPosition, riseSpeed * Time.deltaTime);

            // Check if the enemy has fully descended
            if (Vector2.Distance(enemyRect.anchoredPosition, startPosition) < 0.1f)
            {
                ResetEnemy();
            }
        }
    }

    // Update the timer display
    private void UpdateTimerUI()
    {
        timerText.text = Mathf.Ceil(currentTimer).ToString();
    }

    // Start the rising animation
    private void StartRising()
    {
        isRising = true;
    }

    // Called when the enemy finishes rising and the lights are off
    private void EnemyFullyRisen()
    {
        isRising = false;
        isWaitingToDescend = true;

        // Reset lights-off timer
        lightsOffTimer = lightsOffDuration;
    }

    // Start descending after lights-off duration
    private void StartDescending()
    {
        isWaitingToDescend = false;
        lightsOff = false; // Automatically turn lights back on
        darkOverlay.SetActive(false);
    }

    // Reset the enemy to its initial state
    private void ResetEnemy()
    {
        isWaitingToDescend = false;
        currentTimer = timeToRise;
    }

    // Handle toggling lights on/off
    private void ToggleLights()
    {
        lightsOff = !lightsOff;
        darkOverlay.SetActive(lightsOff);
    }

    // Trigger the jumpscare
    private void TriggerJumpscare()
    {
        // Ensure jumpscare only happens if the lights are on
        if (lightsOff) return;

        jumpscareActive = true;
        jumpscareImage.SetActive(true);

        // Stop all other actions temporarily
        isRising = false;
        isWaitingToDescend = false;

        // Start shaking effect
        StartCoroutine(ShakeJumpscare());

        // Wait for jumpscare duration, then reset
        Invoke(nameof(EndJumpscare), jumpscareDuration);
    }

    // End the jumpscare and reset the game state
    private void EndJumpscare()
    {
        jumpscareActive = false;
        jumpscareImage.SetActive(false);

        // Reset jumpscare image position
        if (jumpscareImage != null)
        {
            jumpscareImage.transform.localPosition = originalJumpscarePosition;
        }

        // Reset enemy position
        enemyRect.anchoredPosition = startPosition;
        ResetEnemy();
    }

    // Shake effect coroutine
    private System.Collections.IEnumerator ShakeJumpscare()
    {
        float elapsedTime = 0;

        while (elapsedTime < jumpscareDuration)
        {
            Vector3 randomOffset = new Vector3(
                Random.Range(-shakeIntensity, shakeIntensity),
                Random.Range(-shakeIntensity, shakeIntensity),
                0
            );

            jumpscareImage.transform.localPosition = originalJumpscarePosition + randomOffset;

            elapsedTime += Time.deltaTime;

            yield return new WaitForSeconds(1f / shakeSpeed); // Control shake speed
        }
    }
}
