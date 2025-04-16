using UnityEngine;

public class GunControl : MonoBehaviour
{
    public Camera mainCamera; // Assign your main camera in the Inspector.
    public Transform bodyTransform; // Assign the Body Transform in the Inspector.
    public Transform firePoint; // Assign the Fire Point Transform in the Inspector.
    public GameObject bulletPrefab; // Assign your bullet prefab in the Inspector.
    public float bulletSpeed = 25f; // Adjust bullet speed as needed.

    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main; // Automatically find the main camera if not assigned.
        }

        if (bodyTransform == null)
        {
            Debug.LogError("Body Transform is not assigned in the Inspector!");
        }

        if (firePoint == null)
        {
            Debug.LogError("Fire Point is not assigned in the Inspector!");
        }

        if (bulletPrefab == null)
        {
            Debug.LogError("Bullet Prefab is not assigned in the Inspector!");
        }
    }

    void Update()
    {
        if (mainCamera != null && bodyTransform != null)
        {
            // Get the mouse position in world space
            Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f; // Ensure the z-coordinate is the same as the player

            // Calculate the direction from the player's body to the mouse
            Vector3 direction = mousePosition - bodyTransform.position;
            direction.Normalize(); // Normalize the direction vector

            // Calculate the angle in degrees
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

            // Rotate the player's body
            bodyTransform.rotation = Quaternion.Euler(0, 0, angle);
        }

        // Handle shooting input
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            // Instantiate the bullet at the fire point's position and rotation
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

            // Get the Rigidbody2D component of the bullet
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                // Apply velocity to the bullet in the forward direction of the fire point
                rb.linearVelocity = firePoint.right * bulletSpeed;

                // Optionally, you can destroy the bullet after some time
                // Destroy(bullet, 2f);
            }
            else
            {
                Debug.LogError("Bullet prefab does not have a Rigidbody2D component.");
            }
        }
        else
        {
            Debug.LogError("Bullet prefab or firePoint not assigned!");
        }
    }
}