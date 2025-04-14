using UnityEngine;

public class BulletScript : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Destroy the bullet when it collides with anything.
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Destroy the bullet when it collides with a trigger.
        Destroy(gameObject);
    }
}
