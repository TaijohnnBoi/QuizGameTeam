using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonHoverAudio : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public AudioClip hoverSound; // Drag your audio clip here in the Inspector
    private AudioSource audioSource;

    void Start()
    {
        // Get the AudioSource component on the button (or add it if it doesn't exist)
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Play the audio when the mouse enters the button, but only if it's not already playing
        if (!audioSource.isPlaying)
        {
            audioSource.clip = hoverSound;
            audioSource.Play();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Optional: You could stop the sound when the mouse leaves, if you want to immediately stop the sound
        // audioSource.Stop();
    }
}
