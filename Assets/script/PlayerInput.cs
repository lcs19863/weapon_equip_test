using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour
{
    // Units per second
    [SerializeField]
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    [SerializeField]
    float mSpeed = 5.0f;

    // Degrees per second
    [SerializeField]
    float mAngularSpeed = 180.0f;

    // Idle timer variables
    [SerializeField]
    float mIdleTime = 2.0f;
    float mTimer = 0.0f;

    Vector3 mDefaultScale;

    void Start ()
    {
        // Keep a backup of the original scale
        mDefaultScale = transform.localScale;
    }

    void Update ()
    {
        MoveObject ();
    }

    private void MoveObject()
    {
        // Obtain input information (See "Horizontal" and "Vertical" in the Input Manager)
        float horizontal = Input.GetAxis ("Horizontal");
        float vertical = Input.GetAxis ("Vertical");
        float fire = Input.GetAxis("Fire");

        // Check if there is movement
        if (!Mathf.Approximately(vertical, 0.0f) || !Mathf.Approximately(horizontal, 0.0f))
        {
            Vector3 direction = new Vector3(horizontal, 0.0f, vertical);

            // Cap the magnitude of direction vector
            direction = Vector3.ClampMagnitude(direction, 1.0f);

            // Translate the game object in world space
            transform.Translate (direction * mSpeed * Time.deltaTime, Space.World);

            // Rotate the game object
            //transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction), mAngularSpeed * Time.deltaTime);

            // Reset idle timer to zero
            mTimer = 0.0f;
            transform.localScale = mDefaultScale;
        }
        if (fire == 1)
        {
            Fire();
        }
    }

    void Fire()
    {
        // Create the Bullet from the Bullet Prefab
        var bullet = (GameObject)Instantiate(
            bulletPrefab,
            bulletSpawn.position,
            bulletSpawn.rotation);

        // Add velocity to the bullet
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 6;

        // Destroy the bullet after 2 seconds
        Destroy(bullet, 2.0f);
    }

}
