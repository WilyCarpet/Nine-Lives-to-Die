using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] // SerializeField allows you to set this value in the Unity Inspector
    private float damageAmount = 10f; // Amount of damage the bullet will deal

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            // Destroy the bullet when it collides with an enemy.
            var healthController = collision.gameObject.GetComponent<HealthController>();

            healthController.TakeDamage(damageAmount);
        }
        Destroy(gameObject);
            
    }
}
