using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogOnEvent : MonoBehaviour
{
    // Attach events to debug for raycasting and collision detections
    private void OnCollisionEnter(Collision other) {
        Debug.Log("Collision Enter: " + other.gameObject.name);
    }

    private void OnCollisionExit(Collision other) {
        Debug.Log("Collision Exit: " + other.gameObject.name);
    }

    private void OnCollisionStay(Collision other) {
        Debug.Log("Collision Stay: " + other.gameObject.name);
    }

    private void OnTriggerEnter(Collider other) {  
        Debug.Log("Trigger Enter: " + other.gameObject.name);
    }

    private void OnTriggerExit(Collider other) {
        Debug.Log("Trigger Exit: " + other.gameObject.name);
    }

    private void OnTriggerStay(Collider other) {
        Debug.Log("Trigger Stay: " + other.gameObject.name);
    }
}
