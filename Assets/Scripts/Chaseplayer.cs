using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Chaseplayer : MonoBehaviour
{
    public Rigidbody2D rb;
    [SerializeField] private int speed;
    private GameObject player;
    private Quaternion currentRotation;
    private Quaternion targetRotation;
    public float rotationspeed = 2;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player != null)
        {
            Vector2 direction = player.transform.position - transform.position;
            rb.AddForce(transform.up * speed, ForceMode2D.Force);
            direction.Normalize();
            rb.velocity = Vector2.ClampMagnitude(rb.velocity, speed * 2);
            // Calculate the angle towards the player
            float angle = Mathf.Atan2(direction.x,direction.y) * Mathf.Rad2Deg;

            // Create a rotation from the angle
            targetRotation = Quaternion.Euler(-Vector3.forward * angle);
            // Rotate towards the player
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationspeed);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }
}
