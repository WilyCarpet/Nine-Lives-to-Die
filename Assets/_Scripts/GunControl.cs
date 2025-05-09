using TMPro;
using UnityEngine;
using System.Collections;

public class GunControl : MonoBehaviour
{
    public Camera mainCamera; // Assign your main camera in the Inspector.
    public Transform bodyTransform; // Assign the Body Transform in the Inspector.
    public Transform firePoint; // Assign the Fire Point Transform in the Inspector.
    public GameObject bulletPrefab; // Assign your bullet prefab in the Inspector.
    public SpriteRenderer muzzleFlash;
    public float bulletSpeed = 25f; // Adjust bullet speed as needed.
    public int ammoCount = 6;
    public int maxAmmoCount = 6;
    public int ammoInventory = 30;
    public TextMeshProUGUI ammoUI; // Assign your TextMeshProUGUI element in the Inspector.
    public bool isShotgun;
    public float fireRate = 2f; // Shots per second (2 shots per second = 0.5 second delay)'
    public string gunName = "GunName";
    private float nextFireTime = 0f;

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

        // Initialize the UI text at the start
        UpdateAmmoUI();
        muzzleFlash.enabled = false;
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
        if (Input.GetMouseButton(0) && Time.time >= nextFireTime) // Check if mouse button is held AND if enough time has passed
        {
            Shoot();
            nextFireTime = Time.time + (1f / fireRate); // Calculate the next allowed fire time
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }

    void Shoot()
    {
        // Check if there is ammo
        if (ammoCount > 0)
        {
            if (bulletPrefab != null && firePoint != null)
            {
                Flash();
                // Instantiate the bullet at the fire point's position and rotation
                if (isShotgun)
                {
                    float[] angles = { -15f, -7.5f, 0f, 7.5f, 15f }; // Spread angles

                    foreach (float angle in angles)
                    {
                        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                        bullet.transform.localScale *= 0.7f; // Reduce bullet size

                        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                        if (rb != null)
                        {
                            // Rotate bullet and set velocity
                            bullet.transform.Rotate(0, 0, angle);
                            rb.linearVelocity = bullet.transform.right * bulletSpeed;
                        }
                        else
                        {
                            Debug.LogError("Bullet prefab does not have a Rigidbody2D component.");
                        }
                    }
                    ammoCount--;
                    UpdateAmmoUI();
                }
                else{
                    GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

                    // Get the Rigidbody2D component of the bullet
                    Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

                    if (rb != null)
                    {
                        // Apply velocity to the bullet in the forward direction of the fire point
                        rb.linearVelocity = firePoint.right * bulletSpeed;

                        // Optionally, you can destroy the bullet after some time
                        // Destroy(bullet, 2f);

                        // Decrease the ammo count
                        ammoCount--;
                        UpdateAmmoUI(); // Update the UI after shooting
                    }
                    else
                    {
                        Debug.LogError("Bullet prefab does not have a Rigidbody2D component.");
                    }
                }
                
            }
            else
            {
                Debug.LogError("Bullet prefab or firePoint not assigned!");
            }
        }
        else
        {
            Debug.Log("Out of ammo!"); // Optional: Indicate that the gun is out of ammo
            // You could also play a "click" sound or some other feedback here.
            UpdateAmmoUI(); // Update the UI even when out of ammo (shows 0/max)
        }
    }

    void Reload()
    {
        // Calculate how much ammo is needed to reach max
        int ammoNeeded = maxAmmoCount - ammoCount;

        // Check if ammoInventory has enough to fully reload
        if (ammoInventory >= ammoNeeded)
        {
            // Subtract the required amount from inventory and fill up ammoCount
            ammoInventory -= ammoNeeded;
            ammoCount = maxAmmoCount;
        }
        else
        {
            // If not enough ammo, just take what is available
            ammoCount += ammoInventory;
            ammoInventory = 0;
        }

        UpdateAmmoUI(); // Update the UI after reloading
        Debug.Log("Reloaded! Ammo: " + ammoCount + ", Ammo Inventory: " + ammoInventory); 
    }

    private void OnEnable()
    {
        UpdateAmmoUI();
    }

    // Dedicated function to update the ammo UI text
    public void UpdateAmmoUI()
    {
        if (ammoUI != null)
        {
            ammoUI.text = gunName + ammoCount + "/" + ammoInventory;
        }
        else
        {
            Debug.LogError("Ammo UI TextMeshProUGUI element is not assigned in the Inspector!");
        }
    }

    public void Flash()
    {
        StartCoroutine(DoMuzzleFlash());
    }

    private IEnumerator DoMuzzleFlash()
    {
        muzzleFlash.enabled = true;
        yield return new WaitForSeconds(0.05f); // Wait for 0.5 seconds
        muzzleFlash.enabled = false;
    }
}