using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baddie : MonoBehaviour
{
    [SerializeField]
    private float shootPower = 600f;
    [SerializeField]
    private float shootInterval = 2f;
    public GameObject enemyBulletTemplate;
    private GameObject playerTarget;

    void Update()
    {
        // Random angle rotation
        transform.Rotate(Vector3.up, 5f);

        // Start moving
        // transform.position += 2 * Time.deltaTime * transform.forward;

        // Find the player
        playerTarget = GameObject.FindGameObjectWithTag("Player");

        // Shoot at the player
        if (playerTarget != null) {
            // stop movement
            transform.position += 0 * Time.deltaTime * transform.forward;
            
            // look at the player when player enters area
            transform.LookAt(playerTarget.transform.position);

            // check interval timer
            if (Time.time % shootInterval < 0.1) {
                // stop moving
                Shoot();
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            // other is set in unity via tags
            playerTarget = other.gameObject;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the target collided with a projectile
        if (collision.gameObject.CompareTag("Bullet"))
        {
            // Destroy enemy object child
            Destroy(gameObject.transform.GetChild(0).gameObject);

            // Destroy the target
            Destroy(gameObject);

            // Destroy enemy projectiles
            GameObject[] enemyBullets = GameObject.FindGameObjectsWithTag("EnemyBullet");

            // Destroy the projectile as well
            Destroy(collision.gameObject);
        }
    }

    void Shoot() {
        GameObject newBullet = Instantiate(enemyBulletTemplate, transform.position, transform.rotation);
        // Change bullet origin
        newBullet.transform.position = transform.position + transform.forward * 2;

        newBullet.GetComponent<Rigidbody>().AddForce(transform.forward * shootPower);
    }
}
