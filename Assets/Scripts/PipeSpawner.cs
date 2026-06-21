using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    public GameObject pipePrefab;
    public float spawnRate = 2.0f; // Slightly faster for narrower screens
    private float timer = 0f;

    [Header("Positioning")]
    public float heightOffset = 4.5f; // Increased to utilize the taller 9:16 screen

    private float spawnX; // We will calculate this dynamically

    private void Start()
    {
        // Dynamically calculate the right edge of the screen
        Camera cam = Camera.main;
        float cameraHeight = 2f * cam.orthographicSize;
        float cameraWidth = cameraHeight * cam.aspect;

        // Set spawn position to exactly the right edge of the screen, plus a 2-unit buffer
        spawnX = (cameraWidth / 2f) + 2f;

        SpawnPipe();
    }

    private void Update()
    {
        if (timer < spawnRate)
        {
            timer += Time.deltaTime;
        }
        else
        {
            SpawnPipe();
            timer = 0f;
        }
    }

    [Header("Y-Axis Limits")]
    // The exact centers you provided
    public float topCeilingCenter = 1.486f;
    public float bottomGroundCenter = -1.102f;

    // The buffer zone. Increase this number if the gap is still spawning too close to the ground/ceiling.
    public float padding = 0.69f;

    private void SpawnPipe()
    {
        // Calculate the true safe zone by pushing the limits inward by the padding amount
        float lowestPoint = bottomGroundCenter + padding;
        float highestPoint = topCeilingCenter - padding;

        // Generate a random Y coordinate strictly within the new safe zone
        float randomY = Random.Range(lowestPoint, highestPoint);

        // Spawn at the dynamically calculated X position
        Vector3 spawnPosition = new Vector3(spawnX, randomY, 0);

        Instantiate(pipePrefab, spawnPosition, transform.rotation);
    }
}