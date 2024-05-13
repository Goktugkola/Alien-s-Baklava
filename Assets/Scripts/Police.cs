using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Police : MonoBehaviour
{
    private bool isChasing;
    private int maxSpeed;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            isChasing = true;
            transform.GetComponent<Chaseplayer>().enabled = true;
        }
    }
}
