using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndLevel : MonoBehaviour
{
    [SerializeField] private Image blackimage;
    [SerializeField] private string sceneto;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag( "Player"));
        {
            StartCoroutine(endLevel(1));
        }
    }
    IEnumerator endLevel(float time)
    {
        float elapsedTime = 0f;
        Color startColor = blackimage.color;
        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, 1f);

        while (elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / time;
            blackimage.color = Color.Lerp(startColor, targetColor, t);
            yield return null;
        }

        blackimage.color = targetColor;
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(sceneto);
    }
}
