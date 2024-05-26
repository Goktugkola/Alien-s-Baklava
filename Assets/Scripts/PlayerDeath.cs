using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] private PlayerDeathEvent playerDeathEvent;
    [SerializeField] private ParticleSystem DeathVfx;
    [SerializeField] private SpriteRenderer sprite;
    void death()
    {
        print("imdead");
        sprite.enabled = false;
        DeathVfx.Play();
        StartCoroutine(GameOver(1));
    }
    IEnumerator GameOver(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        playerDeathEvent.Raise();
    }
}
