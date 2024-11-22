using UnityEngine;

public class FallingStars : MonoBehaviour
{
    [SerializeField] private GameObject starPrefab; // Assign your star prefab here
    [SerializeField] private RectTransform canvasRect; // The RectTransform of your Canvas
    [SerializeField] private float spawnInterval = 0.5f; // Time between spawning new stars
    [SerializeField] private float fallSpeed = 100f; // Speed of falling stars
    [SerializeField] private Vector2 starSizeRange = new Vector2(10, 50); // Random size range for stars

    private void Start()
    {
        // Start spawning stars
        InvokeRepeating(nameof(SpawnStar), 0f, spawnInterval);
    }

    private void SpawnStar()
    {
        // Create a new star
        GameObject star = Instantiate(starPrefab, canvasRect);

        // Randomize the initial position
        Vector2 randomPosition = new Vector2(
            Random.Range(0, canvasRect.rect.width),
            canvasRect.rect.height
        );

        // Randomize the size
        float randomSize = Random.Range(starSizeRange.x, starSizeRange.y);
        star.GetComponent<RectTransform>().sizeDelta = new Vector2(randomSize, randomSize);

        // Set the star's position
        star.GetComponent<RectTransform>().anchoredPosition = randomPosition;

        // Start moving the star downward
        StartCoroutine(MoveStar(star));
    }

    private System.Collections.IEnumerator MoveStar(GameObject star)
    {
        RectTransform starRect = star.GetComponent<RectTransform>();

        while (starRect.anchoredPosition.y > -starRect.sizeDelta.y)
        {
            // Move the star downward
            starRect.anchoredPosition += Vector2.down * fallSpeed * Time.deltaTime;
            yield return null;
        }

        // Destroy the star when it goes off-screen
        Destroy(star);
    }
}
