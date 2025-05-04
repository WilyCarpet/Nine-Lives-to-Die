using UnityEngine;
using System.Collections.Generic;


public enum AmmoType
{
    Pistol,
    Shotgun,
    MachineGun
}

public class AmmoCollectible : MonoBehaviour, CollectibleInterface
{

    public int ammoAmount = 10;

    private AmmoType assignedAmmoType;

    private void Start()
    {

        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
        {
            Debug.LogError("AmmoCollectible: Could not find player GameObject with tag 'Player'.");

            enabled = false;
            return;
        }


        GunInventory gunInventory = player.GetComponent<GunInventory>();

        if (gunInventory == null)
        {
            Debug.LogError("AmmoCollectible: Player GameObject does not have a GunInventory script.");

            enabled = false;
            return;
        }


        List<AmmoType> unlockedAmmoTypes = new List<AmmoType>();


        if (gunInventory.weaponUnlocked[(int)AmmoType.Pistol])
        {
            unlockedAmmoTypes.Add(AmmoType.Pistol);
        }
        if (gunInventory.weaponUnlocked[(int)AmmoType.Shotgun])
        {
            unlockedAmmoTypes.Add(AmmoType.Shotgun);
        }
        if (gunInventory.weaponUnlocked[(int)AmmoType.MachineGun])
        {
            unlockedAmmoTypes.Add(AmmoType.MachineGun);
        }


        if (unlockedAmmoTypes.Count == 0)
        {
            Debug.LogWarning("AmmoCollectible: No weapons are unlocked, destroying collectible.");
            Destroy(gameObject);
            return;
        }


        int randomIndex = Random.Range(0, unlockedAmmoTypes.Count);
        assignedAmmoType = unlockedAmmoTypes[randomIndex];

        Debug.Log($"Spawned Ammo Collectible of type: {assignedAmmoType}");
    }

    public void OnCollect(GameObject player)
    {
        if (!enabled)
        {
            return;
        }

        Debug.Log($"Ammo collectible of type {assignedAmmoType} collected by {player.name}");


        Transform bodyTransform = player.transform.Find("Body");

        if (bodyTransform == null)
        {
            Debug.LogError("AmmoCollectible: Player does not have a 'body' child object!");
            return;
        }

        string gunObjectName = "";
        switch (assignedAmmoType)
        {
            case AmmoType.Pistol:
                gunObjectName = "Pistol";
                break;
            case AmmoType.Shotgun:
                gunObjectName = "Shotgun";
                break;
            case AmmoType.MachineGun:
                gunObjectName = "Machine Gun";
                break;
        }

        Transform gunTransform = bodyTransform.Find(gunObjectName);

        if (gunTransform == null)
        {
            Debug.LogError($"AmmoCollectible: Could not find gun object '{gunObjectName}' under player's body!");
            return;
        }

        GunControl gunControl = gunTransform.GetComponent<GunControl>();

        if (gunControl == null)
        {
            Debug.LogError($"AmmoCollectible: Gun object '{gunObjectName}' does not have a GunControl script attached!");
            return;
        }

        gunControl.ammoInventory += ammoAmount;
        gunControl.UpdateAmmoUI();
        Debug.Log($"{ammoAmount} {assignedAmmoType} ammo added to {gunObjectName}. New total: {gunControl.ammoInventory}");

        Destroy(gameObject);
    }
}