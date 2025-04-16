using UnityEngine;

public class BulletScript : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Destroy the bullet when it collides with anything.
        Destroy(gameObject);
    }
}
