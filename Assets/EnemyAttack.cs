using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    private float damageAmount;
    // Amount of damage to deal

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            var healthController = collision.gameObject.GetComponent<HealthController>();

            healthController.TakeDamage(damageAmount);
        }
    }
}
