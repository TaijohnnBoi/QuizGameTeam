using UnityEngine;

public class Manager : MonoBehaviour
{
    public GameObject[] Levels;

    private int currentLevel;

    // Ensure this method is correctly placed inside the class, not inside another method
    public void CorrectAnswer()
    {
        if (currentLevel + 1 != Levels.Length)
        {
            Levels[currentLevel].SetActive(false);

            currentLevel++;
            Levels[currentLevel].SetActive(true);
        }
    }

    // Example method to check if the enemy is active
    public bool IsEnemyActive()
    {
        return true; // Replace this with your actual logic
    }
}
