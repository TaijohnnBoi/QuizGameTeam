using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video; // Add this to include VideoPlayer support

public class CutsceneManager : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Assign the Video Player component in the Inspector
    public string nextSceneName;   // Set the name of the next scene in the Inspector

    void Start()
    {
        // Wait for the video to finish playing, then load the next scene
        videoPlayer.loopPointReached += OnVideoFinished;
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
