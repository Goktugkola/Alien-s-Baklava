using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using Unity.VisualScripting;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    // Reference to the Rigidbody2D component of the object affected by gravity
    public Rigidbody2D rb;

    // The planet object that exerts gravity
    public Transform player;
    private bool active = false;

    // Strength of the gravitational pull
    public float gravityForce = 10f;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = player.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(active == true)
        {
        // Calculate the direction vector from the object to the planet
        Vector2 direction = player.position - transform.position;

        // Calculate the distance between the object and the planet
        float distance = direction.magnitude;

        // Normalize the direction vector to get a unit vector (direction only)
        direction.Normalize();

        // Calculate the gravitational force based on distance and gravity strength
        float gravityMagnitude = gravityForce / (distance *2);

        // Apply the gravitational force to the object's Rigidbody2D
        rb.AddForce(-direction * gravityMagnitude, ForceMode2D.Impulse);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) // Replace "Planet" with your planet's tag
        {
            rb = other.GetComponent<Rigidbody2D>();
            player = other.transform;
            active = true;
        }
    }
    void OnTriggerExit2D(Collider2D other){active = false;}
}
