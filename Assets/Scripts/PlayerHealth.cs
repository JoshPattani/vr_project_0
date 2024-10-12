using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f; // Maximum health of the player
    public float currentHealth; // Current health of the player
    
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth; // Set the player's health to the maximum value
    }

    // Function to reduce the player's health
    public void TakeDamage(float damage)
    {
        currentHealth -= damage; // Subtract the damage value from the player's health
        // Perform actions when the player takes damage
        Debug.Log("Player took " + damage + " damage!");
        
        // Check if the player's health has reached zero
        if (currentHealth <= 0)
        {
            Die(); // Call the Die function
        }
    }

    // Function to handle the player's death
    void Die()
    {
        // Perform actions when the player dies
        Debug.Log("Player has died!");

        // go back to start scene to restart the game
        SceneManager.LoadScene("StartScene");

        // Optionally, reset the player's health to full
        currentHealth = maxHealth;
    }


}
