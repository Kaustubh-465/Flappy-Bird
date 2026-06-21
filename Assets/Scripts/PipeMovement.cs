using UnityEngine;

public class PipeMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private float deadZone; // Calculated dynamically

    private void Start()
    {
        // Dynamically calculate the left edge of the screen
        Camera cam = Camera.main;
        float cameraHeight = 2f * cam.orthographicSize;
        float cameraWidth = cameraHeight * cam.aspect;

        // Set the dead zone to the left edge, minus a 2-unit buffer
        deadZone = -(cameraWidth / 2f) - 2f;
    }

    private void Update()
    {
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;

        if (transform.position.x < deadZone)
        {
            Destroy(gameObject);
        }
    }
}