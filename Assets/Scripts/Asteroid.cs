using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private Transform TargetPosition;
    [SerializeField] private float speed;
    public Rigidbody2D rb;
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            Vector2 currentPosition = transform.position;
            Vector2 targetPosition = new Vector2(TargetPosition.position.x,TargetPosition.position.y);
            transform.position = Vector2.MoveTowards(currentPosition,targetPosition,speed * Time.deltaTime);
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
