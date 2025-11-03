using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro; // Use TextMeshPro

public class GameManager : MonoBehaviour
{
    [Header("Game Objects")]
    [SerializeField] private Asteroid asteroidPrefab;
    [SerializeField] private Star starPrefab; // The Star prefab you created

    [Header("Game State")]
    public int asteroidCount = 0;
    private int level = 0;
    private int score = 0;
    private bool isGameOver = false;

    [Header("UI Elements")]
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private GameObject gameOverPanel; // The entire "Game Over" screen
    [SerializeField] private TMP_Text finalScoreText; // The text on the Game Over screen

    void Start()
    {
        // Make sure Game Over screen is hidden at start
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }

        // Initialize UI
        isGameOver = false;
        score = 0;
        level = 1; // This ensures we always start at level 1
        UpdateScoreUI();
        UpdateLevelUI();
        
        // Spawn asteroids and stars for the first level
        SpawnAsteroidsForLevel();
    }

    private void Update()
    {
        // Don't do anything if the game is over
        if (isGameOver)
        {
            return;
        }

        // If there are no asteroids left, go to the next level
        if (asteroidCount == 0)
        {
            // Increase the level.
            level++;
            UpdateLevelUI();
            
            // Spawn the correct number for this level.
            SpawnAsteroidsForLevel();
        }
    }

    private void SpawnAsteroidsForLevel()
    {
        // Clear out any stars left from the previous level
        ClearOldCollectibles();

        // l=1, n=4; l=2, n=6; l=3, n=8...
        int numAsteroids = (2 * level) + 2;
        for (int i = 0; i < numAsteroids; i++)
        {
            SpawnAsteroid();
        }

        // Spawn 1-3 stars in random locations
        int starCount = Random.Range(1, 4); // Gives 1, 2, or 3
        for (int i = 0; i < starCount; i++)
        {
            SpawnStar();
        }
    }

    // Finds and destroys all remaining stars
    private void ClearOldCollectibles()
    {
        // Find all GameObjects with the "Star" tag
        GameObject[] oldStars = GameObject.FindGameObjectsWithTag("Star");
        foreach (GameObject star in oldStars)
        {
            Destroy(star);
        }
    }

    private void SpawnAsteroid()
    {
        // How far along the edge.
        float offset = Random.Range(0f, 1f);
        Vector2 viewportSpawnPosition = Vector2.zero;

        // Which edge.
        int edge = Random.Range(0, 4);
        if (edge == 0) viewportSpawnPosition = new Vector2(offset, 0);
        else if (edge == 1) viewportSpawnPosition = new Vector2(offset, 1);
        else if (edge == 2) viewportSpawnPosition = new Vector2(0, offset);
        else if (edge == 3) viewportSpawnPosition = new Vector2(1, offset);

        // Create the asteroid.
        Vector2 worldSpawnPosition = Camera.main.ViewportToWorldPoint(viewportSpawnPosition);
        Asteroid asteroid = Instantiate(asteroidPrefab, worldSpawnPosition, Quaternion.identity);
        asteroid.gameManager = this;
    }

    // Spawns a star at a random point *inside* the screen
    private void SpawnStar()
    {
        // Use 0.1 to 0.9 to avoid spawning right on the edge
        Vector2 viewportSpawnPosition = new Vector2(Random.Range(0.1f, 0.9f), Random.Range(0.1f, 0.9f));
        
        Vector2 worldSpawnPosition = Camera.main.ViewportToWorldPoint( viewportSpawnPosition);
        
        // Ensure starPrefab is assigned in the Inspector
        if (starPrefab != null)
        {
            Star star = Instantiate(starPrefab, worldSpawnPosition, Quaternion.identity);
            star.gameManager = this; // Give the star a reference to this manager
        }
        else
        {
            Debug.LogError("Star Prefab is not assigned in the GameManager Inspector!");
        }
    }

    public void AddScore(int points)
    {
        if (isGameOver) return; // Don't add score if game is already over
        score += points;
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    void UpdateLevelUI()
    {
        if (levelText != null)
        {
            levelText.text = "Level: " + level;
        }
    }

    public void GameOver()
    {
        if (isGameOver) return; // Prevent this from running multiple times

        isGameOver = true;
        Debug.Log("Game Over");

        // Show the Game Over panel
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        // Update the final score text
        if (finalScoreText != null)
        {
            finalScoreText.text = "Final Score: " + score;
        }
    }

    // This function is called by the "Restart" button on the UI
    public void RestartGame()
    {
        // Reload the current scene to restart
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu"); 
    }
}

