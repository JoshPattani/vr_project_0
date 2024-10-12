using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private float damage;

    public void SetDamage(float damageAmount)
    {
        damage = damageAmount;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the bullet hits the player
        if (collision.gameObject.CompareTag("Player"))
        {
            // Get the player's health component and apply damage
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);

                // Debugging
                Debug.Log("Player hit by bullet. Player health: " + playerHealth.currentHealth);
            }

            // Destroy the bullet after it hits
            Destroy(gameObject);
        }
        else
        {
            // Optionally, destroy the bullet if it hits other objects
            Destroy(gameObject, 5f); // Bullet is destroyed after 5 seconds if it doesn't hit the player
        }
    }
}
