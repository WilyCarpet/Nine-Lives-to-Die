using UnityEngine;

public class GunInventory : MonoBehaviour
{
    public GameObject[] weapons; // Array to store weapon GameObjects
    private int currentWeaponIndex = 0; // Track active weapon
    public bool[] weaponUnlocked = { true, false, false }; // Track unlocked status for each weapon

    void Start()
    {
        UpdateWeaponState();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchWeapon(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchWeapon(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwitchWeapon(2);
        }
    }

    void SwitchWeapon(int weaponIndex)
    {
        if (weaponUnlocked[weaponIndex]) // Check if weapon is unlocked
        {
            currentWeaponIndex = weaponIndex;
            UpdateWeaponState();
        }
        else
        {
            Debug.Log("Weapon " + (weaponIndex + 1) + " is locked!");
        }
    }

    void UpdateWeaponState()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].SetActive(i == currentWeaponIndex); // Activate only the selected weapon
        }
    }
}
