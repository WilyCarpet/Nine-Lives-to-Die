using System.Collections.Generic;
using UnityEngine;

public class CollectibleSpawner : MonoBehaviour
{
[SerializeField]   
private List<GameObject> collectiblePrefab;

    public void SpawnCollectible(Vector2 position)
    {
        int index = Random.Range(0, collectiblePrefab.Count); // Get a random index from the list of collectibles
        var selectedCollectible = collectiblePrefab[index]; // Select a random collectible prefab from the list

        Instantiate(selectedCollectible, position, Quaternion.identity); // Instantiate the collectible at the specified position with no rotation
    }
}
