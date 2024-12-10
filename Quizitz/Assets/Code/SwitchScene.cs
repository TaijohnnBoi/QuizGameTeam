using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    public void SwitchScenes(string sceneName)
    {
        Time.timeScale = 1; // Ensure the game is running at normal speed
        SceneManager.LoadScene(sceneName);
    }
}
