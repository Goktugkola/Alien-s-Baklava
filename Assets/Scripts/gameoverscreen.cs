using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameoverscreen : MonoBehaviour
{
    private GameManager gameManager;
void Start()
    {
        transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y +5,transform.position.z);
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

    }

        public void OnRestartButtonClick()
    {
        gameManager.OnRestartButtonClick();
    }
}
