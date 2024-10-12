using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Teleport : MonoBehaviour
{
    [Header("Teleport Settings")]
    [SerializeField]
    private GameObject playerOrigin; // The player's GameObject to move
    [SerializeField]
    private LayerMask teleportMask; // Layer mask to identify valid teleport targets
    [SerializeField]
    private InputActionReference teleportButton; // Input action for teleporting
    // [SerializeField]
    // private GameObject teleportTarget; // Target position to teleport to
    
    void Start() {
        if (teleportButton != null)
        {
            // Subscribe to the teleport action
            teleportButton.action.performed += DoRaycast;
        }
    }

    void DoRaycast (InputAction.CallbackContext __)
    {
        
        // Debugging
        Debug.Log("Raycasting...");

        // Raycast from the player's position to detect valid teleport targets
        RaycastHit hit;
        bool didHit = Physics.Raycast(
            
            // Origin
            transform.position,

            // Direction
            transform.forward,

            // Output
            out hit,

            // Max distance
            Mathf.Infinity,

            // Layer mask
            teleportMask
        );

        // Check if the raycast hit a valid teleport target
        if (didHit)
        {
            // Debugging
            Debug.Log("TeleportZone found!");

            // Teleport the player to top of target object
            playerOrigin.transform.position = hit.collider.gameObject.transform.position + new Vector3(0, 1.5f, 0);

            // Optional: Play teleport sound effect
            // AudioSource audioSource = GetComponent<AudioSource>();
            // if (audioSource != null)
            // {
            //     audioSource.Play();
            // }

            // Optional: Play teleport visual effect
            // ParticleSystem particleSystem = GetComponent<ParticleSystem>();
            // if (particleSystem != null)
            // {
            //     particleSystem.Play();
            // }

            // Debugging
            Debug.Log("Teleported!");
        }
        else
        {
            // Debugging
            Debug.Log("No TeleportZone found.");
        }
    }

}

// void DoRaycast (InputAction.CallbackContext __) {
//     RaycastHit hit;
//     bool didHit = Physics.Raycast(

//         // Origin
//         transform.position,

//         // Direction
//         transform.forward,

//         // Output
//         out hit,

//         // Max distance
//         Mathf.Infinity,

//         // Layer mask
//         teleportMask
//     );

//     if (didHit) {
//         playerOrigin.transform.position = hit.collider.gameObject.transform.position;
//         Debug.Log("Teleported!");
//     }
// }
