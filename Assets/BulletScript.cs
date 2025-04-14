using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

    private Camera mainCamera;
    private Vector3 mousePos;
    private Rigidbody2D rb;
    public float force;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rb = GetComponent<Rigidbody2D>();
        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;
        Vector3 rotation = transform.position - mousePos;
        rb.linearVelocity = new Vector2(direction.x, direction.y).normalized * force;
        float rot = Mathf.Atan2(rotation.y,rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
