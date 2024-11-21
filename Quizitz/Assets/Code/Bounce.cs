using UnityEngine;

public class BouncingCircles : MonoBehaviour
{
    [SerializeField] private GameObject circlePrefab; // The circle prefab to instantiate
    [SerializeField] private int numberOfCircles = 5; // Number of circles to create
    [SerializeField] private float bounceHeight = 50f; // Maximum height the circle moves up/down
    [SerializeField] private float bounceSpeed = 1f; // Speed of bouncing motion
    [SerializeField] private float spawnInterval = 0.5f; // Time between spawns
    [SerializeField] private Vector2 spawnAreaMin = new Vector2(-200f, 0f); // Min spawn position
    [SerializeField] private Vector2 spawnAreaMax = new Vector2(200f, 300f); // Max spawn position

    private void Start()
    {
        // Start creating circles at intervals
        for (int i = 0; i < numberOfCircles; i++)
        {
            // Spawn circle at a random position within the given range
            Vector2 spawnPosition = new Vector2(Random.Range(spawnAreaMin.x, spawnAreaMax.x), spawnAreaMin.y);
            GameObject circle = Instantiate(circlePrefab, spawnPosition, Quaternion.identity);
            circle.transform.SetParent(transform, false); // Make the circle a child of this object
            circle.AddComponent<Bounce>(); // Add the Bounce script to control movement
        }
    }
}

public class Bounce : MonoBehaviour
{
    private float startY;
    private RectTransform rectTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        startY = rectTransform.anchoredPosition.y; // Store the starting Y position
    }

    private void Update()
    {
        // Make the object bounce using Mathf.Sin for smooth up and down motion
        float newY = startY + Mathf.Sin(Time.time * Mathf.PI * 2f * 1f) * 50f; // Adjust bounce height as needed
        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, newY);
    }
}
