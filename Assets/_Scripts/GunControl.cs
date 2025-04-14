using UnityEngine;

public class GunRotation : MonoBehaviour
{
    public Camera mainCamera; // Assign your main camera in the Inspector.
    public GameObject gunObject; // Assign your gun GameObject in the Inspector.
    public GameObject bulletPrefab; // Assign your bullet prefab in the Inspector.
    public Transform firePoint; // Assign the firePoint transform in the Inspector.
    public float bulletSpeed = 10f; // Adjust bullet speed as needed.

    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main; // Automatically find the main camera if not assigned.
        }

        if (gunObject == null)
        {
            Debug.LogError("Gun Object is not assigned in the Inspector!");
        }

        if (firePoint == null)
        {
            Debug.LogError("firePoint is not assigned in the Inspector!");
        }
    }

    void Update()
    {
        if (mainCamera != null && gunObject != null)
        {
            Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector3 direction = mousePosition - gunObject.transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            gunObject.transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        if (Input.GetMouseButtonDown(0))
        {
            GunShoot();
        }
    }

    void GunShoot()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                rb.linearVelocity = firePoint.right * bulletSpeed;
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