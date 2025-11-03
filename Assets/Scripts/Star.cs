using UnityEngine;

public class Star : MonoBehaviour
{
    // You can drag a particle effect prefab here in the Inspector
    [SerializeField] private ParticleSystem collectionParticles;
    
    // This will be set by the GameManager when it's spawned
    public GameManager gameManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the object that entered the trigger is the Player
        if (collision.CompareTag("Player"))
        {
            // If we have a GameManager, tell it to add score
            if (gameManager != null)
            {
                gameManager.AddScore(10);
            }

            // If we have a particle effect, spawn it
            if (collectionParticles != null)
            {
                Instantiate(collectionParticles, transform.position, Quaternion.identity);
            }

            // Destroy the star object
            Destroy(gameObject);
        }
    }
}

