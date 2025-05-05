using UnityEngine;
using TMPro;

public class GunBuy : MonoBehaviour
{
    public Collider2D targetCollider;
    public MeshRenderer targetRenderer;
    public TextMeshPro costText;
    public int gunCost = 100;
    private bool playerInside = false;
    public string playerTag = "Player";
    private ScoreManager scoreManager;
    public GunInventory gunInventory;
    public int weaponIndex; // Index of the weapon this purchase unlocks

    void Start()
    {
        scoreManager = ScoreManager.Instance;

        costText.text += gunCost;
        
        if (scoreManager == null)
        {
            Debug.LogError("ScoreManager not found! Ensure you have a ScoreManager in your scene.");
        }

        if (gunInventory == null)
        {
            Debug.LogError("GunInventory not found! Make sure it is attached to the player.");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            playerInside = true;
            if (targetRenderer != null)
            {
                targetRenderer.enabled = true;
            }
            Debug.Log("Player entered gun shop.");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            playerInside = false;
            if (targetRenderer != null)
            {
                targetRenderer.enabled = false;
            }
            Debug.Log("Player exited gun shop.");
        }
    }

    void Update()
    {
        if (playerInside && Input.GetKeyDown(KeyCode.E))
        {
            if (scoreManager != null && scoreManager.score >= gunCost)
            {
                scoreManager.DecreaseScore(gunCost);
                Debug.Log("Player bought weapon " + weaponIndex + "!");

                // Unlock the weapon in GunInventory
                if (gunInventory != null && weaponIndex < gunInventory.weaponUnlocked.Length)
                {
                    gunInventory.weaponUnlocked[weaponIndex] = true;
                }
                else
                {
                    Debug.LogError("Invalid weapon index or GunInventory reference missing!");
                }

                Destroy(gameObject);
            }
            else if (scoreManager != null)
            {
                Debug.Log("Player does not have enough points to buy this weapon.");
            }
        }
    }
}
