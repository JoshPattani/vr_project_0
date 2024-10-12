using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timewarp : MonoBehaviour
{
    [Header("Hand Reference")]
    public GameObject rightHandReference; // Reference to the right hand controller

    // Variables to control time scaling
    public float minTimeScale = 0.1f; // Slowest time scale when moving
    public float maxTimeScale = 1.0f; // Normal time scale when stationary
    public float timeScaleMultiplier = 100.0f; // Multiplier to adjust sensitivity
    private float fixedDeltaTime;
    private Vector3 previousHandPosition;

    void Start()
    {
        // Store the initial fixed delta time
        fixedDeltaTime = Time.fixedDeltaTime;

        // Store the initial hand position
        if (rightHandReference != null)
        {
            previousHandPosition = rightHandReference.transform.position;
        }
        else
        {
            Debug.Log("Right Hand Reference is null");
        }
    }

    void Update()
    {
        if (rightHandReference != null)
        {
            Vector3 currentHandPosition = rightHandReference.transform.position;
            float distance = Vector3.Distance(previousHandPosition,currentHandPosition);
            // If player has moved the controller since the last frame
            if (distance > 0.1f)
            {
                // Debugging
                // Debug.Log("warping time");

                // Calculate the new time scale based on the movement
                float newTimeScale = Mathf.Clamp(maxTimeScale - (distance * timeScaleMultiplier), minTimeScale, maxTimeScale);

                // Update the time scale
                Time.timeScale = Mathf.Clamp(maxTimeScale - (distance * timeScaleMultiplier * Time.deltaTime), minTimeScale, maxTimeScale);

                // Update the fixed delta time
                Time.fixedDeltaTime = fixedDeltaTime * Time.timeScale;

                // Update the previous position for the next frame
                previousHandPosition = currentHandPosition;
            }
            // Return speed to normal when hand is still
            else
            {
                Time.timeScale = maxTimeScale;
                Time.fixedDeltaTime = fixedDeltaTime;

                // Debugging
                // Debug.Log("Time is normal");
            }
        }
        else
        {
            Debug.LogError("Right Hand Reference is not assigned in TimeManager.");
        }
    }

    void OnDisable()
    {
        // Reset time scale to normal when the script is disabled or the game stops
        Time.timeScale = 1.0f;
        Time.fixedDeltaTime = fixedDeltaTime;
    }
}

