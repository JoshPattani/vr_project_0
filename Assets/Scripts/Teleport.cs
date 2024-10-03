using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Teleport : MonoBehaviour
{
    [SerializeField]
    private GameObject playerOrigin;
    [SerializeField]
    private LayerMask teleportMask;
    [SerializeField]
    private InputActionReference teleportButton;
    
    void Start() {
        teleportButton.action.performed += DoRaycast;
    }

    void DoRaycast (InputAction.CallbackContext __) {
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

        if (didHit) {
            playerOrigin.transform.position = hit.collider.gameObject.transform.position;
            Debug.Log("Teleported!");
        }
    }
}
