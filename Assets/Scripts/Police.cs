using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Police : MonoBehaviour
{
    private bool isChasing;
    private int maxSpeed;
    public Rigidbody2D rb;
    public Animator _animator;

    // Start is called before the first frame update
    void Start()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            isChasing = true;
            _animator.SetBool("isChasing", true);
            transform.GetComponent<Chaseplayer>().enabled = true;
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        print(other.gameObject.name);
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.SendMessage("death");
        }
    }
}
