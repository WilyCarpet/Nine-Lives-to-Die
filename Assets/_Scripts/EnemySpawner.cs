using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private float minSpawnRate = 3f;
    [SerializeField] private float maxSpawnRate = 6f;
    [SerializeField] private DoorFunction linkedDoor; // Assign the DoorFunction script of the door in the Inspector
    [SerializeField] private bool startSpawningOnDoorDestroy = true; // Option to control if spawning depends on the door

    private bool canSpawn = false;

    private void Start()
    {
        // If we should start spawning when the door is destroyed, listen for that event
        if (startSpawningOnDoorDestroy && linkedDoor != null)
        {
            // You'll need to modify DoorFunction to have an event or a public flag
            // For simplicity, we'll use a Coroutine to check if the door is destroyed
            StartCoroutine(CheckForDoorDestruction());
        }
        // If not linked to a door or the option is off, start spawning immediately
        else
        {
            canSpawn = true;
            StartCoroutine(Spawner());
        }
    }

    private IEnumerator CheckForDoorDestruction()
    {
        // Wait until the linked door GameObject is destroyed
        while (linkedDoor != null && linkedDoor.gameObject != null)
        {
            yield return null; // Wait for the next frame
        }

        // The door has been destroyed, so now we can start spawning
        canSpawn = true;
        StartCoroutine(Spawner());
    }

    private IEnumerator Spawner()
    {
        while (canSpawn)
        {
            // Generate a random spawn rate between minSpawnRate and maxSpawnRate
            float randomSpawnRate = Random.Range(minSpawnRate, maxSpawnRate);
            WaitForSeconds wait = new WaitForSeconds(randomSpawnRate);

            yield return wait;

            if (enemyPrefabs.Length > 0)
            {
                int rand = Random.Range(0, enemyPrefabs.Length);
                GameObject enemyToSpawn = enemyPrefabs[rand];
                Instantiate(enemyToSpawn, transform.position, Quaternion.identity);
            }
            else
            {
                Debug.LogWarning("No enemy prefabs assigned to the spawner: " + gameObject.name);
                // Optionally stop spawning if there are no prefabs
                canSpawn = false;
            }
        }
    }
}