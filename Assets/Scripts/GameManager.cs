using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Game States")]
    public bool isGameActive = false;
    public bool isGameOver = false;

    [Header("UI References")]
    public GameObject startScreen;
    public GameObject gameOverScreen;
    public SpriteScoreDisplay spriteScoreDisplay;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip flapSound;
    public AudioClip scoreSound;
    public AudioClip crashSound;

    public int score = 0;

    private void Awake()
    {
        if (instance == null) instance = this;

        audioSource = GetComponent<AudioSource>();

        Time.timeScale = 0f;
        startScreen.SetActive(true);
        gameOverScreen.SetActive(false);
    }

    public void StartGame()
    {
        isGameActive = true;
        Time.timeScale = 1f;
        startScreen.SetActive(false);
    }

    // --- NEW AUDIO METHOD ---
    public void PlayFlap()
    {
        if (audioSource != null && flapSound != null)
        {
            audioSource.PlayOneShot(flapSound);
        }
    }

    public void AddScore()
    {
        if (!isGameOver && isGameActive)
        {
            score++;
            spriteScoreDisplay.UpdateScore(score);

            // --- NEW: Play point sound ---
            if (audioSource != null && scoreSound != null) audioSource.PlayOneShot(scoreSound);
        }
    }

    public void GameOver()
    {
        // Prevent the crash sound from playing multiple times if the bird bounces
        if (!isGameOver)
        {
            // --- NEW: Play crash sound ---
            if (audioSource != null && crashSound != null) audioSource.PlayOneShot(crashSound);
        }

        isGameOver = true;
        isGameActive = false;
        Time.timeScale = 0f;
        gameOverScreen.SetActive(true);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}