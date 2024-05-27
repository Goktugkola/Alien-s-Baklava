using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerDeathEvent playerDeathEvent;
    [SerializeField] private PauseGameEvent pauseGameEvent;

    private bool isPaused = false;

    private void OnEnable()
    {
        playerDeathEvent.RegisterListener(OnPlayerDeath);
        pauseGameEvent.RegisterListener(OnPaused);
    }

    private void OnDisable()
    {
        playerDeathEvent.UnregisterListener(OnPlayerDeath);
        pauseGameEvent.UnregisterListener(OnPaused);
    }

    private void OnPlayerDeath()
    {
        StartCoroutine(GameOver());
    }

    public void OnPaused()
    {
        if (isPaused)
        {
            print("resume");
            StartCoroutine(ResumeGame());
        }
        else
        {
            print("pause");
            StartCoroutine(PauseGame());
        }
    }

    private IEnumerator GameOver()
    {
        // Stop time
        Time.timeScale = 0f;

        // Load Game Over scene asynchronously
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("GameOver", LoadSceneMode.Additive);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    private IEnumerator PauseGame()
    {
        isPaused = true;

        // Stop time
        Time.timeScale = 0f;

        // Load PauseMenu scene asynchronously
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("PauseMenu", LoadSceneMode.Additive);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    private IEnumerator ResumeGame()
    {
        // Start time
        Time.timeScale = 1f;

        // Unload PauseMenu scene asynchronously
        AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync("PauseMenu");

        while (!asyncUnload.isDone)
        {
            yield return null;
        }

        isPaused = false;
    }
    public void OnRestartButtonClick()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void ResumeButtonPressed()
    {
        StartCoroutine(ResumeGame());
    }
}
