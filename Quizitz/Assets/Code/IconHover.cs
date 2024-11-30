using UnityEngine;
using UnityEngine.UI;

public class IconHover : MonoBehaviour
{
    public GameObject hoverText; // Assign the corresponding text object in the inspector

    void Start()
    {
        hoverText.SetActive(false); // Ensure the text is hidden at the start
    }

    public void OnMouseEnter()
    {
        hoverText.SetActive(true); // Show the text when the mouse hovers
    }

    public void OnMouseExit()
    {
        hoverText.SetActive(false); // Hide the text when the mouse stops hovering
    }
}
