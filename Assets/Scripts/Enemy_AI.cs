using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AI : MonoBehaviour
{
    [SerializeField]
     private float move_speed = 10f;
     private GameObject playerTarget;

    void Update()
    {
        // Find the player
        if (playerTarget != null) {
            // move towards player when player enters area
            transform.LookAt(playerTarget.transform.position);
            transform.position += transform.forward * move_speed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            // other is set in unity via tags
            playerTarget = other.gameObject;
        }
    }
}
