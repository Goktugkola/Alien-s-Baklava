using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] private PlayerDeathEvent playerDeathEvent;

    private void Die()
    {
        // Call this method when the player dies
        playerDeathEvent.Raise();
    }
}
