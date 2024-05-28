using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    private GameManager gameManager;
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

    }
    public void OnVolumeButtonToggle(bool muted)
    {   if(muted)
        {
            AudioListener.volume = 0;
            print("b");
        }
        else
        {
            AudioListener.volume = 1;
        }
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
