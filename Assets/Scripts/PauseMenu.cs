using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    private GameManager gameManager;
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

    }

    public void OnResumeButtonClick()
    {
        gameManager.ResumeButtonPressed();
    }
    public void OnRestartButtonClick()
    {
        gameManager.OnRestartButtonClick();
    }
}
