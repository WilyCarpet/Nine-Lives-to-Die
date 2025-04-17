using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour, CollectibleInterface
{
    [SerializeField] // SerializeField allows you to set this value in the Unity Inspector
    private float healthAmount = 5; // Amount of health to restore

    // This method is called when the collectible is collected by the player.
    public void OnCollect(GameObject player)
    {
       player.GetComponent<HealthController>().Heal(healthAmount); // Call the RestoreHealth method on the player
    }
    
}
