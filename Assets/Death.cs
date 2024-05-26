using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    [SerializeField] private ParticleSystem DeathVfx;
    [SerializeField] private SpriteRenderer SpriteRenderer;
    void death()
    {
        // Destroy the object that this script is attached to
        DeathVfx.Play();
        StartCoroutine(GameOver(1));
        SpriteRenderer.enabled = false;
    }
    IEnumerator GameOver(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(gameObject);
    }
}
