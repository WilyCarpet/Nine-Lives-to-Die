using UnityEngine;
using TMPro;

public class DoorFunction : MonoBehaviour
{
    // Public variables that can be set in the Unity Inspector.
    public Collider2D targetCollider;  // The specific Collider2D to use for triggering the door.
    public MeshRenderer targetRenderer; // The specific MeshRenderer to disable.
    public TextMeshPro costText;
    public int doorCost = 10;          // The cost to "buy" the door.  Set this in the Inspector.
    private bool playerInside = false; // Flag to track if the player is inside the collider.
    public string playerTag = "Player"; // Tag of the player.

    // Get the Player's Score
    private ScoreManager scoreManager;

    // Use a property if you want other scripts to be able to get the doorCost
    public int DoorCost
    {
        get { return doorCost; }
    }

    void Start()
    {
        //Find the score manager.
        scoreManager = ScoreManager.Instance; // Get the instance of the ScoreManager
        costText.text += doorCost;
        if (scoreManager == null)
        {
            Debug.LogError("ScoreManager not found!  Make sure you have a ScoreManager in your scene.");
        }

        // Make sure targetCollider is assigned
        if (targetCollider == null)
        {
            targetCollider = GetComponent<Collider2D>();
            if (targetCollider == null)
            {
                Debug.LogError("No Collider2D found on this GameObject.  Please assign a Collider2D in the inspector.");
            }
        }
        //Make sure targetRenderer is assigned
        if (targetRenderer == null)
        {
            targetRenderer = GetComponent<MeshRenderer>();
            if (targetRenderer == null)
            {
                Debug.LogError("No MeshRenderer found on this GameObject.  Please assign a MeshRenderer in the inspector.");
            }
        }
    }

    // OnTriggerEnter2D is called when another object enters the collider attached to this GameObject.
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object entering the collider has the tag "Player".
        if (other.CompareTag(playerTag))
        {
            playerInside = true; // Set the flag to true.
            if (targetRenderer != null)
            {
                targetRenderer.enabled = true;
            }
            Debug.Log("Player entered door.");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Check if the object exiting the collider has the tag "Player".
        if (other.CompareTag(playerTag))
        {
            playerInside = false; // Set the flag to false when the player exits.
            if (targetRenderer != null)
            {
                targetRenderer.enabled = false;
            }
            Debug.Log("Player exited door.");
        }
    }

    void Update()
    {
        // Check if the player is inside the collider, the "E" key is pressed, and the player has enough points.
        if (playerInside && Input.GetKeyDown(KeyCode.E))
        {
            //Safeguard against null reference
            if (scoreManager != null && scoreManager.score >= doorCost)
            {
                // Deduct the cost from the player's score.
                scoreManager.DecreaseScore(doorCost);
                Debug.Log("Player bought the door!");
                // Destroy the door GameObject.
                Destroy(gameObject);
            }
            else if (scoreManager != null)
            {
                Debug.Log("Player does not have enough points to buy the door.");
            }
        }
    }

    private void Reset()
    {
        //If the collider is null, try and get it.
        if (targetCollider == null)
        {
            targetCollider = GetComponent<Collider2D>();
            if (targetCollider)
            {
                Debug.Log("Collider2D was automatically assigned.  Please ensure it is the correct collider.");
            }
            else
            {
                Debug.LogError("No Collider2D found on this GameObject.  Please assign a Collider2D in the inspector.");
            }
        }
    }
}
