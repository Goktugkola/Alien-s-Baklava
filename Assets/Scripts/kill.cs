using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kill : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Player"))
        {
            print(other.gameObject.name);
            other.gameObject.SendMessage("death");
        }
        if(other.gameObject.CompareTag("Player"))
        { other.gameObject.SendMessage("death"); }
    }
}
