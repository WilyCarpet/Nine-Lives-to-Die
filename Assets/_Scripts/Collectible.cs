using UnityEngine;

public class Collectible : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private CollectibleInterface collectibleInterface;

    private void Awake()
    {
        collectibleInterface = GetComponent<CollectibleInterface>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<PlayerController>();

        if(player != null){
            
            collectibleInterface.OnCollect(player.gameObject); // Call the OnCollect method on the collectible interface
            Destroy(gameObject); // Destroy the collectible object
        }
    }
}
