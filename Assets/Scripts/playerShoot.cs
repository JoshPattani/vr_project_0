using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    public GameObject BulletTemplate;
    public float shootPower = 550f;
    // audio source
    public AudioClip explosionSFX;

    public InputActionReference trigger;
    // Start is called before the first frame update
    void Start()
    {
        trigger.action.performed += Shoot;
    }

    void Shoot(InputAction.CallbackContext __) {
        GameObject newBullet = Instantiate(BulletTemplate, transform.position, transform.rotation);
        newBullet.GetComponent<Rigidbody>().AddForce(transform.forward * shootPower);
        GetComponent<AudioSource>().PlayOneShot(explosionSFX);
    }
}