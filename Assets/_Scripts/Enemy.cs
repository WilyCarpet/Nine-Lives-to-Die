using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float rotationSpeed = 0.0025f;
    private Rigidbody2D rb;
    public Transform target;
    public int scoreValue = 10; // Added: Points for killing this enemy
    private ScoreManager scoreManager; // Reference to the ScoreManager

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GetTarget();
        //Find the ScoreManager on Start
        scoreManager = FindFirstObjectByType<ScoreManager>();
        if (scoreManager == null)
        {
            Debug.LogError("ScoreManager not found in the scene!");
        }
    }

    private void Update()
    {
        if (target != null)
        {
            RotateTowardsTarget();
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = transform.up * moveSpeed;
    }

    private void RotateTowardsTarget()
    {
        Vector2 targetDirection = target.position - transform.position;
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 90f;
        Quaternion q = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.localRotation = Quaternion.Slerp(transform.localRotation, q, rotationSpeed);
    }

    private void GetTarget()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            target = player.transform;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            // Check if scoreManager is assigned before using it.
            if (scoreManager != null)
            {
                scoreManager.IncreaseScore(scoreValue);
            }
            Destroy(gameObject);
        }
    }
}
