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
    public LifeSystem lifeSystem; // Reference to the life system

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

    private void UpdateTimerUI()
    {
        timerText.text = Mathf.Ceil(currentTimer).ToString();
    }

    private void StartRising()
    {
        isRising = true;
    }

    private void EnemyFullyRisen()
    {
        isRising = false;
        isWaitingToDescend = true;

        lightsOffTimer = lightsOffDuration;
    }

    private void StartDescending()
    {
        isWaitingToDescend = false;
        lightsOff = false;
        darkOverlay.SetActive(false);
    }

    private void ResetEnemy()
    {
        isWaitingToDescend = false;
        currentTimer = timeToRise;
    }

    private void ToggleLights()
    {
        lightsOff = !lightsOff;
        darkOverlay.SetActive(lightsOff);
    }

    private void TriggerJumpscare()
    {
        if (lightsOff) return;

        jumpscareActive = true;
        jumpscareImage.SetActive(true);

        if (lifeSystem != null)
        {
            lifeSystem.LoseLife(); // Deduct a life when the jumpscare happens
        }

        isRising = false;
        isWaitingToDescend = false;

        StartCoroutine(ShakeJumpscare());

        Invoke(nameof(EndJumpscare), jumpscareDuration);
    }

    private void EndJumpscare()
    {
        jumpscareActive = false;
        jumpscareImage.SetActive(false);

        if (jumpscareImage != null)
        {
            jumpscareImage.transform.localPosition = originalJumpscarePosition;
        }

        enemyRect.anchoredPosition = startPosition;
        ResetEnemy();
    }

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

            yield return new WaitForSeconds(1f / shakeSpeed);
        }
    }
}
