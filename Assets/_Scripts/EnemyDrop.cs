using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDrop : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] // SerializeField allows you to set this value in the Unity Inspector
    private float chanceToDrop = 0.5f; // Chance to drop a collectible (50%)

    private CollectibleSpawner collectibleSpawner; // Reference to the CollectibleSpawner script

    private void Awake()
    {
        collectibleSpawner = FindAnyObjectByType<CollectibleSpawner>(); // Find the CollectibleSpawner in the scene
    }

    public void DropCollectible(){
        float random = Random.Range(0f, 1f); // Generate a random float between 0 and 1
        if (random <= chanceToDrop) // Check if the random number is less than or equal to the chance to drop
        {
            Vector2 dropPosition = new Vector2(transform.position.x, transform.position.y); // Set the drop position to the enemy's position

            collectibleSpawner.SpawnCollectible(dropPosition); // Call the SpawnCollectible method on the CollectibleSpawner

        }
    }
}
