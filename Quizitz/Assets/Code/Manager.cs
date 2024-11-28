using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public GameObject[] Levels;

    int currentLevel;

    public void correctAnswer()
    {
        if(currentLevel + 1 != Levels.Length)
        {
            Levels[currentLevel].SetActive(false);

            currentLevel++;
            Levels[currentLevel].SetActive(true);
        }
    }
}
