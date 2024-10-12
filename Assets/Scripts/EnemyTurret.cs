using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurret : MonoBehaviour
{
    /*
    **Setup in Unity**:
        - Create a GameObject for the turret and attach this script to it.
        - Create a `Transform` object (like an empty GameObject) as the `firePoint` where the projectiles will spawn. 
        - Create a projectile prefab (e.g., a simple sphere) and assign it to the `projectilePrefab` field in the inspector.
        - Set the `fireRate`, `rotationSpeed`, and `detectionRange` according to your game's requirements.

    2. **Projectile Script**:
        - You may need a separate script for the projectile to define its movement and behavior after being fired.

    This script provides a basic turret behavior. You can extend it with features like health, damage, and different firing patterns as needed!
    */

    [Header("Turret Settings")]
    public float rotationSpeed = 5f; // Speed at which the turret rotates
    public float detectionRange = 10f; // Range within which the turret can detect the player
    public Transform firePoint; // Where the projectile will be instantiated
    public GameObject projectilePrefab; // The projectile prefab to be fired
    public float fireRate = 1f; // How often the turret can fire (in seconds)
    
    private Transform player; // Reference to the player's transform
    private float nextFireTime = 0f; // Time until the next shot can be fired

    // Start is called before the first frame update
    void Start()
    {
        // Find the player GameObject (assuming it has the tag "Player")
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (player == null)
        {
            Debug.LogWarning("Player not found! Ensure the player has the 'Player' tag.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            // Check if the player is within detection range
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            if (distanceToPlayer <= detectionRange)
            {
                // Rotate turret towards the player
                RotateTowardsPlayer();
                
                // Attempt to fire at the player
                FireAtPlayer();
            }
        }
    }

    private void RotateTowardsPlayer()
    {
        // Calculate the direction to the player
        Vector3 directionToPlayer = (player.position - transform.position).normalized;

        // Calculate rotation step
        Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
        Vector3 rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed).eulerAngles;
        transform.rotation = Quaternion.Euler(0, rotation.y, 0);
    }

    private void FireAtPlayer()
    {
        // Check if enough time has passed since the last shot
        if (Time.time >= nextFireTime)
        {
            // Instantiate the projectile at the fire point
            Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

            // Update the next fire time
            nextFireTime = Time.time + 1f / fireRate;
        }
    }
}