using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SimonSaysEnemy : MonoBehaviour
{
    public Button pinkButton;            // Reference to the Pink button
    public Button greenButton;           // Reference to the Green button
    public Button blueButton;            // Reference to the Blue button
    public TextMeshProUGUI textBubble;   // Text bubble to display the sequence
    public float respawnTime = 5f;       // Time before the enemy reappears

    private string[] colors = { "Pink", "Green", "Blue" };  // Available colors
    private string[] sequence;           // Generated sequence
    private int sequenceLength = 3;      // Number of colors in the sequence
    private int playerIndex = 0;         // Tracks player's current progress in the sequence
    private bool active = false;         // Indicates if the enemy is active

    void Start()
    {
        // Initialize buttons
        pinkButton.onClick.AddListener(() => CheckAnswer("Pink"));
        greenButton.onClick.AddListener(() => CheckAnswer("Green"));
        blueButton.onClick.AddListener(() => CheckAnswer("Blue"));

        HideEnemy(); // Ensure the enemy is hidden at the start
    }

    public void ActivateEnemy()
    {
        GenerateSequence(); // Generate a new sequence
        DisplaySequence();  // Display the sequence to the player
        playerIndex = 0;    // Reset player progress
        active = true;
        gameObject.SetActive(true); // Show the enemy
    }

    private void GenerateSequence()
    {
        sequence = new string[sequenceLength];
        for (int i = 0; i < sequenceLength; i++)
        {
            sequence[i] = colors[Random.Range(0, colors.Length)];
        }
    }

    private void DisplaySequence()
    {
        textBubble.text = string.Join(" ", sequence); // Display the sequence in the text bubble
    }

    private void CheckAnswer(string color)
    {
        if (!active) return;

        if (sequence[playerIndex] == color)
        {
            playerIndex++;
            if (playerIndex >= sequence.Length)
            {
                // Player completed the sequence
                EnemyDefeated();
            }
        }
        else
        {
            // Player clicked the wrong color, optionally handle this (e.g., lose a life)
            Debug.Log("Wrong Color!");
        }
    }

    private void EnemyDefeated()
    {
        active = false;
        Debug.Log("Enemy defeated!");
        HideEnemy();

        // Respawn the enemy after a delay
        Invoke(nameof(ActivateEnemy), respawnTime);
    }

    private void HideEnemy()
    {
        textBubble.text = ""; // Clear the text bubble
        gameObject.SetActive(false); // Hide the enemy
    }
}
