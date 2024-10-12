using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baddie : MonoBehaviour
{
    [SerializeField]
    private float shootPower = 1000f;
    [SerializeField]
    private float shootInterval = 3.5f;
    [SerializeField] 
    private float bulletDamage = 10f; // Amount of damage per bullet
    private float lastShootTime = 0f;
    public GameObject enemyBulletTemplate;
    private GameObject playerTarget;

    void Update()
    {
        // Random angle rotation
        // transform.Rotate(Vector3.up, 5f);

        // Start moving
        // transform.position += 2 * Time.deltaTime * transform.forward;

        // Find the player
        playerTarget = GameObject.FindGameObjectWithTag("Player");

        // Shoot at the player
        if (playerTarget != null) {
            // stop movement
            // transform.position += 0 * Time.deltaTime * transform.forward;
            
            // look at the player when player enters area
            transform.LookAt(playerTarget.transform.position);

            // check interval timer
            if (Time.time - lastShootTime >= shootInterval) {
                // stop moving
                Shoot();
                // reset timer
                lastShootTime = Time.time;
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
        Debug.Log("Collision detected with: " + collision.gameObject.name);
        // Check if the enemy collided with a projectile
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Debug.Log("Bullet collision detected");
            // Destroy the enemy
            Destroy(gameObject);
            // Destroy the projectile
            Destroy(collision.gameObject);
            // Destroy enemy object child
            //Destroy(gameObject.transform.GetChild(0).gameObject);
        }
    }

    void Shoot() {
        GameObject newBullet = Instantiate(enemyBulletTemplate, transform.position, transform.rotation);
        // Change bullet origin
        newBullet.transform.position = transform.position + transform.forward * 2;

        // Assign bullet damage to the bullet script
        EnemyBullet bulletScript = newBullet.GetComponent<EnemyBullet>();
        if (bulletScript != null)
        {
            bulletScript.SetDamage(bulletDamage);
        }

        newBullet.GetComponent<Rigidbody>().AddForce(transform.forward * shootPower);
    }
}
