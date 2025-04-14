using System.Collections.Generic;   
using System.Collections;   
using UnityEngine;
using System.Runtime.CompilerServices;



public class Enemy : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float moveSpeed = 2f;

    public float rotationSpeed = 0.0025f;
    private Rigidbody2D rb;
    public Transform target;


    private void Awake()
    {
       rb = GetComponent<Rigidbody2D>();

    }
    private void Update()
    {
      if(!target){ 

        GetTarget();

      }else {
        RotateTowardsTarget();

      }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = transform.up * moveSpeed;
       
    }
    private void RotateTowardsTarget(){
        Vector2 targetDirection = target.position - transform.position;
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 90f;
        Quaternion q = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.localRotation = Quaternion.Slerp(transform.localRotation, q, rotationSpeed);
    }

    private void GetTarget()
{
    // Find the GameObject with the "Player" tag
    GameObject player = GameObject.FindGameObjectWithTag("Player");
    if (player != null)
    {
        // Assign the player's Transform as the target
        target = player.transform;
    }
}


}