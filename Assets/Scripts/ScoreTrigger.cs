using UnityEngine;

public class ScoreTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Ensure only the bird triggers the score (prevents pipes from scoring themselves)
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.instance.AddScore();
        }
    }
}