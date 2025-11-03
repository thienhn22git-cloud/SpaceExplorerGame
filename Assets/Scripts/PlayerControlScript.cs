using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Ship parameters")]
    [SerializeField] private float shipAcceleration = 10f;
    [SerializeField] private float shipBraking = 0.97f;
    [SerializeField] private float shipMaxVelocity = 10f;
    [SerializeField] private float shipRotationSpeed = 180f;
    [SerializeField] private float bulletSpeed = 8f;

    [Header("Object preferences")]
    [SerializeField] private Transform bulletSpawn;
    [SerializeField] private Rigidbody2D bulletPrefab;
    [SerializeField] private ParticleSystem destructionParticles;

    private Rigidbody2D shipRigidbody;
    private bool isAlive = true;
    private bool isAccelerating = false;
    private bool isDecelerating = false;

    private void Start()
    {
        // Get a reference to the attached Rigidbody2D.
        shipRigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (isAlive)
        {
            HandleShipAcceleration();
            HandleShipRotation();
            HandleShooting();
            HandleDeceleration();
        }
    }

    private void FixedUpdate()
    {
        if (isAlive && isAccelerating)
        {
            // Increase velocity upto a maximum
            shipRigidbody.AddForce(shipAcceleration * transform.up);
            shipRigidbody.linearVelocity = Vector2.ClampMagnitude(shipRigidbody.linearVelocity, shipMaxVelocity);
        }

        if (isAlive && isDecelerating)
        {
            // Apply braking by multiplying velocity by a factor less than 1
            // This creates a smooth slowdown effect.
            shipRigidbody.linearVelocity *= shipBraking;
        }
    }

    private void HandleShipAcceleration()
    {
        // Are we accelerating?
        isAccelerating = Input.GetKey(KeyCode.UpArrow);
    }

    private void HandleShipRotation()
    {
        // Ship rotation.
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(shipRotationSpeed * Time.deltaTime * transform.forward);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(-shipRotationSpeed * Time.deltaTime * transform.forward);
        }
    }

    private void HandleDeceleration()
    {
        // Are we braking?
        isDecelerating = Input.GetKey(KeyCode.DownArrow);
    }

    private void HandleShooting()
    {
        // Shooting.
        if (Input.GetKeyDown(KeyCode.Space))
        {

            Rigidbody2D bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);

            // Inherit velocity only in the forward direction of ship.
            Vector2 shipVelocity = shipRigidbody.linearVelocity;
            Vector2 shipDirection = transform.up;
            float shipForwardSpeed = Vector2.Dot(shipVelocity, shipDirection);

            // Don't want to inherit in the opposite direction, else we'll get stationary bullets.
            if (shipForwardSpeed < 0)
            {
                shipForwardSpeed = 0;
            }

            bullet.linearVelocity = shipDirection * shipForwardSpeed;

            // Add force to propel bullet in direction the player is facing.
            bullet.AddForce(bulletSpeed * transform.up, ForceMode2D.Impulse);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Asteroid")) {
            isAlive = false;

            // Get a reference to the GameManager
            GameManager gameManager = FindAnyObjectByType<GameManager>();

            // Restart game after delay.
            gameManager.GameOver();

            //Show the destroyed effect.
            Instantiate(destructionParticles, transform.position, Quaternion.identity);

            // Destroy the player.
            Destroy(gameObject);
        }
    }
}

