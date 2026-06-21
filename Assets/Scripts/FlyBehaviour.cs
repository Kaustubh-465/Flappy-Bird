using UnityEngine;
using UnityEngine.InputSystem;

public class FlyBehaviour : MonoBehaviour
{
    [SerializeField] private float _velocity = 1.5f;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // 1. Safely check for New Input System clicks/presses
        bool mouseClicked = UnityEngine.InputSystem.Mouse.current != null && UnityEngine.InputSystem.Mouse.current.leftButton.wasPressedThisFrame;
        bool spacebarPressed = UnityEngine.InputSystem.Keyboard.current != null && UnityEngine.InputSystem.Keyboard.current.spaceKey.wasPressedThisFrame;

        if (mouseClicked || spacebarPressed)
        {
            // 2. If the game is sitting at the Start Menu, start it!
            if (!GameManager.instance.isGameActive && !GameManager.instance.isGameOver)
            {
                GameManager.instance.StartGame();
            }

            // 3. Only allow the bird to jump if we haven't crashed
            if (!GameManager.instance.isGameOver)
            {
                rb.linearVelocity = Vector2.up * _velocity; // Note: Ensure '_velocity' matches your jump variable
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Instantly call the global Game Over method
        GameManager.instance.GameOver();
    }
}