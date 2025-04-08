using UnityEngine;

public class GunRotation : MonoBehaviour
{
    public Camera mainCamera; // Assign your main camera in the Inspector.
    public GameObject gunObject; // Assign your gun GameObject in the Inspector.

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
    }
}