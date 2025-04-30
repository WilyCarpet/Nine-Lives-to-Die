using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] // SerializeField allows you to set this value in the Unity Inspector
    private float damageAmount = 10f; // Amount of damage the bullet will deal

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            
            var healthController = collision.gameObject.GetComponent<HealthController>();

            healthController.TakeDamage(damageAmount);

             // Destroy the bullet after it hits the enemy

        } else if(collision.gameObject.CompareTag("Bullet"))
        {

        }
        else{
            Destroy(gameObject); // Destory the Gameobject after colliding with anything
        }
    }
}
