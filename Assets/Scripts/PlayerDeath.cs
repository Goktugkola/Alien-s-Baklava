using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] private PlayerDeathEvent playerDeathEvent;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Police"))
        {
            Die();
        }
    }
    private void Die()
    {
        // Call this method when the player dies
        playerDeathEvent.Raise();
    }
}
