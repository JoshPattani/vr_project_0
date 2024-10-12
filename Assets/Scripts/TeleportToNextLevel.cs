using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportToNextLevel : MonoBehaviour
{
    [Header("Scene to Load")]
    public string nextLevelSceneName; // Name of the scene to load (e.g., "Level2")

    [Header("Tag to Identify Player")]
    public string playerTag = "Player"; // Default tag for the player

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object that entered the trigger zone is the player
        if (other.CompareTag(playerTag))
        {
            // Load the next level scene
            SceneManager.LoadScene(nextLevelSceneName);
        }
    }
}

