using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scroller : MonoBehaviour
{
    [SerializeField] private RawImage _img;
    [SerializeField] private float _x, _y;

    void Update()
    {
        // Get the current UV position
        Vector2 uvPosition = _img.uvRect.position;

        // Update the UV position
        uvPosition += new Vector2(_x, _y) * Time.deltaTime;

        // Wrap the UV position so that it stays within the bounds of 0 and 1
        uvPosition.x = Mathf.Repeat(uvPosition.x, 1f); // Wrap horizontally
        uvPosition.y = Mathf.Repeat(uvPosition.y, 1f); // Wrap vertically

        // Set the new UV coordinates, keeping the same size
        _img.uvRect = new Rect(uvPosition, _img.uvRect.size);
    }
}
