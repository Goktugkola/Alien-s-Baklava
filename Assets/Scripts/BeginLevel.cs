using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeginLevel : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Image blackimage;
    void Start()
    {
        StartCoroutine(beginLevel(2));
    }

    // Update is called once per frame
    IEnumerator beginLevel(float time)
    {
        float elapsedTime = 0f;
        Color startColor = new Color(blackimage.color.r,blackimage.color.g,blackimage.color.b,1f);
        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, 0f);

        while (elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / time;
            blackimage.color = Color.Lerp(startColor, targetColor, t);
            yield return null;
        }
        blackimage.color = targetColor;
        yield return new WaitForSeconds(time);
        Debug.Log("Level Start");
    }
}
