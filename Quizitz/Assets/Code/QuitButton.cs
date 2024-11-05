using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitButton : MonoBehaviour
{
    // This function will be called when the button is clicked
    public void QuitGame()
    {
#if UNITY_EDITOR
            // If we're running in the Unity editor, stop playing
            UnityEditor.EditorApplication.isPlaying = false;
#else
        // If we're in a built game, quit the application
        Application.Quit();
#endif
    }
}

