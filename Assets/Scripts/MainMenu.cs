using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject OptionsMenu;
    public string sceneName;
    public void PlayGame()
    {
        SceneManager.LoadScene(sceneName);
    }
    public void Options()
    {
        OptionsMenu.SetActive(true);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}