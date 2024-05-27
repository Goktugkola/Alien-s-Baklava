using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameoverscreen : MonoBehaviour
{
    private GameManager gameManager;
void Start()
    {
        transform.position = Camera.main.transform.position;
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

    }

        public void OnRestartButtonClick()
    {
        gameManager.OnRestartButtonClick();
    }
}
